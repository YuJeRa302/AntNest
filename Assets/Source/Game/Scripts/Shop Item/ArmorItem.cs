using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Armor", menuName = "Create Armor", order = 51)]
public class ArmorItem : Item
{
    [Header("[Armor Stats]")]
    [SerializeField] private int _armor;
    [SerializeField] private int _armorLevel;

    public int ItemArmor => _armor;
    public int ArmorLevel => _armorLevel;

    public event Action<bool> OnChangeState;

    public void SetState(bool state)
    {
        OnChangeState?.Invoke(state);
    }
}
