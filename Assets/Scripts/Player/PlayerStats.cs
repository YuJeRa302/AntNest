using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("[Speed]")]
    [SerializeField] private float _speed;

    public float Speed => _speed;
}