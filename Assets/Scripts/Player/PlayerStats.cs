using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("[UI]")]
    [SerializeField] private UIUpdate _uIUpdate;
    [Header("[Wallet]")]
    [SerializeField] private Wallet _wallet;
    [Header("[Player]")]
    [SerializeField] private Player _player;
    [Header("[Speed]")]
    [SerializeField] private float _speed;
    [Header("[MaxPlayerLevel]")]
    [SerializeField] private int _maxPlayerLevel;
    [Header("[Healing Value]")]
    [SerializeField] private int _healing = 20;

    private readonly int _maxExperience = 100;
    private readonly Dictionary<int, int> _levels = new();
    private int _currentLevel = 0;
    private int _currentHealthPotion = 2;
    private int _currentExperience = 0;

    public float Speed => _speed;
    public int PlayerLevel => _currentLevel;
    public int Experience => _currentExperience;
    public int CountHealthPotion => _currentHealthPotion;
    public int PlayerDamage => _player.PlayerEquipment.CurrentWeapon.Damage;
    public int PlayerArmor => _player.PlayerEquipment.CurrentArmor.ItemArmor;

    public void SetDefaultLevel(int level, int experience)
    {
        GenerateLevelPlayer(_maxPlayerLevel);
        _levels.TryGetValue(_currentLevel, out int value);
        _currentLevel = (level == 0) ? _currentLevel : level;
        _currentExperience = (experience == 0) ? _currentExperience : experience;
        _uIUpdate.ChangeLevel(_currentLevel);
        _uIUpdate.SetDefaultParameters(value, _currentExperience);
        _uIUpdate.ChangeCountPotion(_currentHealthPotion);
    }

    public void OnEnemyDie(Enemy enemy)
    {
        _currentExperience += enemy.ExperienceReward;
        _wallet.TakeCoin(enemy.GoldReward);
        _uIUpdate.OnChangeGold(enemy.GoldReward);
        _uIUpdate.OnChangeExperience(enemy.ExperienceReward);
        UpdateStats(_currentLevel);
    }

    public int Heal()
    {
        _currentHealthPotion--;
        _uIUpdate.ChangeCountPotion(_currentHealthPotion);
        return _healing;
    }

    public void TakePotion()
    {
        _currentHealthPotion++;
        _uIUpdate.ChangeCountPotion(_currentHealthPotion);
    }

    private void UpdateStats(int level)
    {
        if (_levels.TryGetValue(level, out int value))
        {
            if (_currentExperience >= value)
            {
                var difference = _currentExperience - value;
                _currentLevel++;
                _currentExperience = difference;
                _uIUpdate.ChangeLevel(_currentLevel);
                _levels.TryGetValue(_currentLevel, out int currentValue);
                _uIUpdate.SetNewValueSliderExperience(currentValue, _currentExperience);
            }
        }
        else
        {
            return;
        }
    }

    private void GenerateLevelPlayer(int level)
    {
        for (int i = 0; i < level; i++)
        {
            _levels.Add(i, _maxExperience + _maxExperience * i);
        }
    }
}