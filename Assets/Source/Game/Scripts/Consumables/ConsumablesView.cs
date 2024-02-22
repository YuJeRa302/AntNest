public class ConsumablesView : ItemView
{
    private Consumables _consumables;

    private void OnDestroy()
    {
        BuyButton.onClick.RemoveListener(OnButtonClick);
    }

    public override void Initialize<Item>(Item item, Player player)
    {
        _consumables = item as Consumables;
        ShopItem = _consumables.Item;
        AddListener();
        Fill(_consumables);
    }

    private void Fill(Consumables consumables)
    {
        _consumables = consumables;
        ItemName.TranslationName = _consumables.Name;
        ItemPrice.text = _consumables.Price.ToString();
        ItemIcon.sprite = _consumables.ItemIcon;
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
