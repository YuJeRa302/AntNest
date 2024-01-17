using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private readonly int _indexEndlessWave = 0;
    private readonly int _defaultCountEnemy = 0;
    [Header("[SpawnParameters]")]
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private int _delayEnemySpawn = 15;
    [Header("[Level Entities]")]
    [SerializeField] private LevelObserver _levelObserver;

    private List<Enemy> _enemies = new();
    private int _totalCountEnemy;
    private int _currentWave = 0;
    private int _currentCountEnemy = 0;
    private Levels _levels;
    private IEnumerator _spawnEnemy;
    private IEnumerator _spawnWave;

    public Action<Enemy> EnemyDied;
    public Action<int> WaveSpawning;
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

    public void Initialize(LoadConfig loadConfig)
    {
        _levels = loadConfig.Levels;
        CalculateTotalNumberOfEnemies();
        SpawnNextWave(_levelObserver.IsPlayerAlive);
    }

    private void OnPauseGame()
    {
        if (_spawnWave != null) StopCoroutine(_spawnWave);

        ChangeEnemiesState(false);
    }

    private void OnResumeGame()
    {
        ChangeEnemiesState(true);
        SpawnNextWave(_levelObserver.IsPlayerAlive);
    }

    private void OnEndGame()
    {
        DestroyEnemies();
    }

    private void OnEnemyDie(Enemy enemy)
    {
        _currentCountEnemy++;
        EnemyDied?.Invoke(enemy);
        enemy.Dying -= OnEnemyDie;
        SpawnNextWave(_levelObserver.IsPlayerAlive);
    }

    private void SpawnNextWave(bool isPlayerAlive)
    {
        if (isPlayerAlive == true)
        {
            if (_levels.IsStandart == true) SetStandartSpawn(_currentCountEnemy);
            else SetEndlessSpawn(_currentCountEnemy);
        }
        else return;
    }

    private void SetStandartSpawn(int currentCountEnemy)
    {
        if (_currentWave < _levels.Wave.Length - 1)
        {
            if (currentCountEnemy == _levels.Wave[_currentWave].CountEnemy)
            {
                SetWaveParameters(_levels.Wave, _currentWave);
            }
            else return;
        }
        else if (_totalCountEnemy == _levelObserver.CountKillEnemy) LastEnemyDied?.Invoke();
    }

    private void SetEndlessSpawn(int currentCountEnemy)
    {
        if (currentCountEnemy == _levels.Wave[_indexEndlessWave].CountEnemy)
        {
            SetWaveParameters(_levels.Wave, _indexEndlessWave);
        }
        else return;
    }

    private void SetWaveParameters(Wave[] waves, int index)
    {
        _currentWave++;
        WaveSpawning?.Invoke(_currentWave);
        Spawn(waves, index);
        _currentCountEnemy = _defaultCountEnemy;
    }

    private void Spawn(Wave[] wave, int index)
    {
        if (wave.Length > 0)
        {
            _spawnWave = SpawnWave(wave, index);
            StartCoroutine(_spawnWave);
        }
        else return;
    }

    private IEnumerator SpawnWave(Wave[] wave, int index)
    {
        yield return new WaitForSeconds(wave[index].DelaySpawn);
        _spawnEnemy = SpawnEnemy(wave[index].EnemyPrefab, wave[index].CountEnemy);
        StartCoroutine(_spawnEnemy);

        if (_spawnWave != null) StopCoroutine(_spawnWave);
    }

    private IEnumerator SpawnEnemy(Enemy enemy, int countEnemy)
    {
        _enemies.Clear();

        while (countEnemy > 0)
        {
            EnemyCreate(enemy);
            countEnemy--;
            yield return new WaitForSeconds(_delayEnemySpawn);
        }

        if (_spawnEnemy != null) StopCoroutine(_spawnEnemy);
    }

    private void EnemyCreate(Enemy template)
    {
        Enemy enemy = Instantiate(template, new Vector3(_spawnPoint.localPosition.x, _spawnPoint.localPosition.y, _spawnPoint.localPosition.z), new Quaternion(0, 180, 0, 0));
        _enemies.Add(enemy);
        enemy.Dying += OnEnemyDie;
    }

    private void ChangeEnemiesState(bool state)
    {
        if (_enemies.Count > 0)
        {
            for (int i = 0; i < _enemies.Count; i++)
            {
                _enemies[i].enabled = state;
            }
        }
        else return;
    }

    private void CalculateTotalNumberOfEnemies()
    {
        for (int index = 0; index < _levels.Wave.Length; index++)
        {
            _totalCountEnemy += _levels.Wave[index].CountEnemy;
        }
    }

    private void DestroyEnemies()
    {
        if (_spawnWave != null) StopCoroutine(_spawnWave);

        ClearListEnemies();
    }

    private void ClearListEnemies()
    {
        if (_enemies.Count > 0)
        {
            for (int i = 0; i < _enemies.Count; i++)
            {
                Destroy(_enemies[i].gameObject);
                _enemies[i].Dying -= OnEnemyDie;
            }
        }
        else return;
    }
}
