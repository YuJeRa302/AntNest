using System;
using UnityEngine;

public class GrenadeButtonGameObject : ConsumableButtonGameObject
{
    [Obsolete]
    protected override void Use()
    {
        if (CountConsumableItem > MinValue && IsUseConsumable == false)
        {
            CountConsumableItem--;
            ItemGameObject = Instantiate(ConsumableItemData.ItemGameObject, new Vector3(PlacementPoint.position.x,
                PlacementPoint.position.y, PlacementPoint.position.z), Quaternion.identity);
            (ItemGameObject as GrenadeGameObject).Initialize(ConsumableItemData);
            (ItemGameObject as GrenadeGameObject).Rigidbody.AddForce(transform.forward * (ItemGameObject as GrenadeGameObject).ThrowForce,
                ForceMode.VelocityChange);
            ApplyConsumable(ButtonDelay);
        }
        else
            return;
    }
}
