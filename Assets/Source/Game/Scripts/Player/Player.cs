using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [Header("[Wallet]")]
    [SerializeField] private Wallet _wallet;
    [Header("[Player Stats]")]
    [SerializeField] private PlayerStats _playerStats;
    [Header("[Player Movement]")]
    [SerializeField] private PlayerMovement _playerMovement;
    [Header("[Player Equipment]")]
    [SerializeField] private PlayerEquipment _playerEquipment;
    [Header("[Player Effects]")]
    [SerializeField] private PlayerEffects _playerEffects;
    [Header("[Player Sound]")]
    [SerializeField] private PlayerSound _playerSound;

    private readonly int _minHealth = 0;
    private readonly UnityEvent<int> _healthBarUpdate = new();
    private readonly UnityEvent _playerDie = new();
    private readonly int _maxHealth = 100;
    private int _currentHealth = 0;

    public int Coins => _wallet.GiveCoin();
    public int PlayerLevel => _playerStats.PlayerLevel;
    public int PlayerExperience => _playerStats.Experience;
    public int PlayerMaxHealth => _maxHealth;
    public int PlayerCurrentHealth => _currentHealth;
    public PlayerStats PlayerStats => _playerStats;
    public PlayerEquipment PlayerEquipment => _playerEquipment;
    public PlayerEffects PlayerEffects => _playerEffects;
    public PlayerSound PlayerSounds => _playerSound;
    public Wallet Wallet => _wallet;

    public event UnityAction<int> ChangedHealth
    {
        add => _healthBarUpdate.AddListener(value);
        remove => _healthBarUpdate.RemoveListener(value);
    }

    public event UnityAction PlayerDie
    {
        add => _playerDie.AddListener(value);
        remove => _playerDie.RemoveListener(value);
    }

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (_currentHealth > _minHealth)
        {
            var currentDamage = damage - _playerStats.PlayerArmor;

            if (currentDamage < _minHealth) currentDamage = _minHealth;

            if (_playerStats.AbilityArmor > _minHealth) _playerStats.UpdateArmor();

            _currentHealth = Mathf.Clamp(_currentHealth - currentDamage, _minHealth, _maxHealth);
            _healthBarUpdate.Invoke(_currentHealth);
        }
        else _playerDie.Invoke();
    }

    public void TakeHealPotion()
    {
        if (_playerStats.CountHealthPotion > 0 && _currentHealth != _maxHealth)
        {
            ChangeHealth(_playerStats.Heal());
        }
        else return;
    }

    public void TakeHealRune(int value)
    {
        if (_currentHealth != _maxHealth)
        {
            ChangeHealth(value);
        }
        else return;
    }

    public void BuyConsumables(int value)
    {
        _wallet.Buy(value);
        _playerStats.TakePotion();
    }

    public void Recover()
    {
        _currentHealth = _maxHealth;
        _healthBarUpdate.Invoke(_currentHealth);
    }

    public void ChangeHealth(int value)
    {
        _currentHealth = Mathf.Clamp(_currentHealth + value, _minHealth, _maxHealth);
        _healthBarUpdate.Invoke(_currentHealth);
    }
}