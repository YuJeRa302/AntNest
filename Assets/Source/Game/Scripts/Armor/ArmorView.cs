using Lean.Localization;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ArmorView : MonoBehaviour
{
    [Header("[Armor View]")]
    [SerializeField] private Text _priceItem;
    [SerializeField] private Image _iconItem;
    [SerializeField] private Image _coinIcon;
    [SerializeField] private Button _buyButton;
    [SerializeField] private Button _changeButton;
    [SerializeField] private Image _isBayed;
    [SerializeField] private Image _isCurrentArmor;
    [SerializeField] private Text _armorItem;
    [SerializeField] private Text _levelItem;
    [Header("[Name]")]
    [SerializeField] private LeanLocalizedText _name;

    private Armor _armor;

    public event Action<Armor, ArmorView> BuyButtonClick;
    public event Action<Armor> ChangeArmorButtonClick;

    public void Render(Armor armor)
    {
        _armor = armor;
        _priceItem.text = armor.Price.ToString();
        _iconItem.sprite = armor.ItemIcon;
        _armorItem.text = armor.ItemArmor.ToString();
        _name.TranslationName = armor.Name;
        _levelItem.text = armor.ArmorLevel.ToString();
        _buyButton.interactable = false;
    }

    public void TryLockItem()
    {
        if (_armor.IsBayed)
        {
            _buyButton.gameObject.SetActive(false);
            _isBayed.gameObject.SetActive(true);
            _changeButton.gameObject.SetActive(true);
        }
    }

    public void OnButtonClick()
    {
        BuyButtonClick?.Invoke(_armor, this);
    }

    public void OnChangeCurrentArmor()
    {
        ChangeArmorButtonClick?.Invoke(_armor);
    }

    private void OnEnable()
    {
        _armor.OnChangeState += SetCurrent;
        _buyButton.onClick.AddListener(OnButtonClick);
        _buyButton.onClick.AddListener(TryLockItem);
        _changeButton.onClick.AddListener(OnChangeCurrentArmor);
    }

    private void OnDisable()
    {
        _armor.OnChangeState -= SetCurrent;
        _buyButton.onClick.RemoveListener(OnButtonClick);
        _buyButton.onClick.RemoveListener(TryLockItem);
        _changeButton.onClick.RemoveListener(OnChangeCurrentArmor);
    }

    private void SetCurrent(bool state)
    {
        _isCurrentArmor.gameObject.SetActive(state);
    }

    public void TryUnlockBuyButton(Player player)
    {
        if (player.PlayerStats.Level >= _armor.ArmorLevel) _buyButton.interactable = true;
        else return;
    }
}
