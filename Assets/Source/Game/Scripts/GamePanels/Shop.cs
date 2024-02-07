using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : GamePanels
{
    [SerializeField] private Button _openPanel;
    [SerializeField] private Button _closePanel;
    [SerializeField] private DialogPanel _dialogPanel;
    [SerializeField] private ShopTab[] _shopTabs;

    private List<ItemView> _itemViews = new();
    private List<Weapon> _weapons;

    public event Action<int, int> PlayerResourceUpdated;

    protected DialogPanel DialogPanel => _dialogPanel;

    private void Awake()
    {
        gameObject.SetActive(false);
        AddPanelsListener();
        _openPanel.onClick.AddListener(OpenPanel);
        _closePanel.onClick.AddListener(ClosePanel);
    }

    private void OnDestroy()
    {
        RemovePanelsListener();
        _openPanel.onClick.RemoveListener(OpenPanel);
        _closePanel.onClick.RemoveListener(ClosePanel);
    }

    protected virtual void FillPanel() { }

    protected override void OpenPanel()
    {
        gameObject.SetActive(true);
        UpdatePlayerResource();
        GetItemData();
        CloseAllPanels();
    }

    protected void UpdatePlayerResource()
    {
        PlayerResourceUpdated?.Invoke(Player.Wallet.Coins, Player.PlayerStats.PlayerAbility.Points);
    }

    private void GetItemData()
    {
        _weapons = Player.PlayerStats.PlayerDamage.GetListWeapon();
        SearchItemContainer();
    }

    private void SearchItemContainer()
    {
        foreach (var panel in _shopTabs)
        {
            switch (panel.ItemType)
            {
                case TypeItem.Weapon:
                    CreateListItem(_weapons, panel.ItemView, panel.Container);
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
        SetButtonListener(itemView);
        _itemViews.Add(view);
    }

    private void SetButtonListener(ItemView itemView)
    {
        itemView.BuyButtonClick += OnBuyItem;
        itemView.ChangeItemButtonClick += OnChangeWeapon;
        itemView.ChangeItemButtonClick += OnChangeArmor;
    }

    private void OnBuyItem(ItemView itemView)
    {
        if (itemView.Item.Price <= Player.Wallet.Coins)
        {
            Player.Wallet.Buy(itemView.Item.Price);
            itemView.Item.Buy();
            UpdatePlayerResource();
        }
        else DialogPanel.Open();
    }

    private void OnChangeWeapon(ItemView itemView)
    {
        Player.PlayerStats.PlayerDamage.ChangeCurrentWeapon((itemView as WeaponView).Weapon);
    }

    private void OnChangeArmor(ItemView itemView)
    {
        Player.PlayerStats.PlayerArmor.ChangeCurrentArmor((itemView as ArmorView).Armor);
    }

    private void AddPanelsListener()
    {
        foreach (var panel in _shopTabs)
        {
            panel.PanelOpened += CloseAllPanels;
        }
    }

    private void RemovePanelsListener()
    {
        foreach (var panel in _shopTabs)
        {
            panel.PanelOpened -= CloseAllPanels;
        }
    }

    private void CloseAllPanels()
    {
        foreach (var panel in _shopTabs)
        {
            panel.gameObject.SetActive(false);
        }
    }
}