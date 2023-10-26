using UnityEngine;

public class Wave : MonoBehaviour
{
    [Header("[Wave Stats]")]
    [SerializeField] private string _nameWave;
    [SerializeField] private int _countEnemy;
    [SerializeField] private int _delaySpawn;
    [Header("[Enemy Prefab]")]
    [SerializeField] private Enemy _prefabEnemy;

    public string NameWave => _nameWave;
    public int CountEnemy => _countEnemy;
    public int DelaySpawn => _delaySpawn;
    public Enemy EnemyPrefab => _prefabEnemy;
}
