using System.Collections.Generic;
using UnityEngine;

public class WeaponPanel : ShopTab
{
    //[Header("[Views]")]
    //[SerializeField] private EquipmentPanelItemView _weaponPanelItemView;

    //private List<EquipmentPanelItemView> _itemViews = new();
    //private List<EquipmentItemState> _equipmentItemState;

    //protected override void FillTab()
    //{
    //    //_equipmentItemState = Player.PlayerInventory.ListWeapon;

    //    foreach (var item in _equipmentItemState)
    //        AddItem(item, _weaponPanelItemView, ItemContainer);
    //}

    //private void AddItem(ItemState itemState, EquipmentPanelItemView itemView, GameObject container)
    //{
    //    var view = Instantiate(itemView, container.transform);
    //   // view.Initialize(itemState, Player);
    //    AddButtonListener(view);
    //    _itemViews.Add(view);
    //}

    //private void AddButtonListener(EquipmentPanelItemView itemView)
    //{
    //    itemView.BuyButtonClick += OnBuyItem;
    //    itemView.ChangeCurrentEquipment += OnEquipItem;
    //}

    //private void OnBuyItem(EquipmentPanelItemView itemView)
    //{
    //    if (itemView.EquipmentItemState.ItemData.Price <= Player.Wallet.Coins)
    //    {
    //        Player.Wallet.Buy(itemView.EquipmentItemState.ItemData.Price);
    //        Player.PlayerStats.PlayerEquipment.BuyEquipmentItem(itemView.EquipmentItemState);
    //        UpdatePlayerResourceValue();
    //    }
    //    else DialogPanel.OpenPanel();
    //}

    //private void OnEquipItem(EquipmentPanelItemView itemView)
    //{
    //    Player.PlayerStats.PlayerEquipment.EquipWeapon(itemView.EquipmentItemState);
    //}

    //private void ClearItems()
    //{
    //    foreach (var view in _itemViews)
    //    {
    //        view.BuyButtonClick -= OnBuyItem;
    //        view.ChangeCurrentEquipment -= OnEquipItem;
    //        Destroy(view.gameObject);
    //    }

    //    _itemViews.Clear();
    //}
}