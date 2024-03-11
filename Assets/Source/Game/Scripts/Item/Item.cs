using UnityEngine;

public abstract class Item : ScriptableObject
{
    [SerializeField] private Sprite _sprite;
    [SerializeField] private int _price;
    [SerializeField] private bool _isBayed;
    [SerializeField] private string _name;

    public Sprite ItemIcon => _sprite;
    public string Name => _name;
    public int Price => _price;
    public bool IsBayed => _isBayed;

    public virtual void Buy()
    {
        _isBayed = true;
    }

    public virtual void Upgrade() { }
}
