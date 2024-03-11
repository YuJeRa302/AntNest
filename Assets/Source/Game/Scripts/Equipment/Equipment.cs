using System;
using UnityEngine;

public abstract class Equipment : ItemObject
{
    [SerializeField] private int _value;

    public event Action<bool> ActiveStateChanged;

    public int Value => _value;

    public void LoadItemData(int value)
    {
        _value = value;
    }

    public void SetState(bool state)
    {
        gameObject.SetActive(state);
        ActiveStateChanged?.Invoke(state);
    }
}