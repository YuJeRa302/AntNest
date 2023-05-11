using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [Header("[Player Stats]")]
    [SerializeField] private PlayerStats _playerStats;
    [Header("[Player Movement]")]
    [SerializeField] private PlayerMovement _playerMovement;

    private readonly int _minHealth = 0;
    private int _currentHealth = 0;
    private int _playerArmor = 0;
    private int _maxHealth = 100;
    private UnityEvent<int> _healthBarUpdate = new UnityEvent<int>();
    private UnityEvent _playerDie = new UnityEvent();

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

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (_currentHealth > 0)
        {
            _currentHealth = Mathf.Clamp(_currentHealth - (damage - _playerArmor), _minHealth, _maxHealth);
            _healthBarUpdate.Invoke(_currentHealth);
        }
        else
        {
            _playerDie.Invoke();
        }
    }

    public void UpdateArmor(int value)
    {
        _playerArmor += value;
    }

    public void UpdateMaxHealth(int value)
    {
        _maxHealth += value;
    }

    public void Recover()
    {
        _currentHealth = _maxHealth;
        _healthBarUpdate.Invoke(_currentHealth);
    }
}