using Lean.Localization;
using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class ItemView : MonoBehaviour
{
    [Header("[View]")]
    [SerializeField] protected Text ItemPrice;
    [SerializeField] protected Image ItemIcon;
    [SerializeField] protected Button BuyButton;
    [SerializeField] protected LeanLocalizedText ItemName;

    protected Item ShopItem;

    public Action<ItemView> BuyButtonClick;
    public Action<ItemView> ChangeCurrentArmor;
    public Action<ItemView> ChangeCurrentWeapon;
    public Action<ItemView> UpgradeButtonClick;

    public Item Item => ShopItem;

    public virtual void Initialize<Item>(Item item, Player player) { }
}
