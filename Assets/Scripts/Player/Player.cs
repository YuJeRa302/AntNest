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
    [Header("[Player Achievements]")]
    [SerializeField] private PlayerAchievements _playerAchievements;

    private readonly int _minHealth = 0;
    private readonly UnityEvent<int> _healthBarUpdate = new();
    private readonly UnityEvent _playerDie = new();
    private readonly int _maxHealth = 100;
    private int _currentHealth = 0;

    public int Coins => _wallet.GiveCoin();
    public int PlayerLevel => _playerStats.PlayerLevel;
    public int PlayerExperience => _playerStats.Experience;
    public int PlayerMaxHealth => _maxHealth;
    public PlayerStats PlayerStats => _playerStats;
    public PlayerAchievements PlayerAchievements => _playerAchievements;
    public PlayerEquipment PlayerEquipment => _playerEquipment;
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
        if (_currentHealth > 0)
        {
            _currentHealth = Mathf.Clamp(_currentHealth - (damage - _playerStats.PlayerArmor), _minHealth, _maxHealth);
            _healthBarUpdate.Invoke(_currentHealth);
        }
        else
        {
            _playerDie.Invoke();
        }
    }

    public void Heal()
    {
        if (_playerStats.CountHealthPotion > 0 && _currentHealth != _maxHealth)
        {
            _currentHealth = Mathf.Clamp(_currentHealth + _playerStats.Heal(), _minHealth, _maxHealth);
            _healthBarUpdate.Invoke(_currentHealth);
        }
        else
        {
            return;
        }
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
}