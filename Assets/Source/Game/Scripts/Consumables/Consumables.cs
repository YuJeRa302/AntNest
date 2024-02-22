using UnityEngine;

public class Consumables : MonoBehaviour
{
    [SerializeField] private ConsumableItem _consumableItem;

    public ConsumableItem Item => _consumableItem;
    public Sprite ItemIcon => _consumableItem.ItemIcon;
    public string Name => _consumableItem.Name;
    public int Price => _consumableItem.Price;
}