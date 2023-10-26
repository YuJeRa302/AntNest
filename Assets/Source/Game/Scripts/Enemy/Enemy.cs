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
    [SerializeField] private float _speed;
    [Header("[Enemy Tag Name]")]
    [SerializeField] private string _tagName;
    [Header("[Ability Damage]")]
    [SerializeField] private int _abilityDamage;
    [Header("[Enemy NavMeshAgent]")]
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [Header("[Enemy Sprite]")]
    [SerializeField] private Sprite _enemySprite;
    [Header("[Enemy Effects]")]
    [SerializeField] private ParticleSystem _hit;
    [SerializeField] private ParticleSystem _ability;
    [SerializeField] private ParticleSystem _dieEffect;
    [Header("[Health Threshold]")]
    [SerializeField] private int _healthThreshold;
    [Header("[Audio Source]")]
    [SerializeField] private AudioSource _audioSource;
    [Header("[Audio Clip]")]
    [SerializeField] private AudioClip _audioClipDie;
    [SerializeField] private AudioClip _hitPlayer;

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
    public NavMeshAgent NavMeshAgent => _navMeshAgent;
    public Sprite Sprite => _enemySprite;
    public int AbilityDamage => _abilityDamage;
    public AudioClip HitPlayer => _hitPlayer;
    public AudioSource AudioSource => _audioSource;
    public string TagEnemy => _tagName;

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
        CastAbility();
        TakeHit();
        _health = Mathf.Clamp(_health - damage, _minHealth, _health);
        _healthBarUpdate.Invoke(Health);
        _hit.Play();
    }

    protected virtual void Start()
    {
        EnemyUi.SetSliderValue(Health);
        Target = FindObjectOfType<Player>();
        _audioSource.volume = FindObjectOfType<LevelParameters>().LoadConfig.AmbientVolume;
    }

    private void CastAbility()
    {
        if (_health < _healthThreshold)
        {
            if (_ability != null)
            {
                _ability.gameObject.SetActive(true);
                _ability.Play();
            }
        }
        else return;
    }

    private void SetDyingParameters()
    {
        IsDead = true;
        _dieEffect.Play();
        _hit.gameObject.SetActive(false);
        _audioSource.PlayOneShot(_audioClipDie);
    }
}