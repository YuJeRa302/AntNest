using System.Collections.Generic;
using System.Linq;

namespace Assets.Source.Game.Scripts
{
    [System.Serializable]
    public struct PlayerEquipmentState
    {
        public List<EquipmentItemState> Items;
        public EquipmentItemState EquippedWeapon => Items.FirstOrDefault(item => item.IsEquipped && item.ItemData.ItemType == TypeItem.Weapon);
        public EquipmentItemState EquippedArmor => Items.FirstOrDefault(item => item.IsEquipped && item.ItemData.ItemType == TypeItem.Armor);
    }
}