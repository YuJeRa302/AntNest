using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Animator _animator;
    [SerializeField] private Joystick _joystick;
    [SerializeField] private Camera _cameraPlayer;
    [SerializeField] private Player _player;

    private readonly float _maxVectorValue = 1f;
    private readonly float _minVectorValue = 0.0f;

    private Vector3 _moveVector;
    private float _speed;

    private void Awake()
    {
        _player.PlayerStats.PlayerHealth.ChangedHealth += Hit;
    }

    private void OnDestroy()
    {
        _player.PlayerStats.PlayerHealth.ChangedHealth -= Hit;
    }

    private void Start()
    {
        _speed = _player.PlayerStats.Speed;
    }

    private void Update()
    {
        Movement();
    }

    private void Step()
    {
        _player.PlayerSounds.AudioSourceStep.PlayOneShot(_player.PlayerSounds.FootStep);
    }

    private void Hit(int hit)
    {
        _animator.SetTrigger(TransitionParameter.Hit.ToString());
    }

    private void Movement()
    {
        _moveVector = Vector3.zero;
        _moveVector.x = _joystick.GetHorizontalValue() * _speed;
        _moveVector.z = _joystick.GetVerticalValue() * _speed;
        _animator.SetFloat(TransitionParameter.Horizontal.ToString(), _moveVector.x);
        _animator.SetFloat(TransitionParameter.Vertical.ToString(), _moveVector.z);
        _animator.SetFloat(TransitionParameter.Speed.ToString(), _moveVector.sqrMagnitude);

        if (Vector3.Angle(Vector3.forward, _moveVector) > _maxVectorValue || Vector3.Angle(Vector3.forward, _moveVector) == _minVectorValue)
        {
            Vector3 direct = Vector3.RotateTowards(transform.forward, _moveVector, _speed, _minVectorValue);
            transform.rotation = Quaternion.LookRotation(direct);
        }

        _characterController.Move(_moveVector.normalized * _speed * Time.deltaTime);
    }
}