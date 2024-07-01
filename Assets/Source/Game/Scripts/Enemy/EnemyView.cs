using UnityEngine;
using UnityEngine.UI;

public class EnemyView : MonoBehaviour
{
    [Header("[Canvas]")]
    [SerializeField] private Canvas _enemyCanvas;
    [Header("[Sliders]")]
    [SerializeField] private Slider _sliderHP;
    [Header("[Enemy Stats]")]
    [SerializeField] private Enemy _enemy;
    [SerializeField] private Text _level;
    [SerializeField] private Text _health;
    [SerializeField] private Image _enemyIcon;
    [Header("[Images]")]
    [SerializeField] private Image _coolDownImage;
    [SerializeField] private Image _abilityImage;
    [SerializeField] private Sprite _cancelSprite;
    [Header("[View GameObject]")]
    [SerializeField] private GameObject _enemyViewGameObject;

    private ParticleSystem _hit;
    private ParticleSystem _dieEffect;
    private ParticleSystem _ability;
    private PlayerCamera _playerUICamera;

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
        _enemyViewGameObject.transform.LookAt(_playerUICamera.transform);
    }

    public void Initialize(EnemyData enemyData, ParticleSystem particleDie, ParticleSystem particleHit, ParticleSystem particleAbility, PlayerCamera playerCamera)
    {
        _hit = particleHit;
        _dieEffect = particleDie;
        _ability = particleAbility;
        _playerUICamera = playerCamera;
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