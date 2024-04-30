using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAbility : MonoBehaviour
{
    [SerializeField] private Player _player;
    [Header("[Containers]")]
    [SerializeField] private Transform _abilityObjectContainer;
    [SerializeField] private Transform _playerEffectsContainer;
    [SerializeField] private Transform _weaponEffectsContainer;
    [Header("[AbilityReloadImage]")]
    [SerializeField] private Image _reloadingImage;

    private ParticleSystem _abilityEffect;
    private AbilityItemGameObject _abilityItemGameObject;

    public ParticleSystem AbilityEffect => _abilityEffect;
    public AbilityItemGameObject AbilityItemGameObject => _abilityItemGameObject;

    public void BuyAbility(AbilityState abilityState)
    {
        if (abilityState == null)
            return;

        abilityState.IsBuyed = true;

        if (_abilityItemGameObject != null)
            Destroy(_abilityItemGameObject.gameObject);

        if (_abilityEffect != null)
            Destroy(_abilityEffect.gameObject);

        if (abilityState.AbilityData.EffectType == TypeEffect.Weapon)
            _abilityEffect = Instantiate(abilityState.AbilityData.ParticleSystem, _weaponEffectsContainer);
        else _abilityEffect = Instantiate(abilityState.AbilityData.ParticleSystem, _playerEffectsContainer);

        _abilityItemGameObject = Instantiate(abilityState.AbilityData.ItemGameObject, _abilityObjectContainer);
        _abilityItemGameObject.Initialize(_player, abilityState, _reloadingImage, _abilityEffect);
    }

    public void UpgradeAbility(AbilityState abilityState) 
    {
        abilityState.CurrentLevel++;
        BuyAbility(abilityState);
    }
}
