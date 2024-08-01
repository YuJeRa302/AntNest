using System.Collections.Generic;
using UnityEngine;

namespace Assets.Source.Game.Scripts
{
    [CreateAssetMenu(fileName = "New Ability Item", menuName = "Create Ability Item", order = 51)]
    public class AbilityItemData : ItemData
    {
        private readonly string _maxLevel = "MAX";

        [Header("[Current Ability Stats]")]
        [SerializeField] private int _currentLevel = 0;
        [SerializeField] private string _description;
        [SerializeField] private int _upgradePrice;
        [SerializeField] private float _abilityDuration;
        [Header("[Level Ability]")]
        [SerializeField] private List<AbilityParameters> _abilityParameters = new ();
        [SerializeField] private Sprite _shopSprite;
        [SerializeField] private AudioClip _sound;
        [SerializeField] private AbilityItem _abilityItem;
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private TypeEffect _typeEffect;
        [SerializeField] private TypeAbility _typeAbility;

        public TypeAbility TypeAbility => _typeAbility;
        public TypeEffect EffectType => _typeEffect;
        public int UpgradePrice => _upgradePrice;
        public string Description => _description;
        public float AbilityDuration => _abilityDuration;
        public List<AbilityParameters> AbilityLevels => _abilityParameters;
        public float CurrentDelay => _abilityParameters[_currentLevel].Delay;
        public int CurrentAbilityValue => _abilityParameters[_currentLevel].AbilityValue;
        public Sprite ShopSprite => _shopSprite;
        public AbilityItem AbilityItem => _abilityItem;
        public ParticleSystem ParticleSystem => _particleSystem;
        public AudioClip Sound => _sound;

        public void GetNextLevelAbilityValue(int currentLevel, out string delay, out string abilityValue)
        {
            var nextAbilityLevel = ++currentLevel;

            if (nextAbilityLevel < _abilityParameters.Count)
            {
                delay = _abilityParameters[nextAbilityLevel].Delay.ToString();
                abilityValue = _abilityParameters[nextAbilityLevel].AbilityValue.ToString();
            }
            else
            {
                delay = _maxLevel;
                abilityValue = _maxLevel;
            }
        }
    }
}