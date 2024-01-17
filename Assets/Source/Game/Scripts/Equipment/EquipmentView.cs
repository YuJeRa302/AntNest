using Lean.Localization;
using System;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentView : MonoBehaviour
{
    [Header("[Equipment View]")]
    [Header("[Text]")]
    [SerializeField] private Text _priceItem;
    [SerializeField] private Text _itemValue;
    [SerializeField] private Text _itemLevel;
    [Header("[Image]")]
    [SerializeField] private Image _iconItem;
    [SerializeField] private Image _coinIcon;
    [SerializeField] private Image _isBayed;
    [SerializeField] private Image _isCurrentEquipment;
    [Header("[Button]")]
    [SerializeField] private Button _buyButton;
    [SerializeField] private Button _changeButton;
    [Header("[LeanLocalizedText]")]
    [SerializeField] private LeanLocalizedText _name;

    private Equipment _equipment;

    public event Action<Equipment, EquipmentView> BuyButtonClick;
    public event Action<Equipment> ChangeEquipmentButtonClick;

    public void Render(Equipment equipment)
    {
        _equipment = equipment;
        _name.TranslationName = equipment.Name;
        _priceItem.text = equipment.Price.ToString();
        _iconItem.sprite = equipment.ItemIcon;
        _itemValue.text = equipment.Value.ToString();
        _itemLevel.text = equipment.Level.ToString();
        _buyButton.interactable = false;
    }

    public void TryLockItem()
    {
        if (_equipment.IsBayed)
        {
            _buyButton.gameObject.SetActive(false);
            _isBayed.gameObject.SetActive(true);
            _changeButton.gameObject.SetActive(true);
        }
    }

    public void OnChangeCurrentEquipment()
    {
        ChangeEquipmentButtonClick?.Invoke(_equipment);
    }

    public void OnButtonClick()
    {
        BuyButtonClick?.Invoke(_equipment, this);
    }

    public void TryUnlockBuyButton(Player player)
    {
       // if (player.Level >= _equipment.Level) _buyButton.interactable = true;
       // else return;
    }

    private void OnEnable()
    {
        _equipment.OnChangeState += SetCurrent;
        _buyButton.onClick.AddListener(OnButtonClick);
        _buyButton.onClick.AddListener(TryLockItem);
        _changeButton.onClick.AddListener(OnChangeCurrentEquipment);
    }

    private void OnDisable()
    {
        _equipment.OnChangeState -= SetCurrent;
        _buyButton.onClick.RemoveListener(OnButtonClick);
        _buyButton.onClick.RemoveListener(TryLockItem);
        _changeButton.onClick.RemoveListener(OnChangeCurrentEquipment);
    }

    private void SetCurrent(bool state)
    {
        _isCurrentEquipment.gameObject.SetActive(state);
    }
}