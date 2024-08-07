using System;
using UnityEngine;

namespace Assets.Source.Game.Scripts
{
    [Serializable]
    public class ItemSlot : MonoBehaviour
    {
        [SerializeField] private TypeItem _itemType;
        [SerializeField] private Transform _itemObjectContainer;

        private Item _item;

        public TypeItem TypeItem => _itemType;
        public Item Item => _item;

        public void EquipItem(EquipmentItemState equipmentItemState)
        {
            _item = Instantiate(equipmentItemState.ItemData.Item as EquipmentItem, _itemObjectContainer);
        }

        public void RemoveItem()
        {
            if (_item != null)
                Destroy(_item.gameObject);
        }
    }
}