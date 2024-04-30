using UnityEngine;

[CreateAssetMenu(fileName = "New Wave", menuName = "Create Wave", order = 51)]
public class WaveData : ScriptableObject
{
    [Header("[Wave Stats]")]
    [SerializeField] private int _countEnemy;
    [SerializeField] private int _delaySpawn;
    [Header("[Enemy Data]")]
    [SerializeField] private EnemyData _enemyData;

    public int CountEnemy => _countEnemy;
    public int DelaySpawn => _delaySpawn;
    public EnemyData EnemyData => _enemyData;
}