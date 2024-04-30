using UnityEngine;
using UnityEngine.UI;

public class EnemyView : MonoBehaviour
{
    [Header("[Sliders]")]
    [SerializeField] private Slider _sliderHP;
    [Header("[Enemy Stats]")]
    [SerializeField] private Enemy _enemy;
    [SerializeField] private Text _level;
    [SerializeField] private Text _health;
    [SerializeField] private Image _enemyIcon;
    [Header("[Enemy Effects]")]
    [SerializeField] private ParticleSystem _hit;
    [SerializeField] private ParticleSystem _dieEffect;
    [SerializeField] private ParticleSystem _ability;
    [Header("[Images]")]
    [SerializeField] private Image _coolDownImage;
    [SerializeField] private Image _abilityImage;
    [SerializeField] private Sprite _cancelSprite;
    [Header("[View GameObject]")]
    [SerializeField] private GameObject _enemyViewGameObject;

    private Camera _playerCamera;

    public Image CoolDownImage => _coolDownImage;
    public Sprite CancelSprite => _cancelSprite;

    private void OnDestroy()
    {
        _enemy.ChangedHealth -= OnChangeHealth;
        _enemy.HitTaking -= OnHitTaking;
        _enemy.EnemyMovement.EnemyDying -= OnEnemyDying;
        _enemy.EnemyAbility.AbilityUsing -= OnUseAbility;
    }

    private void LateUpdate()
    {
        _enemyViewGameObject.transform.LookAt(_playerCamera.transform);
    }

    public void Initialize(EnemyData enemyData)
    {
        _playerCamera = FindObjectOfType<Camera>();
        Fill(enemyData);
        AddListener();
    }

    private void AddListener()
    {
        _enemy.ChangedHealth += OnChangeHealth;
        _enemy.HitTaking += OnHitTaking;
        _enemy.EnemyMovement.EnemyDying += OnEnemyDying;
        _enemy.EnemyAbility.AbilityUsing += OnUseAbility;
    }

    private void Fill(EnemyData enemyData)
    {
        _level.text = _enemy.Level.ToString();
        _health.text = _enemy.Health.ToString();
        SetSliderValue(_enemy.Health);
        _enemyIcon.sprite = enemyData.EnemyIcon;
        _abilityImage.sprite = enemyData.AbilitySprite;
        _cancelSprite = enemyData.CancelAbilitySprite;
    }

    private void SetSliderValue(int value)
    {
        _sliderHP.maxValue = value;
        _sliderHP.value = _sliderHP.maxValue;
    }

    private void OnChangeHealth(int target)
    {
        _sliderHP.value = target;
        _health.text = target.ToString();
    }

    private void OnHitTaking()
    {
        _hit.Play();
    }

    private void OnEnemyDying()
    {
        _dieEffect.Play();
        _hit.gameObject.SetActive(false);
    }

    private void OnUseAbility()
    {
        _ability.Play();
    }
}