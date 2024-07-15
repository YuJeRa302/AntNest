using System;
using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private readonly float _delay = 0.75f;

    [Header("[Animator]")]
    [SerializeField] protected Animator Animator;
    [Header("[Enemy]")]
    [SerializeField] private Enemy _enemy;

    private IEnumerator _makeDamage;
    private Player _target;
    private bool _isAttack = false;
    private bool _isDead = false;

    public event Action EnemyDying;
    public event Action AttackingEnemyRemoved;

    private void OnDestroy()
    {
        _enemy.HitTaking -= TakeHit;
    }

    private void Update()
    {
        if (_isDead != true)
        {
            CheckEnemyHealth();
            CheckEnemyTarget();

            if (_isAttack != true)
                Move();
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (_isDead != true)
        {
            if (collision.TryGetComponent(out Player player))
                Attack(player);
        }
        else
            return;
    }

    private void CheckEnemyTarget()
    {
        if (_target != null)
            return;
        else
            AttackingEnemyRemoved?.Invoke();
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            _isAttack = false;

            if (_makeDamage != null)
                StopCoroutine(_makeDamage);
        }
    }

    public void Initialize(Player player)
    {
        _target = player;
        _enemy.HitTaking += TakeHit;
    }

    private void CheckEnemyHealth()
    {
        if (_enemy.Health == _enemy.MinHealth)
            Die();
        else
            return;
    }

    private void Attack(Player player)
    {
        _isAttack = true;

        if (_makeDamage != null)
            StopCoroutine(_makeDamage);

        _makeDamage = AttackPlayer(player);
        StartCoroutine(_makeDamage);
    }

    private IEnumerator AttackPlayer(Player player)
    {
        while (_isAttack == true)
        {
            Animator.Play(EnemyTransitionParameter.Attack.ToString());
            player.PlayerStats.PlayerHealth.TakeDamage(_enemy.Damage);
            yield return new WaitForSeconds(_delay);
        }
    }

    private void Die()
    {
        if (_makeDamage != null)
            StopCoroutine(_makeDamage);

        _isDead = true;
        Animator.Play(EnemyTransitionParameter.Die.ToString());
        EnemyDying.Invoke();
    }

    private void Move()
    {
        _enemy.NavMeshAgent.SetDestination(_target.transform.position);
        Animator.Play(EnemyTransitionParameter.Run.ToString());
    }

    private void TakeHit()
    {
        Animator.SetTrigger(EnemyTransitionParameter.Hit.ToString());
    }

    private void HitPlayer()
    {
        _enemy.EnemySound.PlayHitSound();
    }
}
