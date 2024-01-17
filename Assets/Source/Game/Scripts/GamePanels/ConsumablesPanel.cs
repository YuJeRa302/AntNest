using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsumablesPanel : Shop
{
    [SerializeField] private ConsumablesView _consumablesView;
    [SerializeField] private GameObject _consumablesContainer;
    [SerializeField] private Button _button;

    private List<Consumables> _consumables;
    private List<ConsumablesView> _consumablesViews;

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
        if (_consumables == null)
        {
            _consumables = Player.PlayerConsumables.GetListConsumables();
            AddToList(_consumables);
        }
    }

    private void TryBuyItem(Consumables consumables)
    {
        if (consumables.Price <= Player.Wallet.GetCoins())
        {
            Player.PlayerConsumables.BuyConsumables(consumables.Price);
            UpdatePlayerStats();
        }
        else DialogPanel.Opened?.Invoke();
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