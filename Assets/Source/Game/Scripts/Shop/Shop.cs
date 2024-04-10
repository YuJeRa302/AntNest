using System;
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
        Player.PlayerView.UpdatePlayerStats();
    }

    private void ChangePlayerResourceValue()
    {
        PlayerResourceChanged?.Invoke(Player.Wallet.Coins, Player.Wallet.Points);
    }

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
            tab.PlayerResourceUpdated += ChangePlayerResourceValue;
        }
    }

    private void RemovePanelsListener()
    {
        foreach (var tab in _shopTabs)
        {
            tab.TabOpened -= CloseTabs;
            tab.PlayerResourceUpdated -= ChangePlayerResourceValue;
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