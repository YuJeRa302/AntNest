using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    private readonly int _minValue = 0;

    [SerializeField] private Transform _weaponsTransform;
    [SerializeField] private List<Weapon> _weapons;
    [SerializeField] private Player _player;

    private Weapon _currentWeapon;
    private int _abilityDamage;
    private int _hitCount;

    public Weapon CurrentWeapon => _currentWeapon;
    public int AbilityDamage => _abilityDamage;

    public void Initialize()
    {
        AddItemToList();
        _currentWeapon = _weapons[0];
    }

    public void ChangeCurrentWeapon(Weapon weapon)
    {
        _currentWeapon.SetState(false);
        weapon.SetState(true);
        _currentWeapon = weapon;
        _player.PlayerView.UpdatePlayerStats();
    }

    public List<Weapon> GetListWeapon()
    {
        return _weapons;
    }

    public void Increase(int damage, int hitCount, ParticleSystem effect)
    {
        _player.PlayerStats.PlayerAbility.SetEffect(effect);
        _hitCount = hitCount;
        _abilityDamage = damage;
         //_currentWeapon.Increase(_abilityDamage);
        _player.PlayerView.UpdatePlayerStats();
    }

    public void UpdateWeapon()
    {
        _hitCount--;

        if (_hitCount <= _minValue) Decrease();
        else return;
    }

    private void Decrease()
    {
        // _currentWeapon.Decrease(_abilityDamage);
        _abilityDamage = _minValue;
        _player.PlayerStats.PlayerAbility.AbilityEffect.Stop();
        _player.PlayerView.UpdatePlayerStats();
    }

    private void AddItemToList()
    {
        for (int i = 0; i < _weaponsTransform.childCount; i++)
        {
            _weapons.Add(_weaponsTransform.GetChild(i).GetComponent<Weapon>());
        }
    }
}
