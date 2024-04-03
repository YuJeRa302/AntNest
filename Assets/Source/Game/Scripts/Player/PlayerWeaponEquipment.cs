using UnityEngine;

public class PlayerWeaponEquipment : MonoBehaviour
{
    [Header("[Player Entity]")]
    [SerializeField] private Player _player;
    [Header("[Containers]")]
    [SerializeField] private Transform _weaponObjectContainer;

    private EquipmentItemGameObject _weaponObject;

    public void BuyWeaponItem(EquipmentItemState equipmentItemState)
    {
        equipmentItemState.IsBuyed = true;
    }

    public void EquipWeapon(EquipmentItemState equipmentItemState)
    {
        if (equipmentItemState == null)
            return;

        if (_weaponObject != null)
            Destroy(_weaponObject.gameObject);

        _player.PlayerInventory.EquipItem(equipmentItemState);
        _weaponObject = Instantiate(equipmentItemState.ItemData.ItemGameObject as EquipmentItemGameObject, _weaponObjectContainer);
    }
}
