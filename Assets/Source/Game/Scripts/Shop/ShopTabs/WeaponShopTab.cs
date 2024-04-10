using System.Collections.Generic;
using UnityEngine;

public class WeaponShopTab : ShopTab
{
    [SerializeField] private Player _player;
    [SerializeField] private EquipmentPanelItemView _itemView;
    [SerializeField] private PlayerEquipment _playerEquipment;

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
        foreach (EquipmentItemState equipmentItemState in _player.PlayerInventory.ListWeapon)
        {
            EquipmentPanelItemView view = Instantiate(_itemView, ItemContainer.transform);
            _views.Add(view);
            view.Initialize(equipmentItemState, _player);
            view.BuyButtonClick += OnBuyWeapon;
            view.ChangeCurrentEquipment += OnChangeWeapon;
        }
    }

    private void Clear()
    {
        foreach (EquipmentPanelItemView itemView in _views)
        {
            itemView.BuyButtonClick -= OnBuyWeapon;
            itemView.ChangeCurrentEquipment -= OnChangeWeapon;
            Destroy(itemView.gameObject);
        }

        _views.Clear();
    }

    private void OnBuyWeapon(EquipmentPanelItemView equipmentPanelItemView)
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

    private void OnChangeWeapon(EquipmentPanelItemView equipmentPanelItemView)
    {
        _playerEquipment.EquipItem(equipmentPanelItemView.EquipmentItemState);
        Clear();
        Fill();
    }
}