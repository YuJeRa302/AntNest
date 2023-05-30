using UnityEngine;

public abstract class Achievements : MonoBehaviour
{
    [SerializeField] private int _id;
    [SerializeField] private string _name;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private int _currentCount;
    [SerializeField] private int _maxCount;

    private readonly int _minCount = 0;

    public int Id => _id;
    public Sprite EnemyIcon => _sprite;
    public string Name => _name;
    public int CurrentCount => _currentCount;
    public int MaxCount => _maxCount;

    public void UpdateCountEnemy(int value)
    {
        _currentCount = _currentCount + value < _maxCount ? _currentCount + value : _currentCount + _minCount;
    }
}