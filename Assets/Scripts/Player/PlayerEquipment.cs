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
        _currentWeapon.gameObject.SetActive(false);

        if (_currentWeapon.Damage < weapon.Damage)
        {
            _currentWeapon = weapon;
        }
        else
        {
            return;
        }
    }

    public void BuyArmor(Armor armor)
    {
        _wallet.Buy(armor.Price);
        _armor.Add(armor);
        _currentArmor.gameObject.SetActive(false);

        if (_currentArmor.ItemArmor < armor.ItemArmor)
        {
            _currentArmor = armor;
        }
        else
        {
            return;
        }
    }

    public List<Weapon> GetListWeapon()
    {
        return _weapon;
    }

    public List<Armor> GetListArmor()
    {
        return _armor;
    }

    private void Start()
    {
        AddItemToList();
        _currentWeapon = _weapon[0];
        _currentArmor = _armor[0];
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