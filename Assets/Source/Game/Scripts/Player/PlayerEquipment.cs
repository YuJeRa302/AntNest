using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    private const string SaveKey = "PlayerEquipment";
    
    [Header("[Player Entity]")]
    [SerializeField] private Player _player;
    [Header("[Containers]")]
    [SerializeField] private Transform _weaponObjectContainer;
    [SerializeField] private Transform _armorObjectContainer;
    [Header("[Default Equipment Data]")] 
    [SerializeField] private PlayerEquipmentState _defaultPlayerEquipmentState;

    private PlayerEquipmentState _playerEquipmentState;
    private EquipmentItemGameObject _weaponObject;
    private EquipmentItemGameObject _armorObject;
    
    public event Action EquipmentChanged;
    
    public PlayerEquipmentState PlayerEquipmentState => _playerEquipmentState;
    public EquipmentItemState CurrentWeapon => _playerEquipmentState.EquippedWeapon;
    public EquipmentItemState CurrentArmor => _playerEquipmentState.EquippedArmor;

    public List<EquipmentItemState> ListWeapon => _playerEquipmentState.Items.Where(item => item.ItemData.ItemType == TypeItem.Weapon).ToList();

    public List<EquipmentItemState> ListArmor => _playerEquipmentState.Items.Where(item => item.ItemData.ItemType == TypeItem.Armor).ToList();

    private void Awake()
    {
        LoadPlayerEquipmentState();
    }

    public void Initialize()
    {
        LoadPlayerEquipmentState();
    }

    public void BuyEquipmentItem(EquipmentItemState equipmentItemState)
    {
        equipmentItemState.IsBuyed = true;
    }

    public void EquipWeapon(EquipmentItemState equipmentItemState)
    {
        if (equipmentItemState == null) 
            return;
        
        if (_weaponObject != null)
            Destroy(_weaponObject.gameObject);
        
        if (CurrentWeapon != null)
            CurrentWeapon.IsEquipped = false;
        
        equipmentItemState.IsEquipped = true;
        _weaponObject = Instantiate(equipmentItemState.ItemData.ItemGameObject as EquipmentItemGameObject, _weaponObjectContainer);
        _player.PlayerView.UpdatePlayerStats();
        SavePlayerEquipmentState();
    }

    public void EquipArmor(EquipmentItemState equipmentItemState)
    {
        if (equipmentItemState == null) 
            return;
        
        if (_armorObject != null)
            Destroy(_armorObject.gameObject);
        
        if (CurrentArmor != null)
            CurrentArmor.IsEquipped = false;
        
        equipmentItemState.IsEquipped = true;
        _armorObject = Instantiate(equipmentItemState.ItemData.ItemGameObject as EquipmentItemGameObject, _armorObjectContainer);
        _player.PlayerView.UpdatePlayerStats();
        SavePlayerEquipmentState();
    }

    private void LoadPlayerEquipmentState()
    {
        if (PlayerPrefs.HasKey(SaveKey))
        {
            _playerEquipmentState = JsonUtility.FromJson<PlayerEquipmentState>(PlayerPrefs.GetString(SaveKey));
        }
        else
        {
            _playerEquipmentState = _defaultPlayerEquipmentState;
            PlayerPrefs.SetString(SaveKey, JsonUtility.ToJson(_playerEquipmentState));
        }

        EquipWeapon(_playerEquipmentState.EquippedWeapon);
        EquipArmor(_playerEquipmentState.EquippedArmor);
    }

    private void SavePlayerEquipmentState()
    {
        PlayerPrefs.SetString(SaveKey, JsonUtility.ToJson(_playerEquipmentState));
    }
}

[Serializable]
public struct PlayerEquipmentState
{
    public List<EquipmentItemState> Items;
    public EquipmentItemState EquippedWeapon => Items.FirstOrDefault(item => item.IsEquipped && item.ItemData.ItemType == TypeItem.Weapon);
    public EquipmentItemState EquippedArmor => Items.FirstOrDefault(item => item.IsEquipped && item.ItemData.ItemType == TypeItem.Armor);
}