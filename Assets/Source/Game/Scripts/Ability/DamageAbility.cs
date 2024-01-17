using UnityEngine;

public class DamageAbility : Ability
{
    [Header("[Hit Count]")]
    [SerializeField] private int _hitCount;

    protected override void Use()
    {
        if (IsUseAbility == false)
        {
            Player.PlayerStats.PlayerDamage.Increase(CurrentAbilityValue, _hitCount, Effect);
            ApplyAbility(CurrentDelay);
        }
        else return;
    }
}