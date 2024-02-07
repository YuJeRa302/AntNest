using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponItem _weaponItem;

    public WeaponItem Item => _weaponItem;
    public int Damage => _weaponItem.Damage;
    public Sprite ItemIcon => _weaponItem.ItemIcon;
    public string Name => _weaponItem.Name;
    public int Price => _weaponItem.Price;
    public bool IsBayed => _weaponItem.IsBayed;
    public int Level => _weaponItem.WeaponLevel;

    public event Action<bool> OnChangeState;

    public void SetState(bool state)
    {
        OnChangeState?.Invoke(state);
        gameObject.SetActive(state);
    }
}