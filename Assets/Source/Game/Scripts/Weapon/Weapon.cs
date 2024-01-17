using System;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("[Stats]")]
    [SerializeField] private Sprite _sprite;
    [SerializeField] private int _price;
    [SerializeField] private bool _isBayed;
    [SerializeField] private int _damage;
    [SerializeField] private int _weaponLevel;
    [Header("[Name]")]
    [SerializeField] private string _name;

    public int Damage => _damage;
    public Sprite ItemIcon => _sprite;
    public string Name => _name;
    public int Price => _price;
    public bool IsBayed => _isBayed;
    public int WeaponLevel => _weaponLevel;

    public event Action<bool> OnChangeState;

    public void Increase(int damage)
    {
        _damage += damage;
    }

    public void Decrease(int damage)
    {
        _damage -= damage;
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