using System.Collections;
using UnityEngine;

namespace Assets.Source.Game.Scripts
{
    public class EnemyAttacker : MonoBehaviour
    {
        private readonly float _attackDelay = 0.75f;

        [SerializeField] private Enemy _enemy;

        private IEnumerator _makeDamage;

        public bool IsAttack { get; private set; }

        private void Awake()
        {
            _enemy.Dying += OnEnemyDying;
        }

        private void OnDestroy()
        {
            _enemy.Dying -= OnEnemyDying;
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (_enemy.EnemyMovement.IsDead != true)
            {
                if (collision.TryGetComponent(out Player player))
                    Attack(player);
            }
        }

        private void OnTriggerExit(Collider collision)
        {
            if (collision.TryGetComponent(out Player player))
            {
                IsAttack = false;

                if (_makeDamage != null)
                    StopCoroutine(_makeDamage);
            }
        }

        private void Attack(Player player)
        {
            IsAttack = true;

            if (_makeDamage != null)
                StopCoroutine(_makeDamage);

            _makeDamage = AttackPlayer(player);
            StartCoroutine(_makeDamage);
        }

        private IEnumerator AttackPlayer(Player player)
        {
            while (IsAttack == true)
            {
                _enemy.EnemyMovement.Animator.Play(EnemyTransitionParameter.Attack.ToString());
                player.PlayerStats.PlayerHealth.TakeDamage(_enemy.Damage);
                yield return new WaitForSeconds(_attackDelay);
            }
        }

        private void OnEnemyDying(Enemy enemy)
        {
            if (_makeDamage != null)
                StopCoroutine(_makeDamage);
        }
    }
}