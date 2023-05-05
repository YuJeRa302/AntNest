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

    private Vector3 _moveVector;
    private float _speed;
    private readonly float maxVectorValue = 1f;
    private readonly float minVectorValue = 0.0f;

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
        //_animator.SetFloat(TransitionParametr.Attack.ToString(), true);
    }

    private void Movement()
    {
        _moveVector = Vector3.zero;
        _moveVector.x = _joystick.GetHorizontalValue() * _speed;
        _moveVector.z = _joystick.GetVerticalValue() * _speed;

        _animator.SetFloat(TransitionParametr.Horizontal.ToString(), _moveVector.x);
        _animator.SetFloat(TransitionParametr.Vertical.ToString(), _moveVector.z);
        _animator.SetFloat(TransitionParametr.Speed.ToString(), _moveVector.sqrMagnitude);

        if (Vector3.Angle(Vector3.forward, _moveVector) > maxVectorValue || Vector3.Angle(Vector3.forward, _moveVector) == minVectorValue)
        {
            Vector3 direct = Vector3.RotateTowards(transform.forward, _moveVector, _speed, minVectorValue);
            transform.rotation = Quaternion.LookRotation(direct);
        }

        _characterController.Move(_moveVector * Time.deltaTime);
    }
}