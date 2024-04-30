using UnityEngine;

public class ArmorAbility : AbilityItemGameObject
{
    [Header("[Hit Count]")]
    [SerializeField] private int _hitCount;

    protected override void Use()
    {
        if (IsUseAbility == false)
        {
            Player.PlayerStats.UseAbility(TypeAbility, CurrentDuration, CurrentAbilityValue);
            ApplyAbility(CurrentDelay);
        }
        else return;
    }
}