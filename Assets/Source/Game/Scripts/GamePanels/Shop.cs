using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : GamePanels
{
    [SerializeField] private Button _openButton;
    [SerializeField] private Button _closeButton;
    [SerializeField] private DialogPanel _dialogPanel;
    [SerializeField] private ShopTab[] _shopTabs;

    private List<ItemView> _itemViews = new();
    private List<Weapon> _weapons;
    private List<Armor> _armors;
    private List<Consumables> _consumables;

    public event Action<int, int> PlayerResourceChanged;

    protected DialogPanel DialogPanel => _dialogPanel;

    private void Awake()
    {
        gameObject.SetActive(false);
        AddPanelsListener();
        _openButton.onClick.AddListener(Open);
        _closeButton.onClick.AddListener(Close);
    }

    private void OnDestroy()
    {
        RemovePanelsListener();
        _openButton.onClick.RemoveListener(Open);
        _closeButton.onClick.RemoveListener(Close);
    }

    protected override void Open()
    {
        gameObject.SetActive(true);
        GetPlayerResourceValue();
        GetListItems();
        CloseTabs();
    }

    protected void GetPlayerResourceValue()
    {
        PlayerResourceChanged?.Invoke(Player.Wallet.Coins, Player.PlayerStats.PlayerAbility.Points);
    }

    protected override void Close()
    {
        gameObject.SetActive(false);
        PanelClosed?.Invoke();
        ClearItems();
    }

    private void GetListItems()
    {
        _weapons = Player.PlayerStats.PlayerDamage.GetListWeapon();
        _armors = Player.PlayerStats.PlayerArmor.GetListArmor();
        _consumables = Player.PlayerConsumables.GetListConsumables();
        FillShopTabs();
    }

    private void FillShopTabs()
    {
        foreach (var tab in _shopTabs)
        {
            switch (tab.ItemType)
            {
                case TypeItem.Weapon:
                    CreateListItem(_weapons, tab.ItemView, tab.Container);
                    break;
                case TypeItem.Armor:
                    CreateListItem(_armors, tab.ItemView, tab.Container);
                    break;
                case TypeItem.Consumables:
                    CreateListItem(_consumables, tab.ItemView, tab.Container);
                    break;
            }
        }
    }

    private void CreateListItem<Item>(List<Item> list, ItemView itemView, Transform container)
    {
        for (int i = 0; i < list.Count; i++)
        {
            AddItem(list[i], itemView, container);
        }
    }

    private void AddItem<Item>(Item item, ItemView itemView, Transform container)
    {
        var view = Instantiate(itemView, container);
        view.Initialize(item, Player);
        AddButtonListener(view);
        _itemViews.Add(view);
    }

    private void AddButtonListener(ItemView itemView)
    {
        itemView.BuyButtonClick += OnBuyItem;
        if (itemView is WeaponView) itemView.ChangeCurrentWeapon += OnChangeWeapon;
        if (itemView is ArmorView) itemView.ChangeCurrentArmor += OnChangeArmor;
    }

    private void OnBuyItem(ItemView itemView)
    {
        if (itemView.Item.Price <= Player.Wallet.Coins)
        {
            Player.Wallet.Buy(itemView.Item.Price);
            itemView.Item.Buy();
            GetPlayerResourceValue();
        }
        else DialogPanel.OpenPanel();
    }

    private void OnChangeWeapon(ItemView itemView)
    {
        if (itemView is WeaponView) Player.PlayerStats.PlayerDamage.ChangeCurrentWeapon((itemView as WeaponView).Weapon);
        else return;
    }

    private void OnChangeArmor(ItemView itemView)
    {
        if (itemView is ArmorView) Player.PlayerStats.PlayerArmor.ChangeCurrentArmor((itemView as ArmorView).Armor);
        else return;
    }

    private void ClearItems()
    {
        foreach (var view in _itemViews)
        {
            view.BuyButtonClick -= OnBuyItem;
            view.ChangeCurrentWeapon -= OnChangeWeapon;
            view.ChangeCurrentArmor -= OnChangeArmor;
            Destroy(view.gameObject);
        }

        _itemViews.Clear();
    }

    private void AddPanelsListener()
    {
        foreach (var tab in _shopTabs)
        {
            tab.TabOpened += CloseTabs;
        }
    }

    private void RemovePanelsListener()
    {
        foreach (var tab in _shopTabs)
        {
            tab.TabOpened -= CloseTabs;
        }
    }

    private void CloseTabs()
    {
        foreach (var tab in _shopTabs)
        {
            tab.gameObject.SetActive(false);
        }
    }
}