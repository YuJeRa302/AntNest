using UnityEngine;

namespace Assets.Source.Game.Scripts
{
    [CreateAssetMenu(fileName = "New Wave", menuName = "Create Wave", order = 51)]
    public class WaveData : ScriptableObject
    {
        [SerializeField] private int _countEnemy;
        [SerializeField] private int _delaySpawn;
        [SerializeField] private EnemyData _enemyData;

        public int CountEnemy => _countEnemy;
        public int DelaySpawn => _delaySpawn;
        public EnemyData EnemyData => _enemyData;
    }
}