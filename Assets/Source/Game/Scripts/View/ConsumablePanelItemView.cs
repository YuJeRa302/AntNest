using Lean.Localization;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ConsumablePanelItemView : MonoBehaviour
{
    [SerializeField] private Text _itemPrice;
    [SerializeField] private Text _itemValue;
    [SerializeField] private Image _itemIcon;
    [SerializeField] private Image _shopIcon;
    [SerializeField] private Button _buyButton;
    [SerializeField] private LeanLocalizedText _itemName;

    private ConsumableItemState _consumableItemState;

    public event Action<ConsumablePanelItemView> BuyButtonClicked;

    public ConsumableItemState ConsumableItemState => _consumableItemState;

    private void OnDestroy()
    {
        _buyButton.onClick.RemoveListener(OnButtonClick);
    }

    public void Initialize(ConsumableItemState consumableItemState)
    {
        _consumableItemState = consumableItemState;
        AddListener();
        Fill(consumableItemState);
    }

    private void Fill(ConsumableItemState consumableItemState)
    {
        _itemName.TranslationName = consumableItemState.ConsumableItemData.Name;
        _itemPrice.text = consumableItemState.ConsumableItemData.Price.ToString();
        _itemValue.text = consumableItemState.ConsumableItemData.Value.ToString();
        _shopIcon.sprite = consumableItemState.ConsumableItemData.SpriteShopItem;
        _itemIcon.sprite = consumableItemState.ConsumableItemData.ItemIcon;
    }

    private void AddListener()
    {
        _buyButton.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        BuyButtonClicked?.Invoke(this);
    }
}
