using UnityEngine;

namespace Assets.Source.Game.Scripts
{
    public class PlayerEquipment : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private Transform _equipmentObjectContainer;

        private EquipmentItem _equipmentItem;

        public void BuyItem(EquipmentItemState equipmentItemState)
        {
            equipmentItemState.IsBuyed = true;
        }

        public void EquipItem(EquipmentItemState equipmentItemState)
        {
            if (equipmentItemState == null)
                return;

            if (_equipmentItem != null)
                Destroy(_equipmentItem.gameObject);

            _player.PlayerInventory.EquipItem(equipmentItemState);
            _equipmentItem = Instantiate(equipmentItemState.ItemData.Item as EquipmentItem, _equipmentObjectContainer);
        }
    }
}