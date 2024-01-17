using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmorPanel : Shop
{
    [SerializeField] private ArmorView _armorView;
    [SerializeField] private GameObject _armorContainer;
    [SerializeField] private Button _button;

    private List<Armor> _armors;
    private List<ArmorView> _armorViews;

    private void OnEnable()
    {
        _button.onClick.AddListener(OpenPanel);
        LevelObserver.GameClosed += OnCloseGame;
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OpenPanel);
        LevelObserver.GameClosed -= OnCloseGame;
    }

    protected override void Initialized()
    {
        if (_armors == null)
        {
            _armors = Player.PlayerStats.PlayerArmor.GetListArmor();
            AddEquipment(_armors);
        }

        TryUnlockBuyButton();
    }

    private void AddArmor(Armor armor)
    {
        var view = Instantiate(_armorView, _armorContainer.transform);
        view.BuyButtonClick += OnBuyArmor;
        view.ChangeArmorButtonClick += OnChangeArmor;
        view.Render(armor);
        _armorViews.Add(view);
    }

    private void OnBuyArmor(Armor armor, ArmorView view)
    {
        TryBuyArmor(armor, view);
    }

    private void OnChangeArmor(Armor armor)
    {
        Player.PlayerStats.PlayerArmor.ChangeCurrentArmor(armor);
    }

    private void TryBuyArmor(Armor armor, ArmorView view)
    {
        if (armor.Price <= Player.Wallet.GetCoins())
        {
            Player.PlayerStats.PlayerArmor.BuyArmor(armor);
            armor.Buy();
            view.BuyButtonClick -= OnBuyArmor;
            UpdatePlayerStats();
        }
        else DialogPanel.Opened?.Invoke();
    }

    private void AddEquipment(List<Armor> armors)
    {
        for (int i = 1; i < armors.Count; i++)
        {
            AddArmor(armors[i]);
        }
    }

    private void TryUnlockBuyButton()
    {
        foreach (var view in _armorViews)
        {
            view.TryUnlockBuyButton(Player);
        }
        //for (int i = 0; i < _armorContainer.transform.childCount; i++)
        //{
        //    _armorContainer.transform.GetChild(i).GetComponent<ArmorView>().TryUnlockBuyButton(Player);
        //}
    }

    private void OnCloseGame()
    {
        if (_armorViews.Count > 0)
        {
            foreach (var view in _armorViews)
            {
                view.BuyButtonClick -= OnBuyArmor;
                view.ChangeArmorButtonClick -= OnChangeArmor;
            }
        }
    }
}