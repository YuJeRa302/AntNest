using System.Collections.Generic;
using UnityEngine;

public class EquipmentShopTab : ShopTab
{
    [SerializeField] private Player _player;
    [SerializeField] private EquipmentPanelItemView _itemView;
    [SerializeField] private PlayerEquipment _playerEquipment;

    private List<EquipmentPanelItemView> _views = new();
    private List<EquipmentItemState> _equipmentItemStates = new();

    private void Start()
    {
        if (ItemType == TypeItem.Armor)
            _equipmentItemStates = _player.PlayerInventory.ListArmor;
        else _equipmentItemStates = _player.PlayerInventory.ListWeapon;

        Fill();
    }

    private new void OnDestroy()
    {
        base.OnDestroy();
        Clear();
    }

    private void Fill()
    {
        foreach (EquipmentItemState equipmentItemState in _equipmentItemStates)
        {
            EquipmentPanelItemView view = Instantiate(_itemView, ItemContainer.transform);
            _views.Add(view);
            view.Initialize(equipmentItemState, _player);
            view.BuyButtonClick += OnBuyEquipment;
            view.ChangeCurrentEquipment += OnChangeEquipment;
        }
    }

    private void Clear()
    {
        foreach (EquipmentPanelItemView itemView in _views)
        {
            itemView.BuyButtonClick -= OnBuyEquipment;
            itemView.ChangeCurrentEquipment -= OnChangeEquipment;
            Destroy(itemView.gameObject);
        }

        _views.Clear();
    }

    private void OnBuyEquipment(EquipmentPanelItemView equipmentPanelItemView)
    {
        if (equipmentPanelItemView.EquipmentItemState.ItemData.Price <= _player.Wallet.Coins)
        {
            _playerEquipment.BuyItem(equipmentPanelItemView.EquipmentItemState);
            _player.Wallet.BuyItem(equipmentPanelItemView.EquipmentItemState.ItemData.Price);
            UpdatePlayerResourceValue();
            Clear();
            Fill();
        }
        else DialogPanel.OpenPanel();
    }

    private void OnChangeEquipment(EquipmentPanelItemView equipmentPanelItemView)
    {
        _playerEquipment.EquipItem(equipmentPanelItemView.EquipmentItemState);
        Clear();
        Fill();
    }
}
