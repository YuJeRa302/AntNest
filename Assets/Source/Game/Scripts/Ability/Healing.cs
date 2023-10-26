public class Healing : Ability
{
    public override void Use()
    {
        if (IsUseAbility == false && Player.PlayerCurrentHealth < Player.PlayerMaxHealth)
        {
            Player.ChangeHealth(CurrentAbilityValue);
            ApplyAbility(CurrentDelay);
        }
        else return;
    }
}