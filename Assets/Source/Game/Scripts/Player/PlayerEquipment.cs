using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    [Header("[Transform]")]
    [SerializeField] private Transform _weponsTransform;
    [SerializeField] private Transform _armorsTransform;
    [Header("[Weapon]")]
    [SerializeField] private List<Equipment> _weapon;
    [Header("[Armor]")]
    [SerializeField] private List<Equipment> _armor;
    [Header("[Wallet]")]
    [SerializeField] private Wallet _wallet;
    [Header("[PlayerStats]")]
    [SerializeField] private PlayerStats _playerStats;
    //сделать отдельный скрипт под оружие отдельный под армор
    private Equipment _currentWeapon;
    private Equipment _currentArmor;

    public Equipment CurrentWeapon => _currentWeapon;
    public Equipment CurrentArmor => _currentArmor;
    public int CountWeapon => _weapon.Count;
    public int CountArmor => _armor.Count;

    public void Initialized()
    {
        AddItemToList(_weponsTransform, _armorsTransform, _weapon, _armor);

        _currentWeapon = _weapon[0];
        _currentArmor = _armor[0];
    }

    public void BuyEquipment(Equipment equipment)
    {
        _wallet.Buy(equipment.Price);

        if (equipment is Axe1) _weapon.Add(equipment);
        else _armor.Add(equipment);
    }
    public void ChangeCurrentEquipment(Equipment equipment)
    {
        if (equipment is Axe1) ChangeEquipment(equipment, ref _currentWeapon);
        else ChangeEquipment(equipment, ref _currentArmor);
    }

    private void ChangeEquipment(Equipment newEquipment, ref Equipment currentequipment)
    {
        currentequipment.SetState(false);
        newEquipment.SetState(true);
        currentequipment = newEquipment;
       // _playerStats.UpdatePlayerStats(currentequipment.Value, currentequipment.Value);
    }

    public List<Equipment> GetListWeapon()
    {
        return _weapon;
    }

    public List<Equipment> GetListArmor()
    {
        return _armor;
    }

    public void IncreaseArmor(int armor)
    {
        _currentArmor.Increase(armor);
    }

    public void DecreaseArmor(int armor)
    {
        _currentArmor.Decrease(armor);
    }

    public void IncreaseDamage(int armor)
    {
        _currentWeapon.Increase(armor);
    }

    public void DecreaseDamage(int armor)
    {
        _currentWeapon.Decrease(armor);
    }

    private void AddItem(Transform container, List<Equipment> equipment)
    {
        for (int i = 0; i < container.childCount; i++)
        {
            equipment.Add(container.GetChild(i).GetComponent<Equipment>());
        }
    }

    private void AddItemToList(Transform containerWeapon, Transform containerArmor, List<Equipment> weapon, List<Equipment> armor)
    {
        AddItem(containerWeapon, weapon);
        AddItem(containerArmor, armor);
    }
}