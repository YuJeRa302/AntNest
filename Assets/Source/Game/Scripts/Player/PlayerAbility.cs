using System.Collections.Generic;
using UnityEngine;

public class PlayerAbility : MonoBehaviour
{
    private readonly int _nullValue = 0;

    [Header("[Player Stats]")]
    [SerializeField] private PlayerStats _playerStats;
    [Header("[Ability]")]
    [SerializeField] private List<Ability> _abilities;

    private int _points = 0;
    private ParticleSystem _abilityEffect;

    public int NullValue => _nullValue;
    public int Points => _points;
    public List<Ability> Ability => _abilities;
    public ParticleSystem AbilityEffect => _abilityEffect;

    public List<Ability> GetListAbility()
    {
        return _abilities;
    }

    public void SetPoints(int value)
    {
        _points = value;
    }

    public void SetEffect(ParticleSystem abilityEffect)
    {
        _abilityEffect = abilityEffect;
    }

    public void GivePoints(int value)
    {
        _points = Mathf.Clamp(_points - value, _nullValue, _points);
    }
}
