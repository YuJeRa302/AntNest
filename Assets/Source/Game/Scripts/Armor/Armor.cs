using UnityEngine;
using UnityEngine.Events;

public abstract class Armor : MonoBehaviour
{
    [Header("[Armor View]")]
    [SerializeField] private Sprite _sprite;
    [SerializeField] private int _price;
    [SerializeField] private bool _isBayed;
    [SerializeField] private int _armor;
    [SerializeField] private int _armorLevel;
    [Header("[Name]")]
    [SerializeField] private string _name;

    public int ItemArmor => _armor;
    public Sprite ItemIcon => _sprite;
    public string Name => _name;
    public int Price => _price;
    public int ArmorLevel => _armorLevel;
    public bool IsBayed => _isBayed;

    public event UnityAction<bool> OnChangeState;

    public void Increase(int armor)
    {
        _armor += armor;
    }

    public void Decrease(int armor)
    {
        _armor -= armor;
    }

    public void Buy()
    {
        _isBayed = true;
    }

    public void SetState(bool state)
    {
        gameObject.SetActive(state);
        OnChangeState?.Invoke(state);
    }
}