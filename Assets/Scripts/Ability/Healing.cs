using UnityEngine;

public class Healing : Ability
{
    [SerializeField] private int _health = 50;

    private void Start()
    {
        SetAbilityValue(_health);
    }
}