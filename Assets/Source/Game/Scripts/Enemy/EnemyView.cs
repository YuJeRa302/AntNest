using UnityEngine;
using UnityEngine.UI;

public class EnemyView : MonoBehaviour
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
    [SerializeField] private Sprite _sprite;
    [Header("[Enemy Icon]")]
    [SerializeField] private Image _image;
    [Header("[Enemy Effects]")]
    [SerializeField] private ParticleSystem _hit;
    [SerializeField] private ParticleSystem _dieEffect;
    [Header("[CoolDown Image]")]
    [SerializeField] private Image _coolDownImage;
    [Header("[Cancel Image]")]
    [SerializeField] private Sprite _cancelSprite;

    private Camera _playerCamera;

    public Sprite Sprite => _sprite;
    public ParticleSystem Hit => _hit;
    public ParticleSystem DieEffect => _dieEffect;
    public Image CoolDownImage => _coolDownImage;
    public Sprite CancelSprite => _cancelSprite;

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
        _health.text = _enemy.Health.ToString();
        _image.sprite = _sprite;
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