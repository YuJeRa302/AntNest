using UnityEngine;

public class DamageAbility : Ability
{
    [Header("[Hit Count]")]
    [SerializeField] private int _hitCount;

    public override void Use()
    {
        if (IsUseAbility == false)
        {
            Player.PlayerStats.SetDamageAbility(CurrentAbilityValue, _hitCount, Effect);
            ApplyAbility(CurrentDelay);
        }
        else return;
    }
}