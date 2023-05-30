using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyUI : MonoBehaviour
{
    [Header("[Sliders]")]
    [SerializeField] private Slider _sliderHP;
    [Header("[Enemy]")]
    [SerializeField] private Enemy _enemy;
    [Header("[Enemy Level]")]
    [SerializeField] private TMP_Text _level;
    [Header("[Enemy Sprite]")]
    [SerializeField] private Image _sprite;

    private Camera _playerCamera;

    private void Start()
    {
        _playerCamera = FindObjectOfType<Camera>();
        _level.text = _enemy.Level.ToString();
        _sprite.sprite = _enemy.Sprite;
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

    private void OnEnable()
    {
        _enemy.ChangedHealth += OnChangeHealth;
    }

    private void OnDisable()
    {
        _enemy.ChangedHealth -= OnChangeHealth;
    }

    public void OnChangeHealth(int target)
    {
        _sliderHP.value = target;
    }
}