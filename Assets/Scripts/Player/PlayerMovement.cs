using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("[Controller]")]
    [SerializeField] private CharacterController _characterController;
    [Header("[Animator]")]
    [SerializeField] private Animator _animator;
    [Header("[Joystick]")]
    [SerializeField] private Joystick _joystick;
    [Header("[Player Camera]")]
    [SerializeField] private Camera _cameraPlayer;
    [Header("[PlayerStats]")]
    [SerializeField] private PlayerStats _playerStats;
    [Header("[AttackPoint]")]
    [SerializeField] private Transform _attackPoint;
    [Header("[AttackRange]")]
    [SerializeField] private float _attackRange = 0.5f;
    [Header("[EnemyLayers]")]
    [SerializeField] private LayerMask _enemyLayers;

    private Vector3 _moveVector;
    private float _speed;
    private bool _isAllowAttack = true;
    private IEnumerator _makeDamage;

    private readonly float _maxVectorValue = 1f;
    private readonly float _minVectorValue = 0.0f;
    private readonly float _attackRate = 1.0f;

    enum TransitionParametr
    {
        Horizontal,
        Vertical,
        Speed,
        Attack,
        Die
    }

    private void Start()
    {
        _speed = _playerStats.Speed;
    }

    private void Update()
    {
        Movement();
    }

    public void Attack()
    {
        if (_isAllowAttack == true)
        {
            _isAllowAttack = false;

            if (_makeDamage != null)
            {
                StopCoroutine(_makeDamage);
            }

            _makeDamage = AttackRate();
            StartCoroutine(_makeDamage);
        }
        else
        {
            return;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (_attackPoint == null)
        {
            return;
        }
        else
        {
            Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
        }
    }

    private IEnumerator AttackRate()
    {
        _animator.SetTrigger(TransitionParametr.Attack.ToString());
        yield return new WaitForSeconds(_attackRate);

        Collider[] coliderEnemy = Physics.OverlapSphere(_attackPoint.position, _attackRange, _enemyLayers);

        foreach (Collider collider in coliderEnemy)
        {
            collider.GetComponent<Enemy>().TakeDamage(_playerStats.PlayerDamage);
        }
        _isAllowAttack = true;
    }

    private void Movement()
    {
        _moveVector = Vector3.zero;
        _moveVector.x = _joystick.GetHorizontalValue() * _speed;
        _moveVector.z = _joystick.GetVerticalValue() * _speed;

        _animator.SetFloat(TransitionParametr.Horizontal.ToString(), _moveVector.x);
        _animator.SetFloat(TransitionParametr.Vertical.ToString(), _moveVector.z);
        _animator.SetFloat(TransitionParametr.Speed.ToString(), _moveVector.sqrMagnitude);

        if (Vector3.Angle(Vector3.forward, _moveVector) > _maxVectorValue || Vector3.Angle(Vector3.forward, _moveVector) == _minVectorValue)
        {
            Vector3 direct = Vector3.RotateTowards(transform.forward, _moveVector, _speed, _minVectorValue);
            transform.rotation = Quaternion.LookRotation(direct);
        }

        _characterController.Move(_moveVector * Time.deltaTime);
    }
}