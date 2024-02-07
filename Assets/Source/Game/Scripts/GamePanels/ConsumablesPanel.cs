using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsumablesPanel : Shop
{
    [SerializeField] private Shop _shop;
    [SerializeField] private ConsumablesView _consumablesView;
    [SerializeField] private GameObject _consumablesContainer;
    [SerializeField] private Button _button;

    private List<Consumables> _consumables;
    private List<ConsumablesView> _consumablesViews = new();
    private Player _player;
    private LevelObserver _levelObserver;

    private void Awake()
    {
        //_shop.Initialized += OnShopInitialized;
    }

    private void OnDestroy()
    {
        //_button.onClick.RemoveListener(OpenShopTab);
        //_shop.Initialized -= OnShopInitialized;
        _levelObserver.GameClosed -= OnCloseGame;
    }

    protected override void FillPanel()
    {
        if (_consumables == null)
        {
            _consumables = _player.PlayerConsumables.GetListConsumables();
            AddToList(_consumables);
        }
    }

    private void OnShopInitialized(Player player, LevelObserver levelObserver)
    {
        _player = player;
        _levelObserver = levelObserver;
        //_button.onClick.AddListener(OpenShopTab);
        _levelObserver.GameClosed += OnCloseGame;
        FillPanel();
    }

    private void TryBuyItem(Consumables consumables)
    {
        if (consumables.Price <= _player.Wallet.Coins)
        {
            _player.PlayerConsumables.BuyConsumables(consumables.Price);
            UpdatePlayerResource();
        }
        else DialogPanel.Open();
    }

    private void OnBuyConsumables(Consumables consumables)
    {
        TryBuyItem(consumables);
    }

    private void AddConsumables(Consumables consumables)
    {
        var view = Instantiate(_consumablesView, _consumablesContainer.transform);
        view.BuyButtonClick += OnBuyConsumables;
        view.Render(consumables);
        _consumablesViews.Add(view);
    }

    private void AddToList(List<Consumables> consumables)
    {
        for (int i = 0; i < consumables.Count; i++)
        {
            AddConsumables(consumables[i]);
        }
    }

    private void OnCloseGame()
    {
        if (_consumablesViews.Count > 0)
        {
            foreach (var view in _consumablesViews)
            {
                view.BuyButtonClick -= OnBuyConsumables;
            }
        }
    }
}