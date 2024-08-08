using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Game.Scripts
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private Slider _sliderHP;
        [SerializeField] private Slider _sliderXP;
        [SerializeField] private Player _player;
        [SerializeField] private Text _level;
        [SerializeField] private Text _countExperienceUpdate;
        [SerializeField] private Text _countGoldUpdate;
        [SerializeField] private Animator _experienceUpdateAnimator;
        [SerializeField] private Animator _goldUpdateAnimator;
        [SerializeField] private Text _playerDamage;
        [SerializeField] private Text _playerArmor;
        [SerializeField] private Transform _abilityObjectContainer;
        [SerializeField] private Transform _playerEffectsContainer;
        [SerializeField] private Transform _weaponEffectsContainer;
        [SerializeField] private Image _defaultImage;
        [SerializeField] private Image _reloadingImage;

        private ParticleSystem _abilityEffect;
        private AbilityItem _abilityItem;

        private enum TransitionParametr
        {
            Take
        }

        public ParticleSystem AbilityEffect => _abilityEffect;
        public AbilityItem AbilityItem => _abilityItem;

        private void Awake()
        {
            _player.PlayerStats.PlayerHealth.ChangedHealth += OnChangeHealth;
            _player.PlayerStats.GoldValueChanged += OnChangeGold;
            _player.PlayerStats.ExperienceValueChanged += OnChangeExperience;
            _player.PlayerStats.PlayerAbilityCaster.AbilityBuyed += OnBuyability;
        }

        private void OnDestroy()
        {
            _player.PlayerStats.PlayerHealth.ChangedHealth -= OnChangeHealth;
            _player.PlayerStats.GoldValueChanged -= OnChangeGold;
            _player.PlayerStats.ExperienceValueChanged -= OnChangeExperience;
            _player.PlayerStats.PlayerAbilityCaster.AbilityBuyed -= OnBuyability;
        }

        public void SetNewLevelValue(int value)
        {
            _level.text = value.ToString();
        }

        public void SetExperienceSliderValue(int value, int difference)
        {
            _sliderXP.maxValue = value;
            _sliderXP.value = difference;
        }

        public void SetDefaultParameters(int maxValueSlider, int experience)
        {
            _sliderHP.maxValue = _player.PlayerStats.PlayerHealth.MaxHealth;
            _sliderHP.value = _player.PlayerStats.PlayerHealth.MaxHealth;
            _sliderXP.maxValue = maxValueSlider;
            _sliderXP.value = experience;
        }

        public void UpdatePlayerStats()
        {
            _playerDamage.text = _player.PlayerStats.Damage.ToString();
            _playerArmor.text = _player.PlayerStats.Armor.ToString();
        }

        private void OnBuyability(AbilityState abilityState)
        {
            if (_defaultImage != null)
                Destroy(_defaultImage);

            if (_abilityItem != null)
                Destroy(_abilityItem.gameObject);

            if (_abilityEffect != null)
                Destroy(_abilityEffect.gameObject);

            if (abilityState.AbilityData.EffectType == TypeEffect.Weapon)
                _abilityEffect = Instantiate(abilityState.AbilityData.ParticleSystem, _weaponEffectsContainer);
            else
                _abilityEffect = Instantiate(abilityState.AbilityData.ParticleSystem, _playerEffectsContainer);

            _abilityItem = Instantiate(abilityState.AbilityData.AbilityItem, _abilityObjectContainer);
            _abilityItem.Initialize(_player, abilityState, _reloadingImage, _abilityEffect);
        }

        private void OnChangeExperience(int target)
        {
            SetTextStats(_countExperienceUpdate, target.ToString(), _experienceUpdateAnimator);
            _sliderXP.value += target;
        }

        private void OnChangeGold(int target)
        {
            SetTextStats(_countGoldUpdate, target.ToString(), _goldUpdateAnimator);
        }

        private void OnChangeHealth(int target)
        {
            _sliderHP.value = target;
        }

        private void SetTextStats(Text template, string text, Animator animator)
        {
            animator.SetTrigger(TransitionParametr.Take.ToString());
            template.text = "+" + text;
        }
    }
}