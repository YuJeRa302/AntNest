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

    private readonly int _indexEndlessWave = 0;

    private Player _player;
    private List<Enemy> _enemies = new();
    private LevelDataState _levelDataState;
    private int _countSpawnEnemy;
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
        _countSpawnEnemy = _levelDataState.LevelData.WaveData[_currentWave].CountEnemy;
        _levelObserver.LevelView.ChangeWaveNumber(_currentWave);
        CalculateTotalNumberOfEnemies();
        Spawn(_levelDataState.LevelData.WaveData, _currentWave);
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
        else return;
    }

    private void SetEndlessSpawn(List<WaveData> waveDatas, int index)
    {
        Spawn(waveDatas, index);
    }

    private void Spawn(List<WaveData> waveDatas, int index)
    {
        if (waveDatas.Count > 0)
        {
            _spawnWave = SpawnWave(waveDatas, index);
            StartCoroutine(_spawnWave);
        }
        else return;
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
        _enemies.Clear();

        while (_countSpawnEnemy > 0)
        {
            EnemyCreate(enemyData);
            _countSpawnEnemy--;
            yield return new WaitForSeconds(_delayEnemySpawn);
        }

        if (_spawnEnemy != null)
            StopCoroutine(_spawnEnemy);

        SpawnNextWave(_levelDataState.LevelData.WaveData); ;
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
        if (_enemies.Count > 0)
        {
            for (int i = 0; i < _enemies.Count; i++)
            {
                if (_enemies[i] != null)
                    _enemies[i].enabled = state;
            }
        }
        else return;
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
        if (_spawnWave != null)
            StopCoroutine(_spawnWave);

        ClearListEnemies();
    }

    private void ClearListEnemies()
    {
        if (_enemies.Count > 0)
        {
            for (int i = 0; i < _enemies.Count; i++)
            {
                if (_enemies[i].gameObject != null)
                    Destroy(_enemies[i].gameObject);

                _enemies[i].Dying -= OnEnemyDie;
            }
        }

        _enemies.Clear();
    }
}
