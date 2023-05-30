using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "CreateLevelConfig")]
public class LoadConfig : ScriptableObject
{
    private List<Achievements> _achievements;
    private float _ambientVolume;
    private float _interfaceVolume;
    private Levels _loadLevel;
    private int _playerCoins;
    private int _playerLevel;
    private int _playerExperience;

    public float AmbientVolume => _ambientVolume;
    public float InterfaceVolume => _interfaceVolume;
    public Levels Levels => _loadLevel;
    public int PlayerCoins => _playerCoins;
    public int PlayerLevel => _playerLevel;
    public int PlayerExperience => _playerExperience;

    public void SetSoundParametrs(Slider sliderAmbient, Slider sliderInterface)
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

    public void SetPlayerAchievements(List<Achievements> achievements)
    {
        _achievements = new List<Achievements>(achievements);
    }

    public List<Achievements> GetListAchievements()
    {
        return _achievements;
    }
}