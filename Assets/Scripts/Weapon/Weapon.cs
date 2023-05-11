using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private int _price;
    [SerializeField] private bool _isBayed;
    [SerializeField] private int _damage;

    public int Damage => _damage;
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