using UnityEngine;
using UnityEngine.UI;

public class WeaponView : ItemView
{
    [SerializeField] private Button _changeButton;
    [SerializeField] private Image _isBayed;
    [SerializeField] private Image _isCurrentWeapon;
    [SerializeField] private Text _itemDamage;
    [SerializeField] private Text _levelItem;

    private Weapon _weapon;

    public Weapon Weapon => _weapon;

    private void OnDestroy()
    {
        _weapon.OnChangeState -= SetCurrent;
        BuyButton.onClick.RemoveListener(OnButtonClick);
        BuyButton.onClick.RemoveListener(TryLockItem);
        _changeButton.onClick.RemoveListener(OnChangeCurrentWeapon);
    }

    public override void Initialize<Item>(Item item, Player player)
    {
        _weapon = item as Weapon;
        ShopItem = _weapon.Item;
        Fill(_weapon);
        AddListener();
        TryUnlockBuyButton(player);
        TryLockItem();
        TrySetCurrentWeapon(_weapon, player);
    }

    private void Fill(Weapon weapon)
    {
        ItemName.TranslationName = weapon.Name;
        ItemPrice.text = weapon.Price.ToString();
        ItemIcon.sprite = weapon.ItemIcon;
        BuyButton.interactable = false;
        _itemDamage.text = weapon.Damage.ToString();
        _levelItem.text = weapon.Level.ToString();
    }

    private void AddListener()
    {
        _weapon.OnChangeState += SetCurrent;
        BuyButton.onClick.AddListener(OnButtonClick);
        BuyButton.onClick.AddListener(TryLockItem);
        _changeButton.onClick.AddListener(OnChangeCurrentWeapon);
    }

    private void TryLockItem()
    {
        if (_weapon.IsBayed)
        {
            BuyButton.gameObject.SetActive(false);
            _isBayed.gameObject.SetActive(true);
            _changeButton.gameObject.SetActive(true);
        }
    }

    private void TrySetCurrentWeapon(Weapon weapon, Player player)
    {
        if (player.PlayerStats.PlayerDamage.CurrentWeapon == weapon) ChangeItemButtonClick?.Invoke(this);
        else return;
    }

    private void TryUnlockBuyButton(Player player)
    {
        if (player.PlayerStats.Level >= _weapon.Level) BuyButton.interactable = true;
        else return;
    }

    private void OnChangeCurrentWeapon()
    {
        ChangeItemButtonClick?.Invoke(this);
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