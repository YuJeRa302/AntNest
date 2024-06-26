using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "CreateLevelConfig")]
public class LoadConfig : ScriptableObject
{
    [Header("[Sound]")]
    [SerializeField] private float _ambientVolume;
    [SerializeField] private float _interfaceVolume;
    [SerializeField] private bool _isSoundOn;
    [Header("[Player Stats]")]
    [SerializeField] private bool _isFirstSession;
    [SerializeField] private int _playerCoins;
    [SerializeField] private int _playerLevel;
    [SerializeField] private int _playerExperience;
    [SerializeField] private int _playerScore;

    private string _language = string.Empty;
    private LevelDataState _levelDataState;
    private bool[] _levelsComplete;
    private bool _isGamePause;
    private int _countLevels = 0;
    private TypeDevice _typeDevice;

    public bool IsGamePause => _isGamePause;
    public bool IsFirstSession => _isFirstSession;
    public bool IsSoundOn => _isSoundOn;
    public TypeDevice TypeDevice => _typeDevice;
    public float AmbientVolume => _ambientVolume;
    public float InterfaceVolume => _interfaceVolume;
    public int PlayerCoins => _playerCoins;
    public int PlayerLevel => _playerLevel;
    public int PlayerScore => _playerScore;
    public int PlayerExperience => _playerExperience;
    public int CountLevels => _countLevels;
    public string Language => _language;
    public bool[] LevelsComplete => _levelsComplete;
    public LevelDataState LevelDataState => _levelDataState;

    public void SetPauseGameState(bool value)
    {
        _isGamePause = value;
    }

    public void SetSoundState(bool value)
    {
        _isSoundOn = value;
    }

    public void SetSessionState(bool value)
    {
        _isFirstSession = value;
    }

    public void SetCurrentDevice(TypeDevice typeDevice)
    {
        _typeDevice = typeDevice;
    }

    public void SetCountLevels(int value)
    {
        _countLevels = value;
    }

    public void SetCurrentLanguage(string value)
    {
        _language = value;
    }

    public void SetAmbientVolume(float value)
    {
        _ambientVolume = value;
    }

    public void SetIterfaceVolume(float value)
    {
        _interfaceVolume = value;
    }

    public void SetPlayerParameters(int coins, int level, int experience, int score)
    {
        _playerLevel = level;
        _playerExperience = experience;
        _playerCoins = coins;
        _playerScore = score;
    }

    public void UpdateListPlayerLevels(bool[] levelsComplete)
    {
        _levelsComplete = levelsComplete;
    }

    public void LoadLevelData(LevelDataState levelDataState)
    {
        _levelDataState = levelDataState;
    }
}