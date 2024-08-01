using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Game.Scripts
{
    public class EnemyHealthBar : MonoBehaviour
    {
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
        [SerializeField] private GameObject _enemyViewGameObject;

        private PlayerCamera _playerUICamera;

        public Image CoolDownImage => _coolDownImage;
        public Sprite CancelSprite => _cancelSprite;

        private void OnDestroy()
        {
            _enemy.HealthChanged -= OnChangeHealth;
        }

        private void LateUpdate()
        {
            _enemyViewGameObject.transform.LookAt(_playerUICamera.transform);
        }

        public void Initialize(EnemyData enemyData, PlayerCamera playerCamera)
        {
            _enemy.HealthChanged += OnChangeHealth;
            _playerUICamera = playerCamera;
            Fill(enemyData);
        }

        private void Fill(EnemyData enemyData)
        {
            _level.text = enemyData.Level.ToString();
            _health.text = enemyData.Health.ToString();
            SetSliderValue(enemyData.Health);
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
    }
}