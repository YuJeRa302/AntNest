using UnityEngine;

namespace Assets.Source.Game.Scripts
{
    public class PlayerCamera : MonoBehaviour
    {
        [SerializeField] private Transform _playerTransform;

        private Vector3 _deltaPosition;

        private void Awake()
        {
            _deltaPosition = transform.position - _playerTransform.position;
        }

        private void LateUpdate()
        {
            if (_playerTransform != null)
                transform.position = _playerTransform.position + _deltaPosition;
        }
    }
}