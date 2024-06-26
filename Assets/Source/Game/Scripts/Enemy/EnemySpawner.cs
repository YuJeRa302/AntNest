using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("[SpawnParameters]")]
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private int _delayEnemySpawn = 15;
    [Header("[Level Entities]")]
    [SerializeField] private LevelObserver _levelObserver;

    private readonly int _indexDefaultWave = 0;
    private readonly int _minValue = 0;

    private Player _player;
    private List<Enemy> _enemies = new();
    private LevelDataState _levelDataState;
    private int _countSpawnEnemy;
    private int _countEnemyInLastWave = 0;
    private int _indexExtraWave = 1;
    private int _indexEndlessWave = 0;
    private int[] _numberExtraWave;
    private float _soundVolumeEnemyValue;
    private int _totalCountEnemy;
    private int _currentWave = 0;
    private IEnumerator _spawnEnemy;
    private IEnumerator _spawnWave;

    public Action<Enemy> EnemyDied;
    public Action LastEnemyDied;

    private void OnEnable()
    {
        _levelObserver.GamePaused += OnPauseGame;
        _levelObserver.GameResumed += OnResumeGame;
        _levelObserver.GameEnded += OnEndGame;
    }

    private void OnDisable()
    {
        _levelObserver.GamePaused -= OnPauseGame;
        _levelObserver.GameResumed -= OnResumeGame;
        _levelObserver.GameEnded -= OnEndGame;
    }

    public void Initialize(LoadConfig loadConfig, Player player)
    {
        _levelDataState = loadConfig.LevelDataState;
        _player = player;
        _soundVolumeEnemyValue = loadConfig.AmbientVolume;
        _levelObserver.LevelView.ChangeWaveNumber(_currentWave);
        CalculateTotalNumberOfEnemies();
        _numberExtraWave = _levelDataState.LevelData.NumberExtraWave;
        _countSpawnEnemy = _levelDataState.IsStandart == true ? _levelDataState.LevelData.WaveData[_currentWave].CountEnemy :
            _levelDataState.LevelData.WaveEndlessDatas[_indexExtraWave].CountEnemy;
        var levelData = _levelDataState.IsStandart == true ? _levelDataState.LevelData.WaveData : _levelDataState.LevelData.WaveEndlessDatas;
        Spawn(levelData, _currentWave);
    }

    private void OnPauseGame()
    {
        if (_spawnWave != null)
            StopCoroutine(_spawnWave);

        ChangeEnemiesState(false);
    }

    private void OnResumeGame()
    {
        ChangeEnemiesState(true);
        StartCoroutine(_spawnWave);
    }

    private void OnEndGame()
    {
        DestroyEnemies();
    }

    private void OnEnemyDie(Enemy enemy)
    {
        EnemyDied?.Invoke(enemy);
        enemy.Dying -= OnEnemyDie;

        if (_levelDataState.IsStandart == true)
            CheckCountEnemy();
    }

    private void CheckCountEnemy()
    {
        if (_totalCountEnemy == _levelObserver.CountKillEnemy)
            LastEnemyDied?.Invoke();
    }

    private void SpawnNextWave(List<WaveData> waveDatas)
    {
        _currentWave++;

        if (_levelDataState.IsStandart == true)
            SetStandartSpawn(waveDatas, _currentWave);
        else
            SetEndlessSpawn(waveDatas, _indexEndlessWave);
    }

    private void SetStandartSpawn(List<WaveData> waveDatas, int index)
    {
        if (index < _levelDataState.LevelData.WaveData.Count)
        {
            _countSpawnEnemy = waveDatas[index].CountEnemy;
            Spawn(waveDatas, index);
        }
        else
            return;
    }

    private void SetEndlessSpawn(List<WaveData> waveDatas, int index)
    {
        _countEnemyInLastWave++;
        _countSpawnEnemy = _countEnemyInLastWave;
        Spawn(waveDatas, index);
    }

    private List<WaveData> GetWaveData()
    {
        if (_levelDataState.IsStandart == true)
            return _levelDataState.LevelData.WaveData;

        _levelObserver.LevelView.ShowExtraWaveIcon(false);
        _indexEndlessWave = _indexDefaultWave;

        foreach (int index in _numberExtraWave)
        {
            if (_currentWave == index)
            {
                _indexEndlessWave = _indexExtraWave;
                _indexExtraWave++;
                _levelObserver.LevelView.ShowExtraWaveIcon(true);
            }
        }

        if (_indexEndlessWave > _levelDataState.LevelData.WaveEndlessDatas.Count)
            _indexEndlessWave = _indexDefaultWave;

        return _levelDataState.LevelData.WaveEndlessDatas;
    }

    private void Spawn(List<WaveData> waveDatas, int index)
    {
        if (waveDatas.Count > _minValue)
        {
            _spawnWave = SpawnWave(waveDatas, index);
            StartCoroutine(_spawnWave);
        }
        else
            return;
    }

    private IEnumerator SpawnWave(List<WaveData> waveDatas, int index)
    {
        _spawnEnemy = SpawnEnemy(waveDatas[index].EnemyData);
        yield return new WaitForSeconds(waveDatas[index].DelaySpawn);

        _levelObserver.LevelView.ChangeWaveNumber(_currentWave);
        StartCoroutine(_spawnEnemy);

        if (_spawnWave != null)
            StopCoroutine(_spawnWave);
    }

    private IEnumerator SpawnEnemy(EnemyData enemyData)
    {
        _countEnemyInLastWave = _countSpawnEnemy;
        _enemies.Clear();

        while (_countSpawnEnemy > _minValue)
        {
            EnemyCreate(enemyData);
            _countSpawnEnemy--;
            yield return new WaitForSeconds(_delayEnemySpawn);
        }

        if (_spawnEnemy != null)
            StopCoroutine(_spawnEnemy);

        SpawnNextWave(GetWaveData());
    }

    private void EnemyCreate(EnemyData enemyData)
    {
        Enemy enemy = Instantiate(enemyData.PrefabEnemy, new Vector3(_spawnPoint.localPosition.x, _spawnPoint.localPosition.y, _spawnPoint.localPosition.z), new Quaternion(0, 180, 0, 0));
        enemy.Initialize(enemyData, _soundVolumeEnemyValue, _player);
        _enemies.Add(enemy);
        enemy.Dying += OnEnemyDie;
    }

    private void ChangeEnemiesState(bool state)
    {
        if (_enemies.Count > _minValue)
        {
            for (int i = 0; i < _enemies.Count; i++)
            {
                if (_enemies[i] != null)
                    _enemies[i].enabled = state;
            }
        }
        else
            return;
    }

    private void CalculateTotalNumberOfEnemies()
    {
        for (int index = 0; index < _levelDataState.LevelData.WaveData.Count; index++)
        {
            _totalCountEnemy += _levelDataState.LevelData.WaveData[index].CountEnemy;
        }
    }

    private void DestroyEnemies()
    {
        if (_spawnEnemy != null)
            StopCoroutine(_spawnEnemy);

        if (_spawnWave != null)
            StopCoroutine(_spawnWave);

        ClearListEnemies();
    }

    private void ClearListEnemies()
    {
        if (_enemies.Count > _minValue)
        {
            for (int i = 0; i < _enemies.Count; i++)
            {
                if (_enemies[i] != null)
                {
                    if (_enemies[i].gameObject != null)
                    {
                        _enemies[i].Dying -= OnEnemyDie;
                        Destroy(_enemies[i].gameObject);
                    }
                }
            }
        }

        _enemies.Clear();
    }
}
