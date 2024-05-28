using System;

public class PotionButtonGameObject : ConsumableButtonGameObject
{
    [Obsolete]
    protected override void Use()
    {
        if (CountConsumableItem > MinValue &&
            Player.PlayerStats.PlayerHealth.CurrentHealth != Player.PlayerStats.PlayerHealth.MaxHealth
            && IsUseConsumable == false)
        {
            Player.PlayerStats.PlayerHealth.ChangeHealth(ConsumableItemData.Value);
            ApplyConsumable();
        }
        else
            return;
    }
}
