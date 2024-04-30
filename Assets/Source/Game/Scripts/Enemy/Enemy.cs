using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(EnemySound))]
[RequireComponent(typeof(EnemyMovement))]
[RequireComponent(typeof(EnemyView))]
[RequireComponent(typeof(EnemyAbility))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [Header("[Enemy Entities]")]
    [SerializeField] private EnemyView _enemyView;
    [SerializeField] private EnemySound _enemySound;
    [SerializeField] private EnemyMovement _enemyMovement;
    [SerializeField] private EnemyAbility _enemyAbility;

    private readonly int _minHealth = 0;
    private readonly int _delayDestroy = 1;

    private int _level;
    private int _damage;
    private int _health;
    private int _goldReward;
    private int _experienceReward;
    private int _score;
    private string _tagName;

    public int Level => _level;
    public int Damage => _damage;
    public int Health => _health;
    public int GoldReward => _goldReward;
    public int ExperienceReward => _experienceReward;
    public int Score => _score;
    public string TagEnemy => _tagName;
    public NavMeshAgent NavMeshAgent => _navMeshAgent;
    public EnemyView EnemyView => _enemyView;
    public EnemyMovement EnemyMovement => _enemyMovement;
    public EnemyAbility EnemyAbility => _enemyAbility;
    public EnemySound EnemySound => _enemySound;

    public event Action<int> ChangedHealth;
    public event Action<Enemy> Dying;
    public event Action HitTaking;

    private void OnDestroy()
    {
        _enemyMovement.EnemyDying -= OnEnemyDying;
    }

    public void Initialize(EnemyData enemyData, float soundVolume, Player player)
    {
        Fill(enemyData);
        _enemySound.Initialize(soundVolume, enemyData);
        _enemyView.Initialize(enemyData);
        _enemyAbility.Initialize(enemyData);
        _enemyMovement.Initialize(player);
        _enemyMovement.EnemyDying += OnEnemyDying;
    }

    public void TakeDamage(int damage)
    {
        _health = Mathf.Clamp(_health - damage, _minHealth, _health);
        HitTaking.Invoke();
        ChangedHealth.Invoke(_health);
    }

    private void OnEnemyDying()
    {
        _enemySound.PlayDieSound();
        Dying.Invoke(this);
        Destroy(gameObject, _delayDestroy);
    }

    private void Fill(EnemyData enemyData)
    {
        _level = enemyData.Level;
        _damage = enemyData.Damage;
        _health = enemyData.Health;
        _goldReward = enemyData.GoldReward;
        _experienceReward = enemyData.ExperienceReward;
        _score = enemyData.Score;
        _tagName = enemyData.Name;
    }
}