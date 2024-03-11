using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    [Header("[Container]")]
    [SerializeField] private Transform _weponsTransform;
    [SerializeField] private Transform _armorsTransform;
    [Header("[List Equipment]")]
    [SerializeField] private List<Equipment> _weapon;
    [SerializeField] private List<Equipment> _armor;
    [Header("[Player Entities]")]
    [SerializeField] private Player _player;
    [Header("[Equipment Data]")]
    [SerializeField] private List<ItemData> _itemDatas;

    private Equipment _currentWeapon;
    private Equipment _currentArmor;

    public Equipment CurrentWeapon => _currentWeapon;
    public Equipment CurrentArmor => _currentArmor;

    public void Initialize()
    {
        foreach (var item in _itemDatas)
        {
            AddEquipment((item as EquipmentItem).Template as Equipment);
        }

        _weapon[0].gameObject.SetActive(true);
        _currentWeapon = _weapon[0];
    }

    public void AddEquipment(Equipment equipment)
    {
        if (equipment is Weapon) CreateEquipment(equipment, _weponsTransform, _weapon);
        else CreateEquipment(equipment, _armorsTransform, _armor);
    }

    public void ChangeCurrentEquipment(Equipment equipment)
    {
        if (equipment is Weapon) ChangeEquipment(equipment, ref _currentWeapon);
        else ChangeEquipment(equipment, ref _currentArmor);
    }

    public List<Equipment> GetListWeapon()
    {
        return _weapon;
    }

    public List<Equipment> GetListArmor()
    {
        return _armor;
    }

    private void ChangeEquipment(Equipment newEquipment, ref Equipment currentEquipment)
    {
        currentEquipment.SetState(false);
        newEquipment.SetState(true);
        currentEquipment = newEquipment;
        _player.PlayerView.UpdatePlayerStats();
    }

    private void CreateEquipment(Equipment equipment, Transform transform, List<Equipment> listEquipment)
    {
        Equipment newEquipment = Instantiate(equipment, transform);
        newEquipment.gameObject.SetActive(false);
        listEquipment.Add(newEquipment);
    }
}