using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Source.Game.Scripts
{
    [RequireComponent(typeof(PlayerHealth))]
    [RequireComponent(typeof(PlayerAbilityCaster))]

    public class PlayerStats : MonoBehaviour
    {
        private readonly Dictionary<int, int> _levels = new ();
        private readonly int _maxExperience = 100;
        private readonly int _minValue = 0;

        [SerializeField] private Player _player;
        [SerializeField] private PlayerAbilityCaster _playerAbilityCaster;
        [SerializeField] private PlayerHealth _playerHealth;
        [SerializeField] private float _speed;
        [SerializeField] private int _maxPlayerLevel;

        private int _currentLevel = 1;
        private int _currentExperience = 0;
        private int _score = 0;
        private int _abilityPoints = 0;
        private int _currentDamage;
        private int _currentArmor;

        public event Action<int> GoldValueChanged;
        public event Action<int> ExperienceValueChanged;

        public float Speed => _speed;
        public int Armor => _currentArmor;
        public int Damage => _currentDamage;
        public int Score => _score;
        public int Experience => _currentExperience;
        public int Level => _currentLevel;
        public EquipmentItemState CurrentArmor => _player.PlayerInventory.CurrentArmor;
        public EquipmentItemState CurrentWeapon => _player.PlayerInventory.CurrentWeapon;
        public PlayerAbilityCaster PlayerAbilityCaster => _playerAbilityCaster;
        public PlayerHealth PlayerHealth => _playerHealth;

        private void OnDestroy()
        {
            _playerAbilityCaster.AbilityUsed -= OnAbilityUsed;
            _playerAbilityCaster.AbilityEnded -= OnAbilityEnded;
        }

        public void Initialize(int level, int experience, int score)
        {
            PlayerHealth.Initialize();
            GenerateLevelPlayer(_maxPlayerLevel);
            SetPlayerStats(level, experience, score);
            _playerAbilityCaster.AbilityUsed += OnAbilityUsed;
            _playerAbilityCaster.AbilityEnded += OnAbilityEnded;
        }

        public void EnemyDied(Enemy enemy)
        {
            _currentExperience += enemy.ExperienceReward;
            _player.Wallet.TakeCoins(enemy.GoldReward);
            GoldValueChanged?.Invoke(enemy.GoldReward);
            ExperienceValueChanged?.Invoke(enemy.ExperienceReward);
            _score += enemy.Score;
            SetNewPlayerLevel(_currentLevel);
        }

        public void ChangeEquipment()
        {
            _currentDamage = (CurrentWeapon != null) ? CurrentWeapon.ItemData.Value : _minValue;
            _currentArmor = (CurrentArmor != null) ? CurrentArmor.ItemData.Value : _minValue;
        }

        private void OnAbilityUsed(TypeAbility typeAbility, int abilityValue)
        {
            if (typeAbility == TypeAbility.Damage)
                _currentDamage += abilityValue;
            else if (typeAbility == TypeAbility.Armor)
                _currentArmor += abilityValue;

            _player.PlayerView.UpdatePlayerStats();
        }

        private void OnAbilityEnded(TypeAbility typeAbility, int abilityValue)
        {
            if (typeAbility == TypeAbility.Damage)
                _currentDamage -= abilityValue;
            else if (typeAbility == TypeAbility.Armor)
                _currentArmor -= abilityValue;

            _player.PlayerView.UpdatePlayerStats();
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
                    _player.PlayerView.SetNewLevelValue(_currentLevel);
                    _levels.TryGetValue(_currentLevel, out int currentValue);
                    _player.PlayerView.SetExperienceSliderValue(currentValue, _currentExperience);
                }
            }
        }

        private void SetPlayerLevel(int currentLevel, int generateExperienceValue, int currentExperience)
        {
            _player.PlayerView.SetNewLevelValue(currentLevel);
            _player.PlayerView.SetDefaultParameters(generateExperienceValue, currentExperience);
        }

        private void SetPlayerStats(int level, int experience, int score)
        {
            _levels.TryGetValue(_currentLevel, out int value);
            _currentLevel = (level == 0) ? _currentLevel : level;
            _currentExperience = (experience == 0) ? _currentExperience : experience;
            _score = score;
            SetPlayerLevel(_currentLevel, value, _currentExperience);
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
        }

        private void GetNewPointAblility(int level)
        {
            for (int i = 0; i < level; i++)
            {
                ++_abilityPoints;
            }

            _player.Wallet.SetDefaultAbilityPoints(_abilityPoints);
        }
    }
}