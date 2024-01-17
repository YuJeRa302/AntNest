using System;
using UnityEngine;

public abstract class Equipment : MonoBehaviour
{
    [Header("[Equipment View]")]
    [SerializeField] private Sprite _sprite;
    [SerializeField] private int _price;
    [SerializeField] private bool _isBayed;
    [SerializeField] private int _value;
    [SerializeField] private int _level;
    [SerializeField] private string _name;

    public int Value => _value;
    public Sprite ItemIcon => _sprite;
    public string Name => _name;
    public int Price => _price;
    public int Level => _level;
    public bool IsBayed => _isBayed;

    public event Action<bool> OnChangeState;

    public void Increase(int increaseValue)
    {
        _value += increaseValue;
    }

    public void Decrease(int decreaseValue)
    {
        _value -= decreaseValue;
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