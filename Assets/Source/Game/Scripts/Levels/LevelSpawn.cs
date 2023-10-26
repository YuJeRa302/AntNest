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

    private readonly int _delaySpawn = 15;

    private IEnumerator _spawnEnemy;
    private IEnumerator _spawnWave;
    private Player _player;
    private CharacterController _characterController;

    public void Initialized(Levels levels, int indexWave)
    {
        SpawnPlayer();
        SpawnNextWave(levels.Wave, indexWave);
    }

    public void EnabledDisabledEnemy(bool state)
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
        DestroyObjects(enemy);
    }

    public void SpawnNextWave(Wave[] wave, int index)
    {
        if (wave.Length > 0)
        {
            _levelParameters.WavePanelView.SetWaveName(index);

            if (_levelParameters.Levels.IsStandart == true) _spawnWave = SpawnWave(wave, index);
            else _spawnWave = SpawnWave(wave, _levelParameters.IndexEndlessWave);

            StartCoroutine(_spawnWave);
        }
        else return;
    }

    private void DestroyObjects(Enemy[] enemy)
    {
        for (int i = 0; i < enemy.Length; i++)
        {
            Destroy(enemy[i].gameObject);
        }
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
        enemy.Dying += _levelParameters.OnEnemyDie;
    }
}