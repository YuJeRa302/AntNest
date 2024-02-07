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
    public Action<ItemView> ChangeItemButtonClick;
    public Action<ItemView> UpgradeButtonClick;

    public Item Item => ShopItem;

    public virtual void Initialize<Item>(Item item, Player player) { }
}
