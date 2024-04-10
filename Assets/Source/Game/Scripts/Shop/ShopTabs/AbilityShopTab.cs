using System.Collections.Generic;
using UnityEngine;

public class AbilityShopTab : ShopTab
{
    [SerializeField] private Player _player;
    [SerializeField] private AbilityPanelItemView _itemView;

    private List<AbilityPanelItemView> _views = new();

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
        foreach (AbilityState abilityState in _player.PlayerInventory.ListAbilities)
        {
            AbilityPanelItemView view = Instantiate(_itemView, ItemContainer.transform);
            _views.Add(view);
            view.Initialize(abilityState, _player);
            view.BuyButtonClick += OnBuyAbility;
            view.UpgradeButtonClick += OnUpgradeAbility;
        }
    }

    private void Clear()
    {
        foreach (AbilityPanelItemView itemView in _views)
        {
            itemView.BuyButtonClick += OnBuyAbility;
            itemView.UpgradeButtonClick += OnUpgradeAbility;
            Destroy(itemView.gameObject);
        }

        _views.Clear();
    }

    private void OnBuyAbility(AbilityPanelItemView itemView)
    {
        if (itemView.AbilityState.AbilityData.Price <= _player.Wallet.Points)
        {
            _player.Wallet.BuyAbility(itemView.AbilityState.AbilityData.UpgradePrice);
            _player.PlayerStats.PlayerAbility.BuyAbility(itemView.AbilityState);
            Clear();
            Fill();
        }
        else DialogPanel.OpenPanel();
    }

    private void OnUpgradeAbility(AbilityPanelItemView itemView)
    {
        if (itemView.AbilityState.AbilityData.UpgradePrice <= _player.Wallet.Points)
        {
            _player.Wallet.BuyAbility(itemView.AbilityState.AbilityData.UpgradePrice);
            itemView.AbilityState.AbilityData.IncreaseCurrentLevel();
            _player.PlayerStats.PlayerAbility.BuyAbility(itemView.AbilityState);
            Clear();
            Fill();
        }
        else DialogPanel.OpenPanel();
    }
}
