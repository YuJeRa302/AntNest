using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public abstract class Enemy : EnemyMovement
{
    [Header("[Enemy Stats]")]
    [SerializeField] private int _id;
    [SerializeField] private int _level;
    [SerializeField] private string _name;
    [SerializeField] private int _damage;
    [SerializeField] private int _health;
    [SerializeField] private int _goldReward;
    [SerializeField] private int _experienceReward;
    [SerializeField] private float _speed;
    [Header("[Enemy NavMeshAgent]")]
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [Header("[Enemy Sprite]")]
    [SerializeField] private Sprite _enemySprite;

    private readonly int _minHealth = 0;
    private readonly UnityEvent<int> _healthBarUpdate = new();
    private readonly UnityEvent<Enemy> _die = new();
    private readonly int _delayDestroy = 1;

    public int Id => _id;
    public int Level => _level;
    public string Name => _name;
    public int Damage => _damage;
    public float Speed => _speed;
    public int Health => _health;
    public int GoldReward => _goldReward;
    public int ExperienceReward => _experienceReward;
    public NavMeshAgent NavMeshAgent => _navMeshAgent;
    public Sprite Sprite => _enemySprite;

    public event UnityAction<int> ChangedHealth
    {
        add => _healthBarUpdate.AddListener(value);
        remove => _healthBarUpdate.RemoveListener(value);
    }

    public event UnityAction<Enemy> Dying
    {
        add => _die.AddListener(value);
        remove => _die.RemoveListener(value);
    }

    public void Die()
    {
        IsDead = true;
        _die.Invoke(this);
        Destroy(gameObject, _delayDestroy);
    }

    public void TakeDamage(int damage)
    {
        _health = Mathf.Clamp(_health - damage, _minHealth, _health);
        _healthBarUpdate.Invoke(Health);
        TakeHit();
    }

    protected virtual void Start()
    {
        EnemyUi.SetSliderValue(Health);
        Target = FindObjectOfType<Player>();
    }
}