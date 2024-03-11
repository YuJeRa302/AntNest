using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ability", menuName = "Create Ability", order = 51)]
public class AbilityItem : ItemData
{
    [Header("[Current Ability Stats]")]
    [SerializeField] private int _currentLevel = 0;
    [SerializeField] private float _currentDelay = 0;
    [SerializeField] private int _currentAbilityValue = 0;
    [Header("[Level Ability]")]
    [SerializeField] private List<AbilityLevel> _abilityLevels = new();
    [SerializeField] private string _description;
    [SerializeField] private Sprite _shopSprite;
    [SerializeField] private AudioClip _sound;
    [SerializeField] private int _upgradePrice;

    public int UpgradePrice => _upgradePrice;
    public string Description => _description;
    public Sprite ShopSprite => _shopSprite;
    public List<AbilityLevel> AbilityLevels => _abilityLevels;
    public int CurrentLevel => _currentLevel;
    public float CurrentDelay => _currentDelay;
    public int CurrentAbilityValue => _currentAbilityValue;
    public AudioClip Sound => _sound;
}
