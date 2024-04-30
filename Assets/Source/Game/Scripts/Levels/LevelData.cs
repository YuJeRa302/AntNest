using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Create Level", order = 51)]
public class LevelData : ScriptableObject
{
    [Header("[Level Id]")]
    [SerializeField] private int _levelId;
    [Header("[Level Stats]")]
    [SerializeField] private string _nameScene;
    [SerializeField] private bool _isComplete;
    [Header("[Wave]")]
    [SerializeField] private List<WaveData> _waveDatas;
    [Header("[Level Sprite]")]
    [SerializeField] private Sprite _levelIcon;
    [SerializeField] private Sprite _acceptSprite;
    [SerializeField] private Sprite _cancelSprite;
    [Header("[Level Text]")]
    [SerializeField] private string _standartLevelDescription;
    [SerializeField] private string _endlessLevelDescription;
    [SerializeField] private string _hintsText;

    public string NameScene => _nameScene;
    public string NameEnemy => _waveDatas.FirstOrDefault().EnemyData.Name;
    public string StandartLevelDescription => _standartLevelDescription;
    public string EndlessLevelDescription => _endlessLevelDescription;
    public string HintsText => _hintsText;
    public bool IsComplete => _isComplete;
    public int LevelId => _levelId;
    public Sprite LevelIcon => _levelIcon;
    public Sprite AcceptSprite => _acceptSprite;
    public Sprite CancelSprite => _cancelSprite;
    public List<WaveData> WaveData => _waveDatas;
}

[Serializable]
public class LevelState
{
    public LevelData LevelData;
}

[Serializable]
public class LevelDataState : LevelState
{
    public bool IsStandart;
    public bool IsEndless;
    public bool IsComplete;
}