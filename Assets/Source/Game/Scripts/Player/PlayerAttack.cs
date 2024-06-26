using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Player _player;
    [SerializeField] private Button _attakButton;
    [Header("[AttackPoint]")]
    [SerializeField] private Transform _attackPoint;
    [Header("[AttackRange]")]
    [SerializeField] private float _attackRange = 0.5f;
    [Header("[EnemyLayers]")]
    [SerializeField] private LayerMask _enemyLayers;

    private readonly float _attackRate = 1.267f;

    private bool _isAllowAttack = true;
    private IEnumerator _makeDamage;

    private void Awake()
    {
        _attakButton.onClick.AddListener(Attack);
    }

    private void OnDestroy()
    {
        _attakButton.onClick.RemoveListener(Attack);
    }

    private void Update()
    {
        if (_player.PlayerInput.IsAttackBlocked == false)
        {
            if (Input.GetKey(KeyCode.Mouse0))
                Attack();
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (_attackPoint == null)
            return;
        else
            Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
    }

    private void Attack()
    {
        if (_isAllowAttack == true)
            SetAttackParameters();
        else
            return;
    }

    private IEnumerator AttackRate()
    {
        _animator.SetTrigger(TransitionParameter.Attack.ToString());
        _player.PlayerSounds.AudioSourceAxe.PlayOneShot(_player.PlayerSounds.AxeSound);

        yield return new WaitForSeconds(_attackRate);

        FindAttackedEnemy();
        _isAllowAttack = true;
    }

    private void FindAttackedEnemy()
    {
        Collider[] coliderEnemy = Physics.OverlapSphere(_attackPoint.position, _attackRange, _enemyLayers);

        foreach (Collider collider in coliderEnemy)
        {
            if (collider.TryGetComponent(out Enemy enemy))
                enemy.TakeDamage(_player.PlayerStats.Damage);
        }
    }

    private void SetAttackParameters()
    {
        _isAllowAttack = false;

        if (_makeDamage != null)
            StopCoroutine(_makeDamage);

        _makeDamage = AttackRate();
        StartCoroutine(_makeDamage);
    }
}
