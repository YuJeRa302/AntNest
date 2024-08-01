using UnityEngine;
namespace Assets.Source.Game.Scripts
{
    public class GrenadeButton : ConsumableButton
    {
        //    protected override void Use()
        //    {
        //        if (CountConsumableItem > MinValue && IsUseConsumable == false)
        //        {
        //            ItemGameObject = Instantiate(ConsumableItemData.ItemGameObject,
        //                new Vector3(PlacementPoint.position.x, PlacementPoint.position.y, PlacementPoint.position.z),
        //                Quaternion.identity);
        //            (ItemGameObject as Grenade).Initialize(ConsumableItemData);
        //            (ItemGameObject as Grenade).Rigidbody.AddForce(PlacementPoint.forward * (ItemGameObject as Grenade).ThrowForce, ForceMode.VelocityChange);
        //            ApplyConsumable();
        //        }
        //    }
    }
}