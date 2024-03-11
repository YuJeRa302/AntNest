using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private readonly float _maxVectorValue = 1f;
    private readonly float _minVectorValue = 0.0f;
    private readonly float _attackRate = 1.0f;

    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Animator _animator;
    [SerializeField] private Joystick _joystick;
    [SerializeField] private Camera _cameraPlayer;
    [SerializeField] private Player _player;
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

    enum TransitionParametr
    {
        Horizontal,
        Vertical,
        Speed,
        Attack,
        Die
    }

    public void Attack()
    {
        if (_isAllowAttack == true) SetAttackParameters();
        else return;
    }

    private void Update()
    {
        Movement();
    }

    private void Start()
    {
        _speed = _player.PlayerStats.Speed;
    }

    private void OnDrawGizmosSelected()
    {
        if (_attackPoint == null) return;
        else Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
    }

    private void Step()
    {
        _player.PlayerSounds.AudioSourceStep.PlayOneShot(_player.PlayerSounds.FootStep);
    }

    private IEnumerator AttackRate()
    {
        _animator.SetTrigger(TransitionParametr.Attack.ToString());
        _player.PlayerSounds.AudioSourceAxe.PlayOneShot(_player.PlayerSounds.AxeSound);
        yield return new WaitForSeconds(_attackRate);
        FindAttackedEnemy();

        if (_player.PlayerStats.PlayerDamage.AbilityDamage > 0) _player.PlayerStats.PlayerDamage.UpdateWeapon();
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

    private void FindAttackedEnemy()
    {
        Collider[] coliderEnemy = Physics.OverlapSphere(_attackPoint.position, _attackRange, _enemyLayers);

        foreach (Collider collider in coliderEnemy)
        {
            //if (collider.TryGetComponent<Enemy>(out Enemy enemy)) enemy.TakeDamage(_player.PlayerStats.PlayerDamage.CurrentWeapon.Damage);
            if (collider.TryGetComponent<Enemy>(out Enemy enemy)) enemy.TakeDamage(_player.PlayerStats.PlayerEquipment.CurrentWeapon.Value);
        }
    }

    private void SetAttackParameters()
    {
        _isAllowAttack = false;

        if (_makeDamage != null) StopCoroutine(_makeDamage);

        _makeDamage = AttackRate();
        StartCoroutine(_makeDamage);
    }
}