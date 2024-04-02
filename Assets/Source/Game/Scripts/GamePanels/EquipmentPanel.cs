using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class EquipmentPanel : ShopTab
{
    [FormerlySerializedAs("_weaponView")]
    [Header("[Views]")]
    [SerializeField] private EquipmentPanelItemView _weaponPanelItemView;
    [SerializeField] private EquipmentPanelItemView _armorPanelItemView;
    [Header("[Containers]")]
    [SerializeField] private GameObject _weaponContainer;
    [SerializeField] private GameObject _armorContainer;

    private List<EquipmentItemGameObject> _weapons;
    private List<EquipmentItemGameObject> _armors;
    private List<EquipmentPanelItemView> _itemViews = new();
    private List<EquipmentItemState> _equipmentItemState;

    protected override void FillTab()
    {
        //_equipmentItemState = Player.PlayerInventory.ListWeapon;

       // Debug.Log(Player);
        //var equipmentsState = Player.PlayerInventory.GetPlayerEquipmentState().Items;

        //foreach (var item in _equipmentItemState)
        //{
        //    if (item.ItemData.ItemType == TypeItem.Weapon) AddItem(item, _weaponPanelItemView, _weaponContainer);
        //    else AddItem(item, _weaponPanelItemView, _armorContainer);
        //}
    }

    private void AddItem(ItemState itemState, EquipmentPanelItemView itemView, GameObject container)
    {
        var view = Instantiate(itemView, container.transform);
        //view.Initialize(itemState, Player);
        AddButtonListener(view);
        _itemViews.Add(view);
    }

    private void AddButtonListener(EquipmentPanelItemView itemView)
    {
        //itemView.BuyButtonClick += OnBuyItem;
        //itemView.ChangeCurrentEquipment += OnEquipItem;
    }

    //private void OnBuyItem(EquipmentPanelItemView itemView)
    //{
    //    if (itemView.ItemState.ItemData.Price <= Player.Wallet.Coins)
    //    {
    //        Player.Wallet.Buy(itemView.ItemState.ItemData.Price);
    //        itemView.ItemGameObject.Buy()
    //        GetPlayerResourceValue();
    //    }
    //    else DialogPanel.OpenPanel();
    //}

    //private void OnEquipItem(EquipmentPanelItemView itemView)
    //{
    //    if (itemView.ItemData.ItemType == ItemType.Armor)
    //        Player.PlayerStats.PlayerEquipment.EquipArmor((itemView as EquipmentPanelItemView).ItemState);
    //}

    private void ClearItems()
    {
        foreach (var view in _itemViews)
        {
            //view.BuyButtonClick -= OnBuyItem;
            //view.ChangeCurrentEquipment -= OnEquipItem;
            Destroy(view.gameObject);
        }

        _itemViews.Clear();
    }
}