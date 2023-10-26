using UnityEngine;

public abstract class Levels : MonoBehaviour
{
    [Header("[Level Id]")]
    [SerializeField] private int _levelId;
    [Header("[Level Stats]")]
    [SerializeField] private string _nameScene;
    [SerializeField] private bool _isComplete;
    [Header("[Wave]")]
    [SerializeField] private Wave[] _wave;
    [Header("[Level Sprite]")]
    [SerializeField] private Sprite _sprite;

    private bool _isStandart = false;
    private bool _isEndless = false;

    public string NameScene => _nameScene;
    public string NameEnemy => _wave[0].EnemyPrefab.TagEnemy;
    public bool IsComplete => _isComplete;
    public bool IsStandart => _isStandart;
    public bool IsEndless => _isEndless;
    public int LevelId => _levelId;
    public Sprite Sprite => _sprite;
    public Wave[] Wave => _wave;

    public void SetComplete()
    {
        _isComplete = true;
    }

    public void SetModeParameters(bool isStandart, bool isEndless)
    {
        _isStandart = isStandart;
        _isEndless = isEndless;
    }
}