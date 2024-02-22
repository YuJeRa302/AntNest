using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Create Weapon", order = 51)]
public class WeaponItem : Item
{
    [Header("[Weapon Stats]")]
    [SerializeField] private int _damage;
    [SerializeField] private int _weaponLevel;

    public int Damage => _damage;
    public int WeaponLevel => _weaponLevel;
}
