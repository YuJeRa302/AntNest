using System.Collections.Generic;
using UnityEngine;

public class PlayerArmor : MonoBehaviour
{
    private readonly int _minValue = 0;

    [SerializeField] private Transform _armorsTransform;
    [SerializeField] private List<Armor> _armor;
    [SerializeField] private Player _player;

    private Armor _currentArmor;
    private int _abilityArmor;
    private int _numberDamageBlocks;

    public Armor CurrentArmor => _currentArmor;
    public int AbilityArmor => _abilityArmor;

    public void Initialize()
    {
        AddItemToList();
        _currentArmor = _armor[0];
    }

    public void ChangeCurrentArmor(Armor armor)
    {
        _currentArmor.Item.SetState(false);
        armor.Item.SetState(true);
        _currentArmor = armor;
        _player.PlayerView.UpdatePlayerStats();
    }

    public List<Armor> GetListArmor()
    {
        return _armor;
    }

    public void Increase(int armor, int numberDamageBlocks, ParticleSystem effect)
    {
        _player.PlayerStats.PlayerAbility.SetEffect(effect);
        _numberDamageBlocks = numberDamageBlocks;
        _abilityArmor = armor;
        //_currentArmor.Increase(_abilityArmor);
        _player.PlayerView.UpdatePlayerStats();
    }

    public void UpdateArmor()
    {
        _numberDamageBlocks--;

        if (_numberDamageBlocks <= _minValue) Decrease();
        else return;
    }

    private void Decrease()
    {
        //_currentArmor.Decrease(_abilityArmor);
        _abilityArmor = _minValue;
        _player.PlayerStats.PlayerAbility.AbilityEffect.Stop();
        _player.PlayerView.UpdatePlayerStats();
    }

    private void AddItemToList()
    {
        for (int i = 0; i < _armorsTransform.childCount; i++)
        {
            _armor.Add(_armorsTransform.GetChild(i).GetComponent<Helmet>());
        }
    }
}
