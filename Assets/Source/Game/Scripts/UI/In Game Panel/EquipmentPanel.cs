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

        if (_weapons != null)
        {
            return;
        }
        else
        {
            Player = player;
            _weapons = _playerEquipment.GetListWeapon();
            _armors = _playerEquipment.GetListArmor();

            for (int i = 1; i < _weapons.Count; i++)
            {
                AddWeapon(_weapons[i]);
            }

            for (int i = 1; i < _armors.Count; i++)
            {
                AddArmor(_armors[i]);
            }
        }
    }
}