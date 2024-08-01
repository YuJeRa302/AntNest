namespace Assets.Source.Game.Scripts
{
    public class Healing : AbilityItem
    {
        protected override void Use()
        {
            if (IsUseAbility == false && Player.PlayerStats.PlayerHealth.CurrentHealth < Player.PlayerStats.PlayerHealth.MaxHealth)
            {
                Player.PlayerStats.PlayerHealth.ChangeHealth(CurrentAbilityValue);
                ApplyAbility();
            }
        }
    }
}