using UnityEngine;

public class PlayerArmorEquipment : MonoBehaviour
{
    [Header("[Player Entity]")]
    [SerializeField] private Player _player;
    [Header("[Containers]")]
    [SerializeField] private Transform _armorObjectContainer;

    private EquipmentItemGameObject _armorObject;

    public void BuyArmorItem(EquipmentItemState equipmentItemState)
    {
        equipmentItemState.IsBuyed = true;
    }

    public void EquipArmor(EquipmentItemState equipmentItemState)
    {
        if (equipmentItemState == null)
            return;

        if (_armorObject != null)
            Destroy(_armorObject.gameObject);

        _player.PlayerInventory.EquipItem(equipmentItemState);

        equipmentItemState.IsEquipped = true;
        _armorObject = Instantiate(equipmentItemState.ItemData.ItemGameObject as EquipmentItemGameObject, _armorObjectContainer);
        _player.PlayerView.UpdatePlayerStats();
    }
}
