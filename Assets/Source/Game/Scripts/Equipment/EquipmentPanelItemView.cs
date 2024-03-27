using System;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentPanelItemView : ItemView
{
    [SerializeField] private Button _changeButton;
    [SerializeField] private Image _isBayed;
    [SerializeField] private Image _shopIcon;
    [SerializeField] private Image _isCurrentWeapon;
    [SerializeField] private Text _itemValue;
    [SerializeField] private Text _levelItem;

    private PlayerEquipment _playerEquipment;
    private EquipmentItemState _equipmentItemState;
    
    public event Action<ItemView> ChangeCurrentEquipment;

    public EquipmentItemState EquipmentItemState => _equipmentItemState;

    private void OnDestroy()
    {
        _playerEquipment.EquipmentChanged -= CheckEquipState;
        BuyButton.onClick.RemoveListener(OnButtonClick);
        BuyButton.onClick.RemoveListener(TryLockItem);
        _changeButton.onClick.RemoveListener(OnChangeCurrentEquipment);
    }

    public override void Initialize(ItemState itemState, Player player)
    {
        _playerEquipment = player.PlayerEquipment;
        _equipmentItemState = itemState as EquipmentItemState;
        AddListener();
        Fill(_equipmentItemState);
        TryLockItem();
        TryUnlockBuyButton(player, _equipmentItemState);
        TrySetCurrentEquipment(_equipmentItemState);
    }

    private void Fill(EquipmentItemState equipmentItemState)
    {
        ItemName.TranslationName = equipmentItemState.ItemData.Name;
        ItemPrice.text = equipmentItemState.ItemData.Price.ToString();
        ItemIcon.sprite = equipmentItemState.ItemData.ItemIcon;
        _itemValue.text = equipmentItemState.ItemData.Value.ToString();
        _levelItem.text = equipmentItemState.ItemData.Level.ToString();
        _shopIcon.sprite = equipmentItemState.ItemData.ShopIcon;
        BuyButton.interactable = false;
    }

    private void AddListener()
    {
        _playerEquipment.EquipmentChanged += CheckEquipState;
        BuyButton.onClick.AddListener(OnButtonClick);
        BuyButton.onClick.AddListener(TryLockItem);
        _changeButton.onClick.AddListener(OnChangeCurrentEquipment);
    }

    private void TryLockItem()
    {
        if (_equipmentItemState.IsBuyed == false) 
            return;
        
        BuyButton.gameObject.SetActive(false);
        _isBayed.gameObject.SetActive(true);
        _changeButton.gameObject.SetActive(true);
    }

    private void TrySetCurrentEquipment(EquipmentItemState equipmentItemState)
    {
        _isCurrentWeapon.gameObject.SetActive(equipmentItemState.IsEquipped);
    }

    private void TryUnlockBuyButton(Player player, EquipmentItemState equipmentItemState)
    {
        if (player.PlayerStats.Level >= equipmentItemState.ItemData.Level) 
            BuyButton.interactable = true;
    }

    private void OnChangeCurrentEquipment()
    {
        ChangeCurrentEquipment?.Invoke(this);
    }

    private void OnButtonClick()
    {
        BuyButtonClick?.Invoke(this);
    }

    private void CheckEquipState()
    {
        _isCurrentWeapon.gameObject.SetActive(_equipmentItemState.IsEquipped);
    }
}