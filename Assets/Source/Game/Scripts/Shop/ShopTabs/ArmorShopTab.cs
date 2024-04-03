using System.Collections.Generic;
using UnityEngine;

public class ArmorShopTab : ShopTab
{
    [SerializeField] private Player _player;
    [SerializeField] private EquipmentPanelItemView _itemView;
    [SerializeField] private PlayerArmorEquipment _playerEquipment;

    private List<EquipmentPanelItemView> _views = new();

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
        foreach (EquipmentItemState equipmentItemState in _player.PlayerInventory.ListArmor)
        {
            EquipmentPanelItemView view = Instantiate(_itemView, ItemContainer.transform);
            _views.Add(view);
            view.Initialize(equipmentItemState, _player);
            view.BuyButtonClick += OnBuyArmor;
            view.ChangeCurrentEquipment += OnChangeArmor;
        }
    }

    private void Clear()
    {
        foreach (EquipmentPanelItemView itemView in _views)
        {
            itemView.BuyButtonClick -= OnBuyArmor;
            itemView.ChangeCurrentEquipment -= OnChangeArmor;
            Destroy(itemView.gameObject);
        }

        _views.Clear();
    }

    private void OnBuyArmor(EquipmentPanelItemView equipmentPanelItemView)
    {
        if (equipmentPanelItemView.EquipmentItemState.ItemData.Price <= _player.Wallet.Coins)
        {
            _playerEquipment.BuyArmorItem(equipmentPanelItemView.EquipmentItemState);
            _player.Wallet.Buy(equipmentPanelItemView.EquipmentItemState.ItemData.Price);
            UpdatePlayerResourceValue();
            Clear();
            Fill();
        }
    }

    private void OnChangeArmor(EquipmentPanelItemView equipmentPanelItemView)
    {
        _playerEquipment.EquipArmor(equipmentPanelItemView.EquipmentItemState);
        Clear();
        Fill();
    }
}
