using UnityEngine;
using UnityEngine.Serialization;

public abstract class ItemData : ScriptableObject
{
    [SerializeField] private TypeItem _type;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private int _price;
    [FormerlySerializedAs("_isBayed")] [SerializeField] private bool _isBuyedByDefault;
    [SerializeField] private string _name;

    public Sprite ItemIcon => _sprite;
    public string Name => _name;
    public int Price => _price;
    public bool IsBuyedByDefault => _isBuyedByDefault;
    public TypeItem ItemType => _type;
}
