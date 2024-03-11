using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentView : ItemView
{
    [SerializeField] private Button _changeButton;
    [SerializeField] private Image _isBayed;
    [SerializeField] private Image _shopIcon;
    [SerializeField] private Image _isCurrentWeapon;
    [SerializeField] private Text _itemValue;
    [SerializeField] private Text _levelItem;

    public event Action<ItemView> ChangeCurrentEquipment;

    private void OnDestroy()
    {
        (ItemObj as Equipment).ActiveStateChanged -= SetCurrent;
        BuyButton.onClick.RemoveListener(OnButtonClick);
        BuyButton.onClick.RemoveListener(TryLockItem);
        _changeButton.onClick.RemoveListener(OnChangeCurrentEquipment);
    }

    public override void Initialize(ItemData itemData, Player player)
    {
        ItemObj = (itemData as EquipmentItem).Template;
        ItemDataPrice = (itemData as EquipmentItem).Price;
        GetPlayerEquipment(player);
        AddListener();
        Fill(itemData);
        TryLockItem();
        TryUnlockBuyButton(player, itemData);
        TrySetCurrentEquipment(ItemObj as Equipment, player);
    }

    private void GetPlayerEquipment(Player player)
    {
        List<Equipment> equipmentList;

        if ((ItemObj as Equipment) is Weapon) equipmentList = player.PlayerStats.PlayerEquipment.GetListWeapon();
        else equipmentList = player.PlayerStats.PlayerEquipment.GetListArmor();

        foreach (var equipment in equipmentList)
        {
            if (equipment.Value.Equals((ItemObj as Equipment).Value)) ItemObj = equipment;
        }
    }

    private void Fill(ItemData itemData)
    {
        ItemName.TranslationName = (itemData as EquipmentItem).Name;
        ItemPrice.text = (itemData as EquipmentItem).Price.ToString();
        ItemIcon.sprite = (itemData as EquipmentItem).ItemIcon;
        _itemValue.text = (itemData as EquipmentItem).Value.ToString();
        _levelItem.text = (itemData as EquipmentItem).Level.ToString();
        _shopIcon.sprite = (itemData as EquipmentItem).ShopIcon;
        BuyButton.interactable = false;
    }

    private void AddListener()
    {
        (ItemObj as Equipment).ActiveStateChanged += SetCurrent;
        BuyButton.onClick.AddListener(OnButtonClick);
        BuyButton.onClick.AddListener(TryLockItem);
        _changeButton.onClick.AddListener(OnChangeCurrentEquipment);
    }

    private void TryLockItem()
    {
        if ((ItemObj as Equipment).IsBayed)
        {
            BuyButton.gameObject.SetActive(false);
            _isBayed.gameObject.SetActive(true);
            _changeButton.gameObject.SetActive(true);
        }
    }

    private void TrySetCurrentEquipment(Equipment equipment, Player player)
    {
        if (equipment is Weapon) _isCurrentWeapon.gameObject.SetActive(player.PlayerStats.PlayerEquipment.CurrentWeapon.Equals(equipment));
        else _isCurrentWeapon.gameObject.SetActive(player.PlayerStats.PlayerEquipment.CurrentArmor.Equals(equipment));
    }

    private void TryUnlockBuyButton(Player player, ItemData itemData)
    {
        if (player.PlayerStats.Level >= (itemData as EquipmentItem).Level) BuyButton.interactable = true;
        else return;
    }

    private void OnChangeCurrentEquipment()
    {
        ChangeCurrentEquipment?.Invoke(this);
    }

    private void OnButtonClick()
    {
        BuyButtonClick?.Invoke(this);
    }

    private void SetCurrent(bool state)
    {
        _isCurrentWeapon.gameObject.SetActive(state);
    }
}