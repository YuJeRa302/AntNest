using System.Collections.Generic;
using UnityEngine;

public class ConsumableShopTab : ShopTab
{
    [SerializeField] private Player _player;
    [SerializeField] private ConsumablePanelItemView _itemView;

    private List<ConsumablePanelItemView> _views = new();

    private void Start()
    {
        Fill();
    }

    private new void OnDestroy()
    {
        base.OnDestroy();
        Clear();
    }

    private void Fill()
    {
        foreach (ConsumableItemState consumableItemState in _player.PlayerInventory.ListConsumables)
        {
            ConsumablePanelItemView view = Instantiate(_itemView, ItemContainer.transform);
            _views.Add(view);
            view.Initialize(consumableItemState);
            view.BuyButtonClicked += OnBuyConsumable;
        }
    }

    private void Clear()
    {
        foreach (ConsumablePanelItemView itemView in _views)
        {
            itemView.BuyButtonClicked -= OnBuyConsumable;
            Destroy(itemView.gameObject);
        }

        _views.Clear();
    }

    private void OnBuyConsumable(ConsumablePanelItemView consumablePanelItemView)
    {
        if (consumablePanelItemView.ConsumableItemState.ConsumableItemData.Price <= _player.Wallet.Coins)
        {
            _player.Wallet.BuyItem(consumablePanelItemView.ConsumableItemState.ConsumableItemData.Price);
            _player.PlayerConsumables.BuyItem(consumablePanelItemView.ConsumableItemState);
            UpdatePlayerResourceValue();
            Clear();
            Fill();
        }
        else
            DialogPanel.OpenPanel();
    }
}
