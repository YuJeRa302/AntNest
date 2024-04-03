using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerArmor))]
[RequireComponent(typeof(PlayerDamage))]
[RequireComponent(typeof(PlayerHealth))]
[RequireComponent(typeof(PlayerAbility))]

public class PlayerStats : MonoBehaviour
{
    private readonly Dictionary<int, int> _levels = new();
    private readonly int _maxExperience = 100;

    [SerializeField] private Player _player;
    [SerializeField] private PlayerArmor _playerArmor;
    [SerializeField] private PlayerAbility _playerAbility;
    [SerializeField] private PlayerDamage _playerDamage;
    [SerializeField] private PlayerHealth _playerHealth;
    [Header("[Speed]")]
    [SerializeField] private float _speed;
    [Header("[Max Player Level]")]
    [SerializeField] private int _maxPlayerLevel;

    private int _currentLevel = 1;
    private int _currentExperience = 0;
    private int _score = 0;
    private int _abilityPoints = 0;

    public float Speed => _speed;
    public int Armor => _player.PlayerInventory.CurrentArmor.ItemData.Value;
    public int Damage => _player.PlayerInventory.CurrentWeapon.ItemData.Value;
    public int Score => _score;
    public int Experience => _currentExperience;
    public int Level => _currentLevel;
    public PlayerArmor PlayerArmor => _playerArmor;
    public PlayerDamage PlayerDamage => _playerDamage;
    public PlayerAbility PlayerAbility => _playerAbility;
    public PlayerHealth PlayerHealth => _playerHealth;

    public void Initialize(int level, int experience, int score)
    {
        StatsInitialize();
        GenerateLevelPlayer(_maxPlayerLevel);
        UpdatePlayerStats(level, experience, score);
    }

    public void OnEnemyDie(Enemy enemy)
    {
        _currentExperience += enemy.ExperienceReward;
        _player.Wallet.TakeCoins(enemy.GoldReward);
        _player.PlayerView.OnChangeGold(enemy.GoldReward);
        _player.PlayerView.OnChangeExperience(enemy.ExperienceReward);
        UpdatePlayerScore(enemy.Score);
        SetNewPlayerLevel(_currentLevel);
    }

    private void SetNewPlayerLevel(int level)
    {
        if (_levels.TryGetValue(level, out int value))
        {
            if (_currentExperience >= value)
            {
                var difference = _currentExperience - value;
                _currentLevel++;
                _currentExperience = difference;
                _player.PlayerView.ChangeLevel(_currentLevel);
                _levels.TryGetValue(_currentLevel, out int currentValue);
                _player.PlayerView.SetNewValueSliderExperience(currentValue, _currentExperience);
            }
        }
        else return;
    }

    private void UpdatePlayerScore(int score)
    {
        _score += score;
    }

    private void UpdatePlayerLevel(int currentLevel, int generateExperienceValue, int currentExperience)
    {
        _player.PlayerView.ChangeLevel(currentLevel);
        _player.PlayerView.SetDefaultParameters(generateExperienceValue, currentExperience);
    }

    private void UpdatePlayerStats(int level, int experience, int score)
    {
        _levels.TryGetValue(_currentLevel, out int value);
        _currentLevel = (level == 0) ? _currentLevel : level;
        _currentExperience = (experience == 0) ? _currentExperience : experience;
        _score = score;
        UpdatePlayerLevel(_currentLevel, value, _currentExperience);
        GetNewPointAblility(level);
    }

    private void GenerateLevelPlayer(int level)
    {
        if (_levels.Count == 0)
        {
            for (int i = 0; i < level; i++)
            {
                _levels.Add(i, _maxExperience + _maxExperience * i);
            }
        }
        else return;
    }

    private void StatsInitialize()
    {
        _player.PlayerConsumables.Initialize();
        //PlayerDamage.Initialize();
        PlayerArmor.Initialize();
        PlayerHealth.Initialize();
    }

    private void GetNewPointAblility(int level)
    {
        for (int i = 0; i < level; i++)
        {
            ++_abilityPoints;
        }

        PlayerAbility.SetPoints(_abilityPoints);
    }
}