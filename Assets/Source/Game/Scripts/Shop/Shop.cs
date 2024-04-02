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

    public event Action<int, int> PlayerResourceChanged;

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

    public void ChangePlayerResourceValue()
    {
        PlayerResourceChanged?.Invoke(Player.Wallet.Coins, Player.PlayerStats.PlayerAbility.Points);
    }

    protected override void Open()
    {
        gameObject.SetActive(true);
        ChangePlayerResourceValue();
        InitializeShopTabs();
        CloseTabs();
    }

    protected override void Close()
    {
        gameObject.SetActive(false);
        PanelClosed?.Invoke();
        //ClearItems();
    }

    // private void FillShopTabs()
    // {
    //     foreach (var tab in _shopTabs)
    //     {
    //         switch (tab.ItemType)
    //         {
    //             case TypeItem.Weapon:
    //                 CreateListItem(tab.Items, tab.ItemView, tab.Container);
    //                 break;
    //             case TypeItem.Armor:
    //                 //CreateListItem(_armors, tab.ItemView, tab.Container);
    //                 break;
    //             case TypeItem.Consumables:
    //                 //CreateListItem(tab.Items, tab.ItemView, tab.Container);
    //                 break;
    //             case TypeItem.Ability:
    //                 //CreateListItem(_abilities, tab.ItemView, tab.Container);
    //                 break;
    //         }
    //     }
    // }
    //
    // private void CreateListItem(List<ItemData> itemDatas, ItemView itemView, Transform container)
    // {
    //     foreach (var t in itemDatas)
    //         AddItem(t, itemView, container);
    // }
    //
    // private void AddItem(ItemData itemData, ItemView itemView, Transform container)
    // {
    //     var view = Instantiate(itemView, container);
    //     view.Initialize(itemData, Player);
    //     AddButtonListener(view);
    //     _itemViews.Add(view);
    // }
    //
    // private void AddButtonListener(ItemView itemView)
    // {
    //     if (itemView is AbilityView) 
    //         itemView.BuyButtonClick += OnBuyAbility;
    //     else 
    //         itemView.BuyButtonClick += OnBuyItem;
    //     
    //     if (itemView is EquipmentPanelItemView) 
    //         (itemView as EquipmentPanelItemView).ChangeCurrentEquipment += OnChangeEquipment;
    //     
    //     if (itemView is AbilityView) 
    //         (itemView as AbilityView).UpgradeButtonClick += OnUpgradeAbility;
    // }
    //
    // private void OnBuyItem(ItemView itemView)
    // {
    //     if (itemView.ItemData.Price <= Player.Wallet.Coins)
    //     {
    //         Player.Wallet.Buy(itemView.ItemData.Price);
    //
    //        // if (itemView is EquipmentPanelItemView) itemView.ItemGameObject.Buy();
    //
    //         if (itemView is ConsumablesView) Player.PlayerConsumables.TakePotion();
    //
    //         GetPlayerResourceValue();
    //     }
    //     else DialogPanel.OpenPanel();
    // }
    //
    // private void OnBuyAbility(ItemView itemView)
    // {
    //     if (itemView.ItemData.Price <= Player.PlayerStats.PlayerAbility.Points)
    //     {
    //         Player.PlayerStats.PlayerAbility.GivePoints(itemView.ItemData.Price);
    //         //itemView.Item.Buy();
    //         LockAbility(itemView);
    //         GetPlayerResourceValue();
    //     }
    //     else DialogPanel.OpenPanel();
    // }
    //
    // private void OnUpgradeAbility(ItemView itemView)
    // {
    //     if ((itemView as AbilityView).Ability.UpgradePrice <= Player.PlayerStats.PlayerAbility.Points)
    //     {
    //         Player.PlayerStats.PlayerAbility.GivePoints((itemView as AbilityView).Ability.UpgradePrice);
    //         //itemView.Item.Upgrade();
    //         GetPlayerResourceValue();
    //     }
    //     else DialogPanel.OpenPanel();
    // }
    //
    // private void OnChangeEquipment(ItemView itemView)
    // {
    //     if (itemView.ItemData.ItemType == ItemType.Armor)
    //         Player.PlayerStats.PlayerEquipment.EquipArmor((itemView as EquipmentPanelItemView).ItemState);
    // }
    //
    // private void LockAbility(ItemView itemView)
    // {
    //     foreach (var view in _itemViews)
    //     {
    //         if (view is AbilityView)
    //         {
    //             // if ((view as AbilityView) != (itemView as AbilityView)) (view as AbilityView).LockedAbility();
    //         }
    //     }
    // }
    //
    // private void ClearItems()
    // {
    //     foreach (var view in _itemViews)
    //     {
    //         view.BuyButtonClick -= OnBuyItem;
    //
    //         if (view is AbilityView) view.BuyButtonClick -= OnBuyAbility;
    //         if (view is EquipmentPanelItemView) (view as EquipmentPanelItemView).ChangeCurrentEquipment -= OnUpgradeAbility;
    //         if (view is AbilityView) (view as AbilityView).UpgradeButtonClick -= OnUpgradeAbility;
    //
    //         Destroy(view.gameObject);
    //     }
    //
    //     _itemViews.Clear();
    // }

    private void InitializeShopTabs()
    {
        foreach (var tab in _shopTabs)
        {
            tab.Initialize(_dialogPanel);
        }
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