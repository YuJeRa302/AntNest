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

    protected ItemObject ItemObj;
    protected int ItemDataPrice;

    public Action<ItemView> BuyButtonClick;

    public ItemObject ItemObject => ItemObj;
    public int Price => ItemDataPrice;

    public virtual void Initialize(ItemData itemData, Player player) { }
}
