using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class AbilityShopTab : ShopTab
{
    [SerializeField] private Player _player;
    [SerializeField] private AbilityPanelItemView _itemView;
    [SerializeField] private ScrollRect _scroll;
    [SerializeField] private DotView _dotView;

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

    protected override void OpenTab()
    {
        base.OpenTab();
        _dotView.SetScrollRect(_scroll);
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
            itemView.BuyButtonClick -= OnBuyAbility;
            itemView.UpgradeButtonClick -= OnUpgradeAbility;
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
            UpdatePlayerResourceValue();
            Clear();
            Fill();
        }
        else DialogPanel.OpenPanel();
    }

    private void OnUpgradeAbility(AbilityPanelItemView itemView)
    {
        var lastIndex = itemView.AbilityState.AbilityData.AbilityLevels.LastOrDefault();

        if (itemView.AbilityState.AbilityData.AbilityLevels.LastIndexOf(lastIndex) == itemView.AbilityState.CurrentLevel)
        {
            itemView.UpgradeButtonClick -= OnUpgradeAbility;
            return;
        }

        if (itemView.AbilityState.AbilityData.UpgradePrice <= _player.Wallet.Points)
        {
            _player.Wallet.BuyAbility(itemView.AbilityState.AbilityData.UpgradePrice);
            _player.PlayerStats.PlayerAbility.UpgradeAbility(itemView.AbilityState);
            UpdatePlayerResourceValue();
            Clear();
            Fill();
        }
        else DialogPanel.OpenPanel();
    }
}
