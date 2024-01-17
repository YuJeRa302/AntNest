using Lean.Localization;
using System;
using UnityEngine;
using UnityEngine.UI;

public class WeaponView : MonoBehaviour
{
    [Header("[Weapon View]")]
    [SerializeField] private Text _priceItem;
    [SerializeField] private Image _iconItem;
    [SerializeField] private Image _coinIcon;
    [SerializeField] private Button _buyButton;
    [SerializeField] private Button _changeButton;
    [SerializeField] private Image _isBayed;
    [SerializeField] private Image _isCurrentWeapon;
    [SerializeField] private Text _itemDamage;
    [SerializeField] private Text _levelItem;
    [Header("[Name]")]
    [SerializeField] private LeanLocalizedText _name;

    private Weapon _weapon;

    public event Action<Weapon, WeaponView> BuyButtonClick;
    public event Action<Weapon> ChangeWeaponButtonClick;

    public void Render(Weapon weapon)
    {
        _weapon = weapon;
        _name.TranslationName = weapon.Name;
        _priceItem.text = weapon.Price.ToString();
        _iconItem.sprite = weapon.ItemIcon;
        _itemDamage.text = weapon.Damage.ToString();
        _levelItem.text = weapon.WeaponLevel.ToString();
        _buyButton.interactable = false;
    }

    public void TryLockItem()
    {
        if (_weapon.IsBayed)
        {
            _buyButton.gameObject.SetActive(false);
            _isBayed.gameObject.SetActive(true);
            _changeButton.gameObject.SetActive(true);
        }
    }

    public void OnChangeCurrentWeapon()
    {
        ChangeWeaponButtonClick?.Invoke(_weapon);
    }

    public void OnButtonClick()
    {
        BuyButtonClick?.Invoke(_weapon, this);
    }

    private void OnEnable()
    {
        _weapon.OnChangeState += SetCurrent;
        _buyButton.onClick.AddListener(OnButtonClick);
        _buyButton.onClick.AddListener(TryLockItem);
        _changeButton.onClick.AddListener(OnChangeCurrentWeapon);
    }

    private void OnDisable()
    {
        _weapon.OnChangeState -= SetCurrent;
        _buyButton.onClick.RemoveListener(OnButtonClick);
        _buyButton.onClick.RemoveListener(TryLockItem);
        _changeButton.onClick.RemoveListener(OnChangeCurrentWeapon);
    }

    private void SetCurrent(bool state)
    {
        _isCurrentWeapon.gameObject.SetActive(state);
    }

    public void TryUnlockBuyButton(Player player)
    {
        if (player.PlayerStats.Level >= _weapon.WeaponLevel) _buyButton.interactable = true;
        else return;
    }
}
