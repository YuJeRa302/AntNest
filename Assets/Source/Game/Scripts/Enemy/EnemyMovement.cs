using System;
using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("[Animator]")]
    [SerializeField] protected Animator Animator;
    [Header("[Enemy]")]
    [SerializeField] private Enemy _enemy;

    private readonly float _delay = 1f;

    private IEnumerator _makeDamage;
    private Player _target;
    private bool _isAttack = false;
    private bool _isDead = false;

    public event Action EnemyDying;

    enum TransitionParametr
    {
        Die,
        Run,
        Attack,
        Hit
    }

    private void OnDestroy()
    {
        _enemy.HitTaking -= TakeHit;
    }

    private void Update()
    {
        if (_isDead != true)
        {
            CheckEnemyHealth();

            if (_isAttack != true)
                Move();
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (_isDead != true)
        {
            if (collision.TryGetComponent<Player>(out Player player))
            {
                Animator.Play(TransitionParametr.Attack.ToString());
                Attack(player);
            }
        }
        else return;
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
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
        if (_enemy.Health == 0)
            Die();
        else return;
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
        var waitForSeconds = new WaitForSeconds(_delay);

        while (_isAttack == true)
        {
            player.PlayerStats.PlayerHealth.TakeDamage(_enemy.Damage);
            yield return waitForSeconds;
        }
    }

    private void Die()
    {
        if (_makeDamage != null)
            StopCoroutine(_makeDamage);

        _isDead = true;
        Animator.Play(TransitionParametr.Die.ToString());
        EnemyDying.Invoke();
    }

    private void Move()
    {
        _enemy.NavMeshAgent.SetDestination(_target.transform.position);
        Animator.Play(TransitionParametr.Run.ToString());
    }

    private void TakeHit()
    {
        Animator.SetTrigger(TransitionParametr.Hit.ToString());
    }

    private void HitPlayer()
    {
        _enemy.EnemySound.PlayHitSound();
    }
}
