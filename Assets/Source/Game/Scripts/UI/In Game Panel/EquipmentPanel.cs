using System.Collections.Generic;
using UnityEngine;

public class EquipmentPanel : ShopPanel
{
    [Header("[Views]")]
    [SerializeField] private WeaponView _weaponView;
    [SerializeField] private ArmorView _armorView;
    [Header("[Containers]")]
    [SerializeField] private GameObject _weaponContainer;
    [SerializeField] private GameObject _armorContainer;
    [Header("[Panel]")]
    [SerializeField] private ArmorPanel _armorPanel;
    [SerializeField] private WeaponPanel _weaponPanel;

    private List<Weapon> _weapons;
    private List<Armor> _armors;
    private PlayerEquipment _playerEquipment;

    public ArmorPanel ArmorPanel => _armorPanel;
    public WeaponPanel WeaponPanel => _weaponPanel;

    public void OpenWeaponPanel()
    {
        CloseAllPanels();
        gameObject.SetActive(true);
        _weaponPanel.gameObject.SetActive(true);
    }

    public void OpenArmorPanel()
    {
        CloseAllPanels();
        gameObject.SetActive(true);
        _armorPanel.gameObject.SetActive(true);
    }

    public void AddWeapon(Weapon weapon)
    {
        var view = Instantiate(_weaponView, _weaponContainer.transform);
        view.BuyButtonClick += OnBuyWeapon;
        view.ChangeWeaponButtonClick += OnChangeWeapon;
        view.Render(weapon);
    }

    public void AddArmor(Armor armor)
    {
        var view = Instantiate(_armorView, _armorContainer.transform);
        view.BuyButtonClick += OnBuyArmor;
        view.ChangeArmorButtonClick += OnChangeArmor;
        view.Render(armor);
    }

    public void OnBuyWeapon(Weapon weapon, WeaponView view)
    {
        TryBuyWeapon(weapon, view);
    }

    public void OnBuyArmor(Armor armor, ArmorView view)
    {
        TryBuyArmor(armor, view);
    }

    public void OnChangeArmor(Armor armor)
    {
        _playerEquipment.ChangeCurrentArmor(armor);
    }

    public void OnChangeWeapon(Weapon weapon)
    {
        _playerEquipment.ChangeCurrentWeapon(weapon);
    }

    public void TryBuyWeapon(Weapon weapon, WeaponView view)
    {
        if (weapon.Price <= Player.Coins)
        {
            _playerEquipment.BuyWeapon(weapon);
            weapon.Buy();
            view.BuyButtonClick -= OnBuyWeapon;
            UpdatePlayerStats();
        }
        else DialogPanel.ShowPanel();
    }

    public void TryBuyArmor(Armor armor, ArmorView view)
    {
        if (armor.Price <= Player.Coins)
        {
            _playerEquipment.BuyArmor(armor);
            armor.Buy();
            view.BuyButtonClick -= OnBuyArmor;
            UpdatePlayerStats();
        }
        else DialogPanel.ShowPanel();
    }

    protected override void Filling(Player player)
    {
        _playerEquipment = player.GetComponent<PlayerEquipment>();
        Player = player;

        if (_weapons == null)
        {
            _weapons = _playerEquipment.GetListWeapon();
            _armors = _playerEquipment.GetListArmor();
            AddEquipment(_weapons, _armors);
        }

        TryUnlockBuyButton();
    }

    private void AddEquipment(List<Weapon> weapons, List<Armor> armors)
    {
        for (int i = 1; i < weapons.Count; i++)
        {
            AddWeapon(weapons[i]);
        }

        for (int i = 1; i < armors.Count; i++)
        {
            AddArmor(armors[i]);
        }
    }

    private void TryUnlockBuyButton()
    {
        for (int i = 0; i < _weaponContainer.transform.childCount; i++)
        {
            _weaponContainer.transform.GetChild(i).GetComponent<WeaponView>().TryUnlockBuyButton(Player);
        }

        for (int i = 0; i < _armorContainer.transform.childCount; i++)
        {
            _armorContainer.transform.GetChild(i).GetComponent<ArmorView>().TryUnlockBuyButton(Player);
        }
    }
}