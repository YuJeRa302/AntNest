using UnityEngine;

public class ArmorAbility : Ability
{
    [SerializeField] private int _armor = 4;

    private void Start()
    {
        SetAbilityValue(_armor);
    }
}