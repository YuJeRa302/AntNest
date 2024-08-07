using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Source.Game.Scripts
{
    public class PlayerEquipment : MonoBehaviour
    {
        [SerializeField] private List<ItemSlot> _slotComponents = new ();
        [SerializeField] private Player _player;

        private Dictionary<TypeItem, ItemSlot> _slots = new ();
        private PlayerEquipmentState _playerEquipmentState;

        public void Initialize(PlayerEquipmentState playerEquipmentState)
        {
            _playerEquipmentState = playerEquipmentState;
            Fill();
        }

        public void BuyItem(EquipmentItemState equipmentItemState)
        {
            equipmentItemState.IsBuyed = true;
        }

        public void AddItem(EquipmentItemState equipmentItemState)
        {
            if (equipmentItemState == null)
                return;

            if (_slots[equipmentItemState.ItemData.ItemType].Item != null)
                RemoveItem(equipmentItemState);

            _slots[equipmentItemState.ItemData.ItemType].EquipItem(equipmentItemState);
            equipmentItemState.IsEquipped = true;
            _player.PlayerStats.ChangeEquipment();
        }

        private void Fill()
        {
            foreach (var slot in _slotComponents)
            {
                _slots.Add(slot.TypeItem, slot);
            }
        }

        private void RemoveItem(EquipmentItemState equipmentItemState)
        {
            _slots[equipmentItemState.ItemData.ItemType].RemoveItem();
            var currentEquipment = _playerEquipmentState.Items.FirstOrDefault(item => item.IsEquipped && item.ItemData.ItemType == equipmentItemState.ItemData.ItemType);
            currentEquipment.IsEquipped = false;
        }
    }
}