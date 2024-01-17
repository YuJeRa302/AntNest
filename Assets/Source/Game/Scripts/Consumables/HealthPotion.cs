using UnityEngine;

public class HealthPotion : Consumables
{
    [Header("[Healing Value]")]
    [SerializeField] private int _healing;

    public int Healing => _healing;
}
