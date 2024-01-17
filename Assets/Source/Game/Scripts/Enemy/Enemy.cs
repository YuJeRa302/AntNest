using System;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : EnemyMovement
{
    private readonly int _minHealth = 0;
    private readonly int _delayDestroy = 1;

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

    public int Level => _level;
    public int Damage => _damage;
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


    public event Action<int> ChangedHealth;

    public event Action<Enemy> Dying;


    public void Die()
    {
        Dying.Invoke(this);
        SetDyingParameters();
        Destroy(gameObject, _delayDestroy);
    }

    public void TakeDamage(int damage)
    {
        TakeHit();
        _health = Mathf.Clamp(_health - damage, _minHealth, _health);
        ChangedHealth.Invoke(Health);
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