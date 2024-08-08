using System;
using UnityEngine;

namespace Assets.Source.Game.Scripts
{
    public class PlayerHealth : MonoBehaviour
    {
        private readonly int _minHealth = 0;
        private readonly int _maxHealth = 100;

        [SerializeField] private Player _player;

        private int _currentHealth = 0;

        public event Action<int> ChangedHealth;
        public event Action<int, int, int, int> PlayerDied;

        public int MaxHealth => _maxHealth;
        public int CurrentHealth => _currentHealth;

        public void Initialize()
        {
            _currentHealth = _maxHealth;
        }

        public void TakeDamage(int damage)
        {
            if (_currentHealth == _minHealth)
                SetPlayerDie();

            if (_currentHealth > _minHealth)
            {
                var currentDamage = damage - _player.PlayerStats.Armor;

                if (currentDamage < _minHealth)
                    currentDamage = _minHealth;

                _currentHealth = Mathf.Clamp(_currentHealth - currentDamage, _minHealth, _maxHealth);
                ChangedHealth?.Invoke(_currentHealth);
            }
        }

        public void TakeHealRune(int value)
        {
            if (_currentHealth != _maxHealth)
                ChangeHealth(value);
        }

        public void ChangeHealth(int value)
        {
            _currentHealth = Mathf.Clamp(_currentHealth + value, _minHealth, _maxHealth);
            ChangedHealth?.Invoke(_currentHealth);
        }

        private void SetPlayerDie()
        {
            Destroy(gameObject);
            PlayerDied?.Invoke(_player.Wallet.Coins, _player.PlayerStats.Level, _player.PlayerStats.Experience, _player.PlayerStats.Score);
        }
    }
}