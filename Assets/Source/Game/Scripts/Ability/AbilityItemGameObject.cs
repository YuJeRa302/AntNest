using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AbilityItemGameObject : ItemGameObject
{
    [SerializeField] private Button _useAbilityButton;

    protected Player Player;
    protected bool IsUseAbility = true;
    protected float CurrentDelay;
    protected float CurrentDuration;
    protected int CurrentAbilityValue;
    protected TypeAbility TypeAbility;

    private float _animationTime;
    private Image _reloadingImage;
    private AbilityItemData _abilityItemData;
    private ParticleSystem _particleSystem;
    private Coroutine _delay;

    private void Awake()
    {
        _useAbilityButton.onClick.AddListener(Use);
    }

    private void OnEnable()
    {
        _delay = StartCoroutine(Delay(CurrentDelay));
    }

    private void OnDestroy()
    {
        _useAbilityButton.onClick.RemoveListener(Use);
        StopCoroutine(_delay);
    }

    public void Initialize(Player player, AbilityState abilityState, Image reloadingImage, ParticleSystem particleSystem)
    {
        Player = player;
        CurrentDelay = abilityState.AbilityData.AbilityLevels[abilityState.CurrentLevel].Delay;
        CurrentAbilityValue = abilityState.AbilityData.AbilityLevels[abilityState.CurrentLevel].AbilityValue;
        TypeAbility = abilityState.AbilityData.TypeAbility;
        CurrentDuration = abilityState.AbilityData.AbilityDuration;
        _abilityItemData = abilityState.AbilityData;
        _particleSystem = particleSystem;
        _reloadingImage = reloadingImage;
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