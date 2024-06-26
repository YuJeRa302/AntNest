using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class Ability : ItemGameObject
{
    protected Player Player;
    protected bool IsUseAbility = false;
    protected float CurrentDelay;
    protected int CurrentAbilityValue;

    private float _animationTime;
    private Image _reloadingImage;
    private AbilityItemData _abilityItemData;
    private ParticleSystem _particleSystem;
    private Coroutine _delay;

    public void Initialize(Player player, AbilityState abilityState, Image reloadingImage)
    {
        Player = player;
        _abilityItemData = abilityState.AbilityData;
        _particleSystem = abilityState.AbilityData.ParticleSystem;
        _reloadingImage = reloadingImage;
        _delay = StartCoroutine(Delay(_abilityItemData.CurrentDelay));
    }

    protected virtual void Use() { }

    protected void ApplyAbility(float delay)
    {
        _particleSystem.Play();
        Player.PlayerSounds.AudioPlayerAbility.PlayOneShot(_abilityItemData.Sound);
        UpdateAbility(true, delay);
        ResumeCooldown(delay);
    }

    private void ResumeCooldown(float delay)
    {
        if (gameObject.activeSelf == true)
        {
            if (_delay != null) StopCoroutine(_delay);

            _delay = StartCoroutine(Delay(delay));
        }
        else return;
    }

    private IEnumerator Delay(float delay)
    {
        _animationTime = delay;

        while (_animationTime > 0)
        {
            _animationTime -= Time.deltaTime;
            _reloadingImage.fillAmount = _animationTime / delay;
            yield return null;
        }

        UpdateAbility(false, 0);
    }

    private void UpdateAbility(bool state, float delay)
    {
        IsUseAbility = state;
        _reloadingImage.fillAmount = delay;
    }
}