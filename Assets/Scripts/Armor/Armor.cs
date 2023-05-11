using UnityEngine;

public abstract class Armor : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private int _price;
    [SerializeField] private bool _isBayed;
    [SerializeField] private int _armor;

    public int ItemArmor => _armor;
    public Sprite ItemIcon => _sprite;
    public string Name => _name;
    public int Price => _price;
    public bool IsBayed => _isBayed;

    public void Buy()
    {
        this.gameObject.SetActive(true);
        _isBayed = true;
    }
}