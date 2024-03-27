using System.Collections.Generic;
using UnityEngine;

public class WeaponShopTab : ShopTab
{
    [SerializeField] private Player _player;
    [SerializeField] private ItemView _itemView;
    [SerializeField] private PlayerEquipment _playerEquipment;

    private List<ItemView> _views = new ();

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
        foreach (EquipmentItemState equipmentItemState in _playerEquipment.ListWeapon)
        {
            EquipmentPanelItemView view = Instantiate(_itemView, _container) as EquipmentPanelItemView;
            _views.Add(view);
            view.Initialize(equipmentItemState, _player);
            view.BuyButtonClick += OnBuyWeapon;
            view.ChangeCurrentEquipment += OnChangeWeapon;
        }
    }

    private void Clear()
    {
        foreach (ItemView itemView in _views)
        {
            itemView.BuyButtonClick -= OnBuyWeapon;
            (itemView as EquipmentPanelItemView).ChangeCurrentEquipment -= OnChangeWeapon;
            Destroy(itemView.gameObject);
        }
        
        _views.Clear();
    }

    private void OnBuyWeapon(ItemView equipmentPanelItemView)
    {
        if ((equipmentPanelItemView as EquipmentPanelItemView).EquipmentItemState.ItemData.Price <= _player.Wallet.Coins)
        {
            _playerEquipment.BuyEquipmentItem((equipmentPanelItemView as EquipmentPanelItemView).EquipmentItemState);
            Clear();
            Fill();
        }
    }

    private void OnChangeWeapon(ItemView equipmentPanelItemView)
    {
        _playerEquipment.EquipWeapon((equipmentPanelItemView as EquipmentPanelItemView).EquipmentItemState);
        Clear();
        Fill();
    }
}