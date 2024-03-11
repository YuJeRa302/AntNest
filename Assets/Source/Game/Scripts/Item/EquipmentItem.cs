using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Create Equipment", order = 51)]
public class EquipmentItem : ItemData
{
    [Header("[Equipment Stats]")]
    [SerializeField] private int _value;
    [SerializeField] private int _level;
    [SerializeField] private Sprite _shopIcon;
    [Header("[Template]")]
    [SerializeField] private ItemObject _template;

    public int Value => _value;
    public int Level => _level;
    public Sprite ShopIcon => _shopIcon;
    public ItemObject Template => _template;

    private void Awake()
    {
        (_template as Equipment).LoadItemData(Value);
    }
}
