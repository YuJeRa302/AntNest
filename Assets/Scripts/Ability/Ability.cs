using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private int _price;
    [SerializeField] private bool _isBayed;

    public Sprite AbilityIcon => _sprite;
    public string Name => _name;
    public int Price => _price;
    public bool IsBayed => _isBayed;
    public int Value { get; private set; }

    public void Buy()
    {
        this.gameObject.SetActive(true);
        _isBayed = true;
    }

    protected void SetAbilityValue(int value)
    {
        Value = value;
    }
}