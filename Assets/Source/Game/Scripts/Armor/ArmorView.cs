using System;
using UnityEngine;
using UnityEngine.UI;

public class ArmorView : ItemView
{
    [SerializeField] private Button _changeButton;
    [SerializeField] private Image _isBayed;
    [SerializeField] private Image _isCurrentArmor;
    [SerializeField] private Text _armorItem;
    [SerializeField] private Text _levelItem;

    private Armor _armor;

   // public override event Action<ItemView> BuyButtonClick;
    //public override event Action<ItemView> ChangeItemButtonClick;

    public Armor Armor => _armor;

    private void OnDestroy()
    {
        _armor.Item.OnChangeState -= SetCurrent;
        BuyButton.onClick.RemoveListener(OnButtonClick);
        BuyButton.onClick.RemoveListener(TryLockItem);
        _changeButton.onClick.RemoveListener(OnChangeCurrentArmor);
    }

    public override void Initialize<Item>(Item item, Player player)
    {
        _armor = item as Armor;
        ShopItem = _armor.Item;
        Render(_armor);
        AddListener();
        TryUnlockBuyButton(player);
        TryLockItem();
        TrySetCurrentArmor(_armor, player);
    }

    private void Render(Armor armor)
    {
        ItemName.TranslationName = armor.Name;
        ItemPrice.text = armor.Price.ToString();
        ItemIcon.sprite = armor.ItemIcon;
        BuyButton.interactable = false;
        _armorItem.text = armor.ArmorValue.ToString();
        _levelItem.text = armor.Level.ToString();
    }

    private void AddListener()
    {
        _armor.Item.OnChangeState += SetCurrent;
        BuyButton.onClick.AddListener(OnButtonClick);
        BuyButton.onClick.AddListener(TryLockItem);
        _changeButton.onClick.AddListener(OnChangeCurrentArmor);
    }

    private void TryLockItem()
    {
        if (_armor.IsBayed)
        {
            BuyButton.gameObject.SetActive(false);
            _isBayed.gameObject.SetActive(true);
            _changeButton.gameObject.SetActive(true);
        }
    }

    private void TrySetCurrentArmor(Armor armor, Player player)
    {
        if (player.PlayerStats.PlayerDamage.CurrentWeapon == armor) ChangeItemButtonClick?.Invoke(this);
        else return;
    }

    private void TryUnlockBuyButton(Player player)
    {
        if (player.PlayerStats.Level >= _armor.Level) BuyButton.interactable = true;
        else return;
    }

    private void OnChangeCurrentArmor()
    {
        ChangeItemButtonClick?.Invoke(this);
    }

    private void OnButtonClick()
    {
        //BuyButtonClick?.Invoke(this);
    }

    private void SetCurrent(bool state)
    {
        _isCurrentArmor.gameObject.SetActive(state);
    }
}
