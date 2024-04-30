using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Player _player;

    private readonly int _minHealth = 0;
    private readonly int _maxHealth = 100;

    private int _currentHealth = 0;

    public event Action<int> ChangedHealth;
    public event Action PlayerDie;

    public int MaxHealth => _maxHealth;
    public int CurrentHealth => _currentHealth;

    public void Initialize()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (_currentHealth > _minHealth)
        {
            var currentDamage = damage - _player.PlayerStats.Armor;

            if (currentDamage < _minHealth) currentDamage = _minHealth;

            _currentHealth = Mathf.Clamp(_currentHealth - currentDamage, _minHealth, _maxHealth);
            ChangedHealth.Invoke(_currentHealth);
        }
        else PlayerDie.Invoke();
    }

    public void TakeHealPotion()
    {
        if (_player.PlayerConsumables.CountHealthPotion > 0 && _currentHealth != _maxHealth) ChangeHealth(Heal());
        else return;
    }

    public void TakeHealRune(int value)
    {
        if (_currentHealth != _maxHealth) ChangeHealth(value);
        else return;
    }

    public void ChangeHealth(int value)
    {
        _currentHealth = Mathf.Clamp(_currentHealth + value, _minHealth, _maxHealth);
        ChangedHealth.Invoke(_currentHealth);
    }

    private int Heal()
    {
        return _player.PlayerConsumables.GetPotion();
    }
}
