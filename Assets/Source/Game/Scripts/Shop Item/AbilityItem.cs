using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ability", menuName = "Create Ability", order = 51)]
public class AbilityItem : Item
{
    [SerializeField] private Sprite _abilityShopSprite;
    [SerializeField] private int _upgradePrice;
    [Header("[Level Ability]")]
    [SerializeField] private List<AbilityLevel> _abilityLevels = new();
    [Header("[Current Ability Stats]")]
    [SerializeField] private int _currentLevel = 0;
    [SerializeField] private float _currentDelay = 0;
    [SerializeField] private int _currentAbilityValue = 0;

    //public int Damage => _damage;
   // public int WeaponLevel => _weaponLevel;
}
