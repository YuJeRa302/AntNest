using System.Collections;
using UnityEngine;

public class LevelSpawn : MonoBehaviour
{
    [Header("[EnemySpawnPoints]")]
    [SerializeField] private Transform _spawnPoint;
    [Header("[PlayerSpawnPoints]")]
    [SerializeField] private Transform _playerSpawn;
    [Header("[LevelParameters]")]
    [SerializeField] private LevelParameters _levelParameters;
    [Header("[Delay Spawn Enemy]")]
    [SerializeField] private int _delaySpawn = 15;

    private int _indexWave = 0;
    private int _currentCountEnemy = 0;
    private IEnumerator _spawnEnemy;
    private IEnumerator _spawnWave;
    private Wave[] _wave;
    private Player _player;
    private CharacterController _characterController;

    public void Initialize(Levels levels, int indexWave)
    {
        SpawnPlayer();
        SpawnNextWave(levels.Wave, indexWave);
    }

    public void ChangeEnemiesState(bool state)
    {
        var enemies = FindObjectsOfType<Enemy>();

        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].enabled = state;
        }
    }

    public void SearchForEnemiesToDestroy()
    {
        if (_spawnWave != null) StopCoroutine(_spawnWave);

        var enemy = FindObjectsOfType<Enemy>();
        DestroyEnemies(enemy);
    }

    public void SpawnNextWave(Wave[] wave, int index)
    {
        if (wave.Length > 0)
        {
            _levelParameters.WavePanelView.SetWaveName(index);

            if (_levelParameters.Levels.IsStandart == true)
            {
                SaveWaveParameters(wave, index);
                _spawnWave = SpawnWave(wave, index);
            }
            else
            {
                SaveWaveParameters(wave, _levelParameters.IndexEndlessWave);
                _currentCountEnemy += index;
                _spawnWave = SpawnWave(wave, _levelParameters.IndexEndlessWave);
            }

            StartCoroutine(_spawnWave);
        }
        else return;
    }

    public void StopSpawn()
    {
        if (_spawnWave != null) StopCoroutine(_spawnWave);
    }

    public void ResumeSpawn()
    {
        _spawnWave = SpawnWave(_wave, _indexWave);
        StartCoroutine(_spawnWave);
    }

    private void SaveWaveParameters(Wave[] wave, int index)
    {
        _wave = wave;
        _indexWave = index;
    }

    private void DestroyEnemies(Enemy[] enemy)
    {
        for (int i = 0; i < enemy.Length; i++)
        {
            Destroy(enemy[i].gameObject);
        }
    }

    private IEnumerator SpawnWave(Wave[] wave, int index)
    {
        yield return new WaitForSeconds(wave[index].DelaySpawn);
        _spawnEnemy = SpawnEnemy(wave[index].EnemyPrefab, wave[index].CountEnemy + _currentCountEnemy);
        StartCoroutine(_spawnEnemy);

        if (_spawnWave != null) StopCoroutine(_spawnWave);
    }

    private IEnumerator SpawnEnemy(Enemy enemy, int countEnemy)
    {
        while (countEnemy > 0)
        {
            CreateEnemy(enemy);
            countEnemy--;
            yield return new WaitForSeconds(_delaySpawn);
        }

        if (_spawnEnemy != null) StopCoroutine(_spawnEnemy);
    }

    private void SpawnPlayer()
    {
        _player = FindObjectOfType<Player>();
        _characterController = _player.GetComponent<CharacterController>();
        _characterController.enabled = false;
        _player.transform.localPosition = _playerSpawn.localPosition;
        _characterController.enabled = true;
    }

    private void CreateEnemy(Enemy template)
    {
        Enemy enemy = Instantiate(template, new Vector3(_spawnPoint.localPosition.x, _spawnPoint.localPosition.y, _spawnPoint.localPosition.z), new Quaternion(0, 180, 0, 0));
        //enemy.Dying += _levelParameters.LevelObserver.DyingEnemy;
    }
}