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
    private Player _player;
    private CharacterController _characterController;

    public void StartSpawn(Levels levels)
    {
        SpawnPlayer();
        _spawnEnemy = SpawnEnemy(levels.EnemyPrefab, levels.CountEnemy);
        StartCoroutine(_spawnEnemy);
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
        if (_spawnEnemy != null)
        {
            StopCoroutine(_spawnEnemy);
        }

        var enemy = FindObjectsOfType<Enemy>();
        DestroyObjects(enemy);
    }

    private void DestroyObjects(Enemy[] enemy)
    {
        for (int i = 0; i < enemy.Length; i++)
        {
            Destroy(enemy[i].gameObject);
        }
    }

    private IEnumerator SpawnEnemy(Enemy enemy, int countEnemy)
    {
        while (countEnemy > 0)
        {
            yield return new WaitForSeconds(_delaySpawn);

            CreateEnemy(enemy);
            countEnemy--;
        }

        if (_spawnEnemy != null)
        {
            StopCoroutine(_spawnEnemy);
        }
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