using UnityEngine;

public class ArmorAbility : Ability
{
    [Header("[Hit Count]")]
    [SerializeField] private int _hitCount;

    public override void Use()
    {
        if (IsUseAbility == false)
        {
            Player.PlayerStats.SetArmorAbility(CurrentAbilityValue, _hitCount, Effect);
            ApplyAbility(CurrentDelay);
        }
        else return;
    }
}