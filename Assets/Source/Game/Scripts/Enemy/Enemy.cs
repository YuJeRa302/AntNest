using System;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Source.Game.Scripts
{
    [RequireComponent(typeof(EnemySoundPlayer))]
    [RequireComponent(typeof(EnemyMovement))]
    [RequireComponent(typeof(EnemyHealthBar))]
    [RequireComponent(typeof(EnemyAbilityCaster))]
    [RequireComponent(typeof(EnemyAttacker))]
    public class Enemy : MonoBehaviour
    {
        private readonly int _minHealth = 0;
        private readonly int _delayDestroy = 1;

        [SerializeField] private NavMeshAgent _navMeshAgent;
        [Header("[Enemy Entities]")]
        [SerializeField] private EnemyHealthBar _enemyHealthBar;
        [SerializeField] private EnemySoundPlayer _enemySoundPlayer;
        [SerializeField] private EnemyMovement _enemyMovement;
        [SerializeField] private EnemyAbilityCaster _enemyAbilityCaster;
        [SerializeField] private EnemyAttacker _enemyAttacker;
        [SerializeField] private Transform _particleContainer;

        private ParticleSystem _dieParticle;
        private ParticleSystem _hitParticle;
        private ParticleSystem _abilityParticle;
        private int _health;
        private EnemyData _enemyData;

        public event Action<int> HealthChanged;
        public event Action<Enemy> Dying;
        public event Action HitTaking;

        public int Damage => _enemyData.Damage;
        public int GoldReward => _enemyData.GoldReward;
        public int ExperienceReward => _enemyData.ExperienceReward;
        public int Score => _enemyData.Score;
        public NavMeshAgent NavMeshAgent => _navMeshAgent;
        public EnemyHealthBar EnemyHealthBar => _enemyHealthBar;
        public EnemyMovement EnemyMovement => _enemyMovement;
        public EnemyAttacker EnemyAttacker => _enemyAttacker;

        private void OnDestroy()
        {
            _enemyMovement.AttackingEnemyRemoved -= OnAttackingEnemyRemoved;
        }

        public void Initialize(EnemyData enemyData, float soundVolume, Player player)
        {
            CreateParticleSystem(_particleContainer, enemyData.EnemyDieParticleSystem, enemyData.EnemyHitParticleSystem, enemyData.EnemyAbilityParticleSystem);
            Fill(enemyData);
            _enemySoundPlayer.Initialize(soundVolume, enemyData);
            _enemyHealthBar.Initialize(enemyData, player.PlayerUICamera);
            _enemyAbilityCaster.Initialize(enemyData, _abilityParticle);
            _enemyMovement.Initialize(player);
            _enemyMovement.AttackingEnemyRemoved += OnAttackingEnemyRemoved;
        }

        public void TakeDamage(int damage)
        {
            _health = Mathf.Clamp(_health - damage, _minHealth, _health);

            if (_health == _minHealth)
                EnemyDied();

            _hitParticle.Play();
            HitTaking?.Invoke();
            HealthChanged?.Invoke(_health);
        }

        private void OnAttackingEnemyRemoved()
        {
            Destroy(gameObject);
        }

        private void EnemyDied()
        {
            Dying?.Invoke(this);
            _dieParticle.Play();
            _hitParticle.gameObject.SetActive(false);
            Destroy(gameObject, _delayDestroy);
        }

        private void CreateParticleSystem(Transform container, ParticleSystem particleDie, ParticleSystem particleHit, ParticleSystem particleAbility)
        {
            _dieParticle = Instantiate(particleDie, container);
            _hitParticle = Instantiate(particleHit, container);
            _abilityParticle = Instantiate(particleAbility, container);
        }

        private void Fill(EnemyData enemyData)
        {
            _health = enemyData.Health;
            _enemyData = enemyData;
        }
    }
}