using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    [Header("[Transform]")]
    [SerializeField] private Transform _weponsTransform;
    [SerializeField] private Transform _armorsTransform;
    [Header("[Weapon]")]
    [SerializeField] private List<Weapon> _weapon;
    [Header("[Armor]")]
    [SerializeField] private List<Armor> _armor;
    [Header("[Wallet]")]
    [SerializeField] private Wallet _wallet;
    [Header("[PlayerStats]")]
    [SerializeField] private PlayerStats _playerStats;

    private Weapon _currentWeapon;
    private Armor _currentArmor;

    public Weapon CurrentWeapon => _currentWeapon;
    public Armor CurrentArmor => _currentArmor;
    public int CountWeapon => _weapon.Count;
    public int CountArmor => _armor.Count;

    public void BuyWeapon(Weapon weapon)
    {
        _wallet.Buy(weapon.Price);
        _weapon.Add(weapon);
    }

    public void BuyArmor(Armor armor)
    {
        _wallet.Buy(armor.Price);
        _armor.Add(armor);
    }

    public void ChangeCurrentWeapon(Weapon weapon)
    {
        _currentWeapon.SetState(false);
        weapon.SetState(true);
        _currentWeapon = weapon; 
        _playerStats.UpdatePlayerStats(_currentArmor.ItemArmor, weapon.Damage);
    }

    public void ChangeCurrentArmor(Armor armor)
    {
        _currentArmor.SetState(false);
        armor.SetState(true);
        _currentArmor = armor;
        _playerStats.UpdatePlayerStats(_currentArmor.ItemArmor, _currentWeapon.Damage);
    }

    public List<Weapon> GetListWeapon()
    {
        return _weapon;
    }

    public List<Armor> GetListArmor()
    {
        return _armor;
    }

    public void Initialized()
    {
        AddItemToList();
        _currentWeapon = _weapon[0];
        _currentArmor = _armor[0];
    }

    public void ShowHideEquipment()
    {
        for (int i = 1; i < _weapon.Count; i++)
        {
            _weapon[i].gameObject.SetActive(false);
        }

        for (int i = 1; i < _armor.Count; i++)
        {
            _armor[i].gameObject.SetActive(false);
        }
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

    private void AddItemToList()
    {
        for (int i = 0; i < _weponsTransform.childCount; i++)
        {
            _weapon.Add(_weponsTransform.GetChild(i).GetComponent<Axe>());
        }

        for (int i = 0; i < _armorsTransform.childCount; i++)
        {
            _armor.Add(_armorsTransform.GetChild(i).GetComponent<Helmet>());
        }
    }
}