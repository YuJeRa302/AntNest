using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Game.Scripts
{
    public class AbilityShopTab : ShopTab
    {
        [SerializeField] private Player _player;
        [SerializeField] private AbilityPanelItemView _itemView;
        [SerializeField] private ScrollRect _scroll;

        private List<AbilityPanelItemView> _views = new ();

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
        }

        private void Fill()
        {
            foreach (AbilityState abilityState in _player.PlayerInventory.ListAbilities)
            {
                AbilityPanelItemView view = Instantiate(_itemView, ItemContainer.transform);
                _views.Add(view);
                view.Initialize(abilityState, _player);
                view.BuyButtonClicked += OnBuyAbility;
                view.UpgradeButtonClicked += OnUpgradeAbility;
            }
        }

        private void Clear()
        {
            foreach (AbilityPanelItemView itemView in _views)
            {
                itemView.BuyButtonClicked -= OnBuyAbility;
                itemView.UpgradeButtonClicked -= OnUpgradeAbility;
                Destroy(itemView.gameObject);
            }

            _views.Clear();
        }

        private void ClearUnnecessaryAbility()
        {
            foreach (AbilityPanelItemView itemView in _views)
            {
                if (itemView.AbilityState.IsBuyed == false)
                {
                    itemView.BuyButtonClicked -= OnBuyAbility;
                    itemView.UpgradeButtonClicked -= OnUpgradeAbility;
                    Destroy(itemView.gameObject);
                    _player.PlayerInventory.RemoveUnnecessaryAbility(itemView.AbilityState);
                }
            }
        }

        private void OnBuyAbility(AbilityPanelItemView itemView)
        {
            if (itemView.AbilityState.AbilityData.Price > _player.Wallet.Points)
                DialogPanel.OpenPanel();

            if (itemView.AbilityState.AbilityData.Price <= _player.Wallet.Points)
            {
                _player.Wallet.BuyAbility(itemView.AbilityState.AbilityData.UpgradePrice);
                _player.PlayerStats.PlayerAbilityCaster.BuyAbility(itemView.AbilityState);
                UpdatePlayerResourceValue();
                ClearUnnecessaryAbility();
                Clear();
                Fill();
            }
        }

        private void OnUpgradeAbility(AbilityPanelItemView itemView)
        {
            var lastIndex = itemView.AbilityState.AbilityData.AbilityLevels.LastOrDefault();

            if (itemView.AbilityState.AbilityData.AbilityLevels.LastIndexOf(lastIndex) == itemView.AbilityState.CurrentLevel)
            {
                itemView.UpgradeButtonClicked -= OnUpgradeAbility;
                return;
            }

            if (itemView.AbilityState.AbilityData.UpgradePrice > _player.Wallet.Points)
                DialogPanel.OpenPanel();

            if (itemView.AbilityState.AbilityData.UpgradePrice <= _player.Wallet.Points)
            {
                _player.Wallet.BuyAbility(itemView.AbilityState.AbilityData.UpgradePrice);
                _player.PlayerStats.PlayerAbilityCaster.UpgradeAbility(itemView.AbilityState);
                UpdatePlayerResourceValue();
                Clear();
                Fill();
            }
        }
    }
}