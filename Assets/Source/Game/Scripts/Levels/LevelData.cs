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
    [SerializeField] private bool _hardDifficult;
    [SerializeField] private int[] _numberExtraWave;
    [Header("[Wave Data]")]
    [SerializeField] private List<WaveData> _waveDatas;
    [SerializeField] private List<WaveData> _waveEndlessDatas;
    [Header("[Level Sprite]")]
    [SerializeField] private Sprite _levelIcon;
    [SerializeField] private Sprite _acceptSprite;
    [SerializeField] private Sprite _cancelSprite;
    [SerializeField] private Sprite _endlessSprite;
    [Header("[Level Text]")]
    [SerializeField] private string _standartLevelDescription;
    [SerializeField] private string _levelAvailable;
    [SerializeField] private string _endlessLevelDescription;
    [SerializeField] private string _hintsText;
    [SerializeField] private string _endlessText;

    public bool HardDifficult => _hardDifficult;
    public string NameScene => _nameScene;
    public string EndlessText => _endlessText;
    public string NameEnemy => _waveDatas.FirstOrDefault().EnemyData.Name;
    public string StandartLevelDescription => _standartLevelDescription;
    public string LevelAvailable => _levelAvailable;
    public string EndlessLevelDescription => _endlessLevelDescription;
    public string HintsText => _hintsText;
    public int[] NumberExtraWave => _numberExtraWave;
    public int LevelId => _levelId;
    public Sprite LevelIcon => _levelIcon;
    public Sprite EndlessSprite => _endlessSprite;
    public List<WaveData> WaveData => _waveDatas;
    public List<WaveData> WaveEndlessDatas => _waveEndlessDatas;
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