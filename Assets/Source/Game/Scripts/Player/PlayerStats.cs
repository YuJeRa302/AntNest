using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("[PlayerUI]")]
    [SerializeField] private PlayerUI _playerUI;
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
    private ParticleSystem _abilityEffect;
    private readonly Dictionary<int, int> _levels = new();
    private readonly int _minPoints = 0;
    private int _abilityPoints = 2;
    private int _abilityArmor = 0;
    private int _abilityDamage = 0;
    private int _currentLevel = 0;
    private int _currentHealthPotion = 2;
    private int _currentExperience = 0;
    private int _hitCount;
    private int _score = 0;

    public float Speed => _speed;
    public int PlayerLevel => _currentLevel;
    public int Experience => _currentExperience;
    public int CountHealthPotion => _currentHealthPotion;
    public int PlayerDamage => _player.PlayerEquipment.CurrentWeapon.Damage;
    public int PlayerArmor => _player.PlayerEquipment.CurrentArmor.ItemArmor;
    public int AbilityPoints => _abilityPoints;
    public int AbilityArmor => _abilityArmor;
    public int AbilityDamage => _abilityDamage;
    public int Score => _score;

    public void Initialized(int level, int experience, int score)
    {
        GenerateLevelPlayer(_maxPlayerLevel);
        _levels.TryGetValue(_currentLevel, out int value);
        _currentLevel = (level == 0) ? _currentLevel : level;
        _currentExperience = (experience == 0) ? _currentExperience : experience;
        _score = score;
        UpdatePlayerLevel(_currentLevel, value, _currentExperience, _currentHealthPotion);
    }

    public void OnEnemyDie(Enemy enemy)
    {
        _currentExperience += enemy.ExperienceReward;
        _wallet.TakeCoin(enemy.GoldReward);
        _playerUI.OnChangeGold(enemy.GoldReward);
        _playerUI.OnChangeExperience(enemy.ExperienceReward);
        SetNewPlayerLevel(_currentLevel);
    }

    public int Heal()
    {
        _currentHealthPotion--;
        _playerUI.ChangeCountPotion(_currentHealthPotion);
        return _healing;
    }

    public void TakePotion()
    {
        _currentHealthPotion++;
        _playerUI.ChangeCountPotion(_currentHealthPotion);
    }

    public void GivePoints(int value)
    {
        _abilityPoints = Mathf.Clamp(_abilityPoints - value, _minPoints, _abilityPoints);
    }

    public void SetArmorAbility(int armor, int countHit, ParticleSystem effect)
    {
        _abilityEffect = effect;
        _hitCount = countHit;
        _abilityArmor = armor;
        _player.PlayerEquipment.CurrentArmor.Increase(_abilityArmor);
        UpdatePlayerStats(_player.PlayerEquipment.CurrentArmor.ItemArmor, _player.PlayerEquipment.CurrentWeapon.Damage);
    }

    public void SetDamageAbility(int damage, int countHit, ParticleSystem effect)
    {
        _abilityEffect = effect;
        _hitCount = countHit;
        _abilityDamage = damage;
        _player.PlayerEquipment.CurrentWeapon.Increase(_abilityDamage);
        UpdatePlayerStats(_player.PlayerEquipment.CurrentArmor.ItemArmor, _player.PlayerEquipment.CurrentWeapon.Damage);
    }

    public void UpdateDamage()
    {
        _hitCount--;

        if (_hitCount <= 0)
        {
            _player.PlayerEquipment.CurrentWeapon.Decrease(_abilityDamage);
            _abilityDamage = 0;
            _abilityEffect.Stop();
            UpdatePlayerStats(_player.PlayerEquipment.CurrentArmor.ItemArmor, _player.PlayerEquipment.CurrentWeapon.Damage);
        }
    }

    public void UpdateArmor()
    {
        _hitCount--;

        if (_hitCount <= 0)
        {
            _player.PlayerEquipment.CurrentArmor.Decrease(_abilityArmor);
            _abilityArmor = 0;
            _abilityEffect.Stop();
            UpdatePlayerStats(_player.PlayerEquipment.CurrentArmor.ItemArmor, _player.PlayerEquipment.CurrentWeapon.Damage);
        }
    }

    public void UpdatePlayerStats(int armor, int damage)
    {
        _playerUI.UpdatePlayerStats(armor, damage);
    }

    public void UpdatePlayerScore(int score)
    {
        _score += score;
    }

    private void UpdatePlayerLevel(int currentLevel, int generateExperienceValue, int currentExperience, int currentHealthPotion)
    {
        _playerUI.ChangeLevel(currentLevel);
        _playerUI.SetDefaultParameters(generateExperienceValue, currentExperience);
        _playerUI.ChangeCountPotion(currentHealthPotion);
    }

    private void SetNewPlayerLevel(int level)
    {
        if (_levels.TryGetValue(level, out int value))
        {
            if (_currentExperience >= value)
            {
                var difference = _currentExperience - value;
                _currentLevel++;
                _abilityPoints++;
                _currentExperience = difference;
                _playerUI.ChangeLevel(_currentLevel);
                _levels.TryGetValue(_currentLevel, out int currentValue);
                _playerUI.SetNewValueSliderExperience(currentValue, _currentExperience);
            }
        }
        else return;
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
}