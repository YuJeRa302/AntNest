using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable", menuName = "Create Consumable", order = 51)]
public class ConsumableItem : Item
{
    [Header("[Consumable Value]")]
    [SerializeField] private int _value;

    public int Value => _value;

    public event Action ItemBuyed;

    public override void Buy()
    {
        ItemBuyed?.Invoke();
    }
}
