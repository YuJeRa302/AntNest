using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponPanel : Shop
{
    [SerializeField] private WeaponView _weaponView;
    [SerializeField] private GameObject _weaponContainer;
    [SerializeField] private Button _button;

    private List<Weapon> _weapons;
    private List<WeaponView> _weaponViews;

    private void OnEnable()
    {
        _button.onClick.AddListener(OpenShopPanel);
        LevelObserver.GameClosed += OnCloseGame;
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OpenShopPanel);
        LevelObserver.GameClosed -= OnCloseGame;
    }

    protected override void Initialized()
    {
        if (_weapons == null)
        {
            _weapons = Player.PlayerStats.PlayerDamage.GetListWeapon();
            AddEquipment(_weapons);
        }

        TryUnlockBuyButton();
    }

    private void OnBuyWeapon(Weapon weapon, WeaponView weaponView)
    {
        TryBuyWeapon(weapon, weaponView);
    }

    private void OnChangeWeapon(Weapon weapon)
    {
        Player.PlayerStats.PlayerDamage.ChangeCurrentWeapon(weapon);
    }

    private void TryBuyWeapon(Weapon weapon, WeaponView weaponView)
    {
        if (weapon.Price <= Player.Wallet.GetCoins())
        {
            Player.PlayerStats.PlayerDamage.BuyWeapon(weapon);
            weapon.Buy();
            weaponView.BuyButtonClick -= OnBuyWeapon;
            UpdatePlayerStats();
        }
        else DialogPanel.Opened?.Invoke();
    }

    private void AddWeapon(Weapon weapon)
    {
        var view = Instantiate(_weaponView, _weaponContainer.transform);
        view.BuyButtonClick += OnBuyWeapon;
        view.ChangeWeaponButtonClick += OnChangeWeapon;
        view.Render(weapon);
        _weaponViews.Add(view);
    }

    private void AddEquipment(List<Weapon> weapons)
    {
        for (int i = 1; i < weapons.Count; i++)
        {
            AddWeapon(weapons[i]);
        }
    }

    private void TryUnlockBuyButton()
    {
        foreach (var view in _weaponViews)
        {
            view.TryUnlockBuyButton(Player);
        }
        //for (int i = 0; i < _weaponContainer.transform.childCount; i++)
        //{
        //    _weaponContainer.transform.GetChild(i).GetComponent<WeaponView>().TryUnlockBuyButton(Player);
        //}
    }

    private void OnCloseGame()
    {
        if (_weaponViews.Count > 0)
        {
            foreach (var view in _weaponViews)
            {
                view.BuyButtonClick -= OnBuyWeapon;
                view.ChangeWeaponButtonClick -= OnChangeWeapon;
            }
        }
    }
}