using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Game.Scripts
{
    public class AbilityItem : Item
    {
        private readonly int _minValue = 0;

        [SerializeField] private Button _useAbilityButton;

        protected Player Player;
        protected bool IsUseAbility = true;
        protected float CurrentDuration;
        protected int CurrentAbilityValue;
        protected TypeAbility TypeAbility;

        private float _defaultDelay;
        private float _currentDelay;
        private Image _reloadingImage;
        private AbilityItemData _abilityItemData;
        private ParticleSystem _particleSystem;
        private Coroutine _coolDown;

        private void Awake()
        {
            _useAbilityButton.onClick.AddListener(Use);
        }

        private void OnEnable()
        {
            _coolDown = StartCoroutine(Delay());
        }

        private void OnDisable()
        {
            if (_coolDown != null)
                StopCoroutine(_coolDown);
        }

        private void OnDestroy()
        {
            _useAbilityButton.onClick.RemoveListener(Use);
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Q))
                Use();
        }

        public void Initialize(Player player, AbilityState abilityState, Image reloadingImage, ParticleSystem particleSystem)
        {
            Player = player;
            _defaultDelay = abilityState.AbilityData.AbilityLevels[abilityState.CurrentLevel].Delay;
            CurrentAbilityValue = abilityState.AbilityData.AbilityLevels[abilityState.CurrentLevel].AbilityValue;
            TypeAbility = abilityState.AbilityData.TypeAbility;
            CurrentDuration = abilityState.AbilityData.AbilityDuration;
            _abilityItemData = abilityState.AbilityData;
            _particleSystem = particleSystem;
            _reloadingImage = reloadingImage;
        }

        protected virtual void Use() { }

        protected void ApplyAbility()
        {
            _currentDelay = _defaultDelay;
            _particleSystem.Play();
            Player.PlayerSounds.AudioPlayerAbility.PlayOneShot(_abilityItemData.Sound);
            UpdateAbility(true, _currentDelay);
            ResumeCooldown();
        }

        private void ResumeCooldown()
        {
            if (gameObject.activeSelf == true)
            {
                if (_coolDown != null)
                    StopCoroutine(_coolDown);

                _coolDown = StartCoroutine(Delay());
            }
        }

        private IEnumerator Delay()
        {
            while (_currentDelay > _minValue)
            {
                _currentDelay -= Time.deltaTime;
                _reloadingImage.fillAmount = _currentDelay / _defaultDelay;
                yield return null;
            }

            UpdateAbility(false, _minValue);
        }

        private void UpdateAbility(bool state, float delay)
        {
            IsUseAbility = state;
            _reloadingImage.fillAmount = delay;
        }
    }
}