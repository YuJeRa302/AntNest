using System;
using UnityEngine;

public class MineButtonGameObject : ConsumableButtonGameObject
{
    [Obsolete]
    protected override void Use()
    {
        if (CountConsumableItem > MinValue && IsUseConsumable == false)
        {
            CountConsumableItem--;
            ItemGameObject = Instantiate(ConsumableItemData.ItemGameObject, new Vector3(PlacementPoint.transform.localPosition.x,
                MinValue, PlacementPoint.transform.localPosition.z), Quaternion.identity);
            (ItemGameObject as FieldMineGameObject).Initialize(ConsumableItemData);
            ApplyConsumable(ButtonDelay);
        }
        else
            return;
    }
}
