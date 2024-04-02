using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private const int DefaultItemLevel = 1;

    [SerializeField] private Player _player;
    [SerializeField] private PlayerWeaponEquipment _weaponPlayerEquipment;
    [SerializeField] private PlayerArmorEquipment _armorPlayerEquipment;
    [Header("[Default Equipment Data]")]
    [SerializeField] private PlayerEquipmentState _playerEquipmentState;

    public List<EquipmentItemState> ListWeapon => _playerEquipmentState.Items.Where(item => item.ItemData.ItemType == TypeItem.Weapon).ToList();
    public List<EquipmentItemState> ListArmor => _playerEquipmentState.Items.Where(item => item.ItemData.ItemType == TypeItem.Armor).ToList();

    private void Awake()
    {
        AddDefaultEquipment(ListWeapon);
        AddDefaultEquipment(ListArmor);
    }

    private void AddDefaultEquipment(List<EquipmentItemState> equipmentItemStates)
    {
        foreach (var item in equipmentItemStates)
        {
            if (item.ItemData.Level == DefaultItemLevel)
                AddEquipment(item);
        }
    }

    private void AddEquipment(EquipmentItemState equipmentItemState)
    {
        if (equipmentItemState.ItemData.ItemType == TypeItem.Weapon)
        {
            _weaponPlayerEquipment.BuyWeaponItem(equipmentItemState);
            _weaponPlayerEquipment.EquipWeapon(equipmentItemState);
        }
        else
        {
            _armorPlayerEquipment.BuyArmorItem(equipmentItemState);
            _armorPlayerEquipment.EquipArmor(equipmentItemState);
        }
    }
}

[Serializable]
public struct PlayerEquipmentState
{
    public List<EquipmentItemState> Items;
    public EquipmentItemState EquippedWeapon => Items.FirstOrDefault(item => item.IsEquipped && item.ItemData.ItemType == TypeItem.Weapon);
    public EquipmentItemState EquippedArmor => Items.FirstOrDefault(item => item.IsEquipped && item.ItemData.ItemType == TypeItem.Armor);
}