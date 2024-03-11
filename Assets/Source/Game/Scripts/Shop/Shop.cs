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
        FillShopTabs();
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

    private void FillShopTabs()
    {
        foreach (var tab in _shopTabs)
        {
            switch (tab.ItemType)
            {
                case TypeItem.Weapon:
                    CreateListItem(tab.Items, tab.ItemView, tab.Container);
                    break;
                case TypeItem.Armor:
                    //CreateListItem(_armors, tab.ItemView, tab.Container);
                    break;
                case TypeItem.Consumables:
                    //CreateListItem(tab.Items, tab.ItemView, tab.Container);
                    break;
                case TypeItem.Ability:
                    //CreateListItem(_abilities, tab.ItemView, tab.Container);
                    break;
            }
        }
    }

    private void CreateListItem(List<ItemData> itemDatas, ItemView itemView, Transform container)
    {
        for (int i = 0; i < itemDatas.Count; i++)
        {
            AddItem(itemDatas[i], itemView, container);
        }
    }

    private void AddItem(ItemData itemData, ItemView itemView, Transform container)
    {
        var view = Instantiate(itemView, container);
        view.Initialize(itemData, Player);
        AddButtonListener(view);
        _itemViews.Add(view);
    }

    private void AddButtonListener(ItemView itemView)
    {
        if (itemView is AbilityView) itemView.BuyButtonClick += OnBuyAbility;
        else itemView.BuyButtonClick += OnBuyItem;
        if (itemView is EquipmentView) (itemView as EquipmentView).ChangeCurrentEquipment += OnChangeEquipment;
        if (itemView is AbilityView) (itemView as AbilityView).UpgradeButtonClick += OnUpgradeAbility;
    }

    private void OnBuyItem(ItemView itemView)
    {
        if (itemView.Price <= Player.Wallet.Coins)
        {
            Player.Wallet.Buy(itemView.Price);

            if (itemView is EquipmentView) itemView.ItemObject.Buy();

            if (itemView is ConsumablesView) Player.PlayerConsumables.TakePotion();

            GetPlayerResourceValue();
        }
        else DialogPanel.OpenPanel();
    }

    private void OnBuyAbility(ItemView itemView)
    {
        if (itemView.Price <= Player.PlayerStats.PlayerAbility.Points)
        {
            Player.PlayerStats.PlayerAbility.GivePoints(itemView.Price);
            //itemView.Item.Buy();
            LockAbility(itemView);
            GetPlayerResourceValue();
        }
        else DialogPanel.OpenPanel();
    }

    private void OnUpgradeAbility(ItemView itemView)
    {
        if ((itemView as AbilityView).Ability.UpgradePrice <= Player.PlayerStats.PlayerAbility.Points)
        {
            Player.PlayerStats.PlayerAbility.GivePoints((itemView as AbilityView).Ability.UpgradePrice);
            //itemView.Item.Upgrade();
            GetPlayerResourceValue();
        }
        else DialogPanel.OpenPanel();
    }

    private void OnChangeEquipment(ItemView itemView)
    {
        Player.PlayerStats.PlayerEquipment.ChangeCurrentEquipment(itemView.ItemObject as Equipment);
    }

    private void LockAbility(ItemView itemView)
    {
        foreach (var view in _itemViews)
        {
            if (view is AbilityView)
            {
                // if ((view as AbilityView) != (itemView as AbilityView)) (view as AbilityView).LockedAbility();
            }
        }
    }

    private void ClearItems()
    {
        foreach (var view in _itemViews)
        {
            view.BuyButtonClick -= OnBuyItem;

            if (view is AbilityView) view.BuyButtonClick -= OnBuyAbility;
            if (view is EquipmentView) (view as EquipmentView).ChangeCurrentEquipment -= OnUpgradeAbility;
            if (view is AbilityView) (view as AbilityView).UpgradeButtonClick -= OnUpgradeAbility;

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