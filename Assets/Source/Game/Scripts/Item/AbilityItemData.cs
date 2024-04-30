using System;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] private List<AbilityLevel> _abilityLevels = new();
    [SerializeField] private Sprite _shopSprite;
    [SerializeField] private AudioClip _sound;
    [SerializeField] private AbilityItemGameObject _itemGameObject;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private TypeEffect _typeEffect;
    [SerializeField] private TypeAbility _typeAbility;

    public TypeAbility TypeAbility => _typeAbility;
    public TypeEffect EffectType => _typeEffect;
    public int UpgradePrice => _upgradePrice;
    public string Description => _description;
    public int CurrentLevel => _currentLevel;
    public float AbilityDuration => _abilityDuration;
    public List<AbilityLevel> AbilityLevels => _abilityLevels;
    public float CurrentDelay => _abilityLevels[_currentLevel].Delay;
    public int CurrentAbilityValue => _abilityLevels[_currentLevel].AbilityValue;
    public Sprite ShopSprite => _shopSprite;
    public AbilityItemGameObject ItemGameObject => _itemGameObject;
    public ParticleSystem ParticleSystem => _particleSystem;
    public AudioClip Sound => _sound;

    public void GetNextLevelAbilityValue(int currentLevel, out string delay, out string abilityValue)
    {
        var nextAbilityLevel = ++currentLevel;

        if (nextAbilityLevel < _abilityLevels.Count)
        {
            delay = _abilityLevels[nextAbilityLevel].Delay.ToString();
            abilityValue = _abilityLevels[nextAbilityLevel].AbilityValue.ToString();

        }
        else
        {
            delay = _maxLevel;
            abilityValue = _maxLevel;
        }
    }
}

[Serializable]
public class AbilityItemDataState
{
    public AbilityItemData AbilityData;
}

[Serializable]
public class AbilityState : AbilityItemDataState
{
    public bool IsBuyed;
    public bool IsLocked;
    public int CurrentLevel;
}