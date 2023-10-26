using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    [Header("[Sliders]")]
    [SerializeField] private Slider _sliderHP;
    [Header("[Enemy]")]
    [SerializeField] private Enemy _enemy;
    [Header("[Enemy Level]")]
    [SerializeField] private Text _level;
    [Header("[Enemy HP]")]
    [SerializeField] private Text _health;
    [Header("[Enemy Sprite]")]
    [SerializeField] private Image _sprite;

    private Camera _playerCamera;

    private void Start()
    {
        Initialized();
    }

    private void LateUpdate()
    {
        transform.LookAt(_playerCamera.transform);
    }

    public void SetSliderValue(int value)
    {
        _sliderHP.maxValue = value;
        _sliderHP.value = _sliderHP.maxValue;
    }
    public void OnChangeHealth(int target)
    {
        _sliderHP.value = target;
        _health.text = target.ToString();
    }

    private void Initialized()
    {
        _playerCamera = FindObjectOfType<Camera>();
        _level.text = _enemy.Level.ToString();
        _sprite.sprite = _enemy.Sprite;
        _health.text = _enemy.Health.ToString();
    }

    private void OnEnable()
    {
        _enemy.ChangedHealth += OnChangeHealth;
    }

    private void OnDisable()
    {
        _enemy.ChangedHealth -= OnChangeHealth;
    }
}