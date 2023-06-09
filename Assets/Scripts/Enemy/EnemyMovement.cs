using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("[Animator]")]
    [SerializeField] protected Animator Animator;
    [Header("[UI]")]
    [SerializeField] protected EnemyUI EnemyUi;
    [Header("[Enemy]")]
    [SerializeField] private Enemy _enemy;

    protected Player Target;
    protected bool IsAttack = false;
    protected bool IsDead = false;

    private IEnumerator _makeDamage;
    private readonly float _delay = 1f;

    enum TransitionParametr
    {
        Die,
        Run,
        Attack,
        Hit
    }

    protected void Update()
    {
        if (IsDead != true)
        {
            if (IsAttack != true)
            {
                Move();
            }
        }
    }

    protected void TakeHit()
    {
        Animator.SetTrigger(TransitionParametr.Hit.ToString());
        SetStateDie();
    }

    protected void Move()
    {
        _enemy.NavMeshAgent.SetDestination(Target.transform.position);
        Animator.Play(TransitionParametr.Run.ToString());
    }

    protected void SetStateDie()
    {
        if (_enemy.Health == 0)
        {
            Animator.Play(TransitionParametr.Die.ToString());

            if (_makeDamage != null)
            {
                StopCoroutine(_makeDamage);
            }

            _enemy.Die();
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (IsDead != true)
        {
            if (collision.TryGetComponent<Player>(out Player player))
            {
                Animator.Play(TransitionParametr.Attack.ToString());

                OnAttack(player);
            }
        }
        else
        {
            return;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            IsAttack = false;

            if (_makeDamage != null)
            {
                StopCoroutine(_makeDamage);
            }
        }
    }

    protected void OnAttack(Player player)
    {
        IsAttack = true;

        if (_makeDamage != null)
        {
            StopCoroutine(_makeDamage);
        }

        _makeDamage = AttackPlayer(player);
        StartCoroutine(_makeDamage);
    }

    protected virtual IEnumerator AttackPlayer(Player player)
    {
        var waitForSeconds = new WaitForSeconds(_delay);

        while (IsAttack == true)
        {
            player.TakeDamage(_enemy.Damage);
            yield return waitForSeconds;
        }
    }
}
