using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable Item", menuName = "Create Consumable Item", order = 51)]
public class ConsumableItemData : ItemData
{
    [Header("[Consumable Value]")]
    [SerializeField] private int _value;

    public int Value => _value;
}

[Serializable]
public class ConsumableItemState
{
    public ConsumableItemData ConsumableItemData;
}