public class ConsumablesView : ItemView
{
    private ConsumableItem _consumableItem;

    private void OnDestroy()
    {
        BuyButton.onClick.RemoveListener(OnButtonClick);
    }

    //public override void Initialize<Item>(Item item, Player player)
    //{
    //    _consumableItem = item as ConsumableItem;
    //    ItemDataPrice = _consumableItem.Price;
    //    AddListener();
    //    Fill(_consumableItem);
    //}

    private void Fill(ItemData itemData)
    {
        ItemName.TranslationName = itemData.Name;
        ItemPrice.text = itemData.Price.ToString();
        ItemIcon.sprite = itemData.ItemIcon;
    }

    private void AddListener()
    {
        BuyButton.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        BuyButtonClick?.Invoke(this);
    }
}
