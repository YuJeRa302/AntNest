using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private const int DefaultItemLevel = 1;

    [SerializeField] private Player _player;
    [SerializeField] private PlayerEquipment _weaponPlayerEquipment;
    [SerializeField] private PlayerEquipment _armorPlayerEquipment;
    [Header("[Default Equipment Data]")]
    [SerializeField] private PlayerEquipmentState _defaultPlayerEquipmentState;
    [Header("[Default Ability Data]")]
    [SerializeField] private PlayerAbilityState _defaultAbilityState;
    [Header("[Default Consumable Data]")]
    [SerializeField] private PlayerConsumableState _defaultConsumableState;
    [Header("[Default Count Health Potion]")]
    [SerializeField] private int _defaultCountHealthPotion;

    private PlayerEquipmentState _playerEquipmentState;
    private PlayerAbilityState _playerAbilityState;
    private PlayerConsumableState _playerConsumableState;

    public List<EquipmentItemState> ListWeapon => _playerEquipmentState.Items.Where(item => item.ItemData.ItemType == TypeItem.Weapon).ToList();
    public List<EquipmentItemState> ListArmor => _playerEquipmentState.Items.Where(item => item.ItemData.ItemType == TypeItem.Armor).ToList();
    public List<AbilityState> ListAbilities => _playerAbilityState.Items.ToList();
    public List<ConsumableItemState> ListConsumables => _playerConsumableState.Items.ToList();
    public EquipmentItemState CurrentWeapon => _playerEquipmentState.EquippedWeapon;
    public EquipmentItemState CurrentArmor => _playerEquipmentState.EquippedArmor;
    public int DefaultCountHealthPotion => _defaultCountHealthPotion;

    private void Awake()
    {
        LoadDefaultState();
        AddDefaultEquipment(ListWeapon);
        AddDefaultEquipment(ListArmor);
        _player.PlayerView.UpdatePlayerStats();
    }

    public void EquipItem(EquipmentItemState equipmentItemState)
    {
        if (equipmentItemState.ItemData.ItemType == TypeItem.Weapon)
        {
            if (CurrentWeapon != null)
                CurrentWeapon.IsEquipped = false;
        }
        else
        {
            if (CurrentArmor != null)
                CurrentArmor.IsEquipped = false;
        }

        equipmentItemState.IsEquipped = true;
    }

    private void LoadDefaultState()
    {
        _playerEquipmentState = _defaultPlayerEquipmentState;
        _playerAbilityState = _defaultAbilityState;
        _playerConsumableState = _defaultConsumableState;
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
            _weaponPlayerEquipment.BuyItem(equipmentItemState);
            _weaponPlayerEquipment.EquipItem(equipmentItemState);
        }
        else
        {
            _armorPlayerEquipment.BuyItem(equipmentItemState);
            _armorPlayerEquipment.EquipItem(equipmentItemState);
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

[Serializable]
public struct PlayerAbilityState
{
    public List<AbilityState> Items;
}

[Serializable]
public struct PlayerConsumableState
{
    public List<ConsumableItemState> Items;
}