using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "CreateLevelConfig")]
public class LoadConfig : ScriptableObject
{
    [Header("[Sound]")]
    [SerializeField] private float _ambientVolume;
    [SerializeField] private float _interfaceVolume;
    [Header("[Player Stats]")]
    [SerializeField] private int _playerCoins;
    [SerializeField] private int _playerLevel;
    [SerializeField] private int _playerExperience;

    private List<Achievements> _achievements;
    private Levels _loadLevel;
    private string _language;
    private Sprite _languageSprite;
    private Dictionary<int, bool> _playerLevels =  new();

    public float AmbientVolume => _ambientVolume;
    public float InterfaceVolume => _interfaceVolume;
    public Levels Levels => _loadLevel;
    public int PlayerCoins => _playerCoins;
    public int PlayerLevel => _playerLevel;
    public int PlayerExperience => _playerExperience;
    public List<Achievements> Achievements => _achievements;
    public string Language => _language;
    public Sprite LanguageSprite => _languageSprite;
    public Dictionary<int, bool> PlayerLevels => _playerLevels;

    public void SetCurrentLanguageImage(Sprite sprite)
    {
        _languageSprite = sprite;
    }

    public void SetCurrentLanguage(string value)
    {
        _language = value;
    }

    public void SetSoundParameters(Slider sliderAmbient, Slider sliderInterface)
    {
        _ambientVolume = sliderAmbient.value;
        _interfaceVolume = sliderInterface.value;
    }

    public void SetLevelParameters(Levels loadLevel)
    {
        _loadLevel = loadLevel;
    }

    public void SetPlayerParameters(int coins, int level, int experience)
    {
        _playerLevel = level;
        _playerExperience = experience;
        _playerCoins = coins;
    }

    public void UpdateListPlayerLevels(Dictionary<int, bool> playerLevels) 
    {
        _playerLevels = playerLevels;
    }

    public void SetPlayerAchievements(List<Achievements> achievements)
    {
        _achievements = new List<Achievements>(achievements);
    }
}