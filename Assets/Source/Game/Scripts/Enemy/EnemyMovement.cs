using System;
using UnityEngine;

namespace Assets.Source.Game.Scripts
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Enemy _enemy;

        private Player _target;

        public event Action AttackingEnemyRemoved;

        public bool IsDead { get; private set; } = false;
        public Animator Animator => _animator;

        private void OnDestroy()
        {
            _enemy.HitTaking -= TakeHit;
            _enemy.Dying -= OnEnemyDying;
        }

        private void Update()
        {
            if (IsDead != true)
            {
                CheckEnemyTarget();

                if (_enemy.EnemyAttacker.IsAttack != true)
                    Move();
            }
        }

        public void Initialize(Player player)
        {
            _target = player;
            _enemy.HitTaking += TakeHit;
            _enemy.Dying += OnEnemyDying;
        }

        private void CheckEnemyTarget()
        {
            if (_target != null)
                return;
            else
                AttackingEnemyRemoved?.Invoke();
        }

        private void OnEnemyDying(Enemy enemy)
        {
            IsDead = true;
            _animator.Play(EnemyTransitionParameter.Die.ToString());
        }

        private void Move()
        {
            _enemy.NavMeshAgent.SetDestination(_target.transform.position);
            _animator.Play(EnemyTransitionParameter.Run.ToString());
        }

        private void TakeHit()
        {
            _animator.SetTrigger(EnemyTransitionParameter.Hit.ToString());
        }
    }
}