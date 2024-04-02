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

    private ItemState _itemState;

    public Action<ItemView> BuyButtonClick;

    public ItemState ItemState => _itemState;

    public virtual void Initialize(ItemState itemState, Player player) { }
}