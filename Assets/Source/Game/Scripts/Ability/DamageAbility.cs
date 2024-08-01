namespace Assets.Source.Game.Scripts
{
    public class DamageAbility : AbilityItem
    {
        protected override void Use()
        {
            if (IsUseAbility == false)
            {
                Player.PlayerStats.PlayerAbilityCaster.UseAbility(TypeAbility, CurrentDuration, CurrentAbilityValue);
                ApplyAbility();
            }
        }
    }
}