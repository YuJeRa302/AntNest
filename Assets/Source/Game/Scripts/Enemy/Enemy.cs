using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public abstract class Enemy : EnemyMovement
{
    [Header("[Enemy Stats]")]
    [SerializeField] private int _id;
    [SerializeField] private int _level;
    [SerializeField] private int _damage;
    [SerializeField] private int _health;
    [SerializeField] private int _goldReward;
    [SerializeField] private int _experienceReward;
    [SerializeField] private int _score;
    [SerializeField] private float _speed;
    [Header("[Enemy Tag Name]")]
    [SerializeField] private string _tagName;
    [Header("[Enemy NavMeshAgent]")]
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [Header("[Enemy View]")]
    [SerializeField] private EnemyView _enemyView;
    [Header("[Enemy Sound]")]
    [SerializeField] private EnemySound _enemySound;

    private readonly int _minHealth = 0;
    private readonly UnityEvent<int> _healthBarUpdate = new();
    private readonly UnityEvent<Enemy> _die = new();
    private readonly int _delayDestroy = 1;

    public int Id => _id;
    public int Level => _level;
    public int Damage => _damage;
    public float Speed => _speed;
    public int Health => _health;
    public int GoldReward => _goldReward;
    public int ExperienceReward => _experienceReward;
    public int Score => _score;
    public NavMeshAgent NavMeshAgent => _navMeshAgent;
    public Sprite Sprite => _enemyView.Sprite;
    public AudioClip HitPlayer => _enemySound.HitPlayer;
    public AudioSource AudioSource => _enemySound.AudioSource;
    public string TagEnemy => _tagName;
    public EnemyView EnemyView => _enemyView;

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
        _die.Invoke(this);
        SetDyingParameters();
        Destroy(gameObject, _delayDestroy);
    }

    public void TakeDamage(int damage)
    {
        TakeHit();
        _health = Mathf.Clamp(_health - damage, _minHealth, _health);
        _healthBarUpdate.Invoke(Health);
        _enemyView.Hit.Play();
    }

    protected virtual void Start()
    {
        Target = FindObjectOfType<Player>();
        _enemyView.SetSliderValue(Health);
        _enemySound.AudioSource.volume = FindObjectOfType<LevelParameters>().LoadConfig.AmbientVolume;
    }

    private void SetDyingParameters()
    {
        IsDead = true;
        _enemyView.DieEffect.Play();
        _enemyView.Hit.gameObject.SetActive(false);
        _enemySound.AudioSource.PlayOneShot(_enemySound.AudioClipDie);
    }
}