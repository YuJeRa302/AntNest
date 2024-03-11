using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable", menuName = "Create Consumable", order = 51)]
public class ConsumableItem : ItemData
{
    [Header("[Consumable Value]")]
    [SerializeField] private int _value;

    public int Value => _value;
}
