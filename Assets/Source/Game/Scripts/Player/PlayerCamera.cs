using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [Header("PlayerTransform")]
    [SerializeField] private Transform _playerTransform;

    private Vector3 _deltaPosition;

    private void Awake()
    {
        _deltaPosition = transform.position - _playerTransform.position;
    }

    private void FixedUpdate()
    {
        if (_playerTransform != null)
            transform.position = _playerTransform.position + _deltaPosition;
    }
}