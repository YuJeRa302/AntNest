using UnityEngine;

public class DamageAbility : AbilityItemGameObject
{
    [Header("[Hit Count]")]
    [SerializeField] private int _hitCount;

    protected override void Use()
    {
        if (IsUseAbility == false)
        {
            Player.PlayerStats.PlayerDamage.Increase(CurrentAbilityValue, _hitCount);
            ApplyAbility(CurrentDelay);
        }
        else return;
    }
}