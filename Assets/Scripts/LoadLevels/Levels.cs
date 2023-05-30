using UnityEngine;

public abstract class Levels : MonoBehaviour
{
    [Header("[Level Stats]")]
    [SerializeField] private string _nameScene;
    [SerializeField] private string _nameLocation;
    [SerializeField] private int _countEnemy;
    [SerializeField] private bool _isComplete;
    [Header("[Enemy Prefab]")]
    [SerializeField] private Enemy _prefab;
    [Header("[Level Sprite]")]
    [SerializeField] private Sprite _sprite;

    public string NameScene => _nameScene;
    public string NameLocation => _nameLocation;
    public Enemy EnemyPrefab => _prefab;
    public int CountEnemy => _countEnemy;
    public bool IsComplete => _isComplete;
    public Sprite Sprite => _sprite;

    public void SetComplete()
    {
        _isComplete = true;
    }
}