using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    [Header("[Player Entity]")]
    [SerializeField] private Player _player;
    [Header("[Containers]")]
    [SerializeField] private Transform _equipmentObjectContainer;

    private EquipmentItemGameObject _equipmentObject;

    public void BuyItem(EquipmentItemState equipmentItemState)
    {
        equipmentItemState.IsBuyed = true;
    }

    public void EquipItem(EquipmentItemState equipmentItemState)
    {
        if (equipmentItemState == null)
            return;

        if (_equipmentObject != null)
            Destroy(_equipmentObject.gameObject);

        _player.PlayerInventory.EquipItem(equipmentItemState);
        _equipmentObject = Instantiate(equipmentItemState.ItemData.ItemGameObject as EquipmentItemGameObject, _equipmentObjectContainer);
    }
}