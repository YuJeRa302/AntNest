using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbility : MonoBehaviour
{
    [Header("[Player]")]
    [SerializeField] private Player _player;
    [Header("[Player Stats]")]
    [SerializeField] private PlayerStats _playerStats;
    [Header("[Ability]")]
    [SerializeField] private List<Ability> _abilities;

    public void BuyAbility(Ability ability)
    {
        if (_playerStats.AbilityPoints >= ability.Price)
        {
            _abilities.Add(ability);
            _playerStats.GivePoints(ability.Price);
        }
        else return;
    }

    public void UpgradeAbility(Ability ability)
    {
        if (_playerStats.AbilityPoints >= ability.UpgradePrice)
        {
            _playerStats.GivePoints(ability.UpgradePrice);
        }
        else return;
    }

    public List<Ability> GetListAbility()
    {
        return _abilities;
    }
}
