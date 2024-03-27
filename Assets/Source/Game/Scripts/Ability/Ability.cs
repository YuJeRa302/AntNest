using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class Ability : ItemGameObject
{
    private readonly string _maxLevel = "MAX";

    protected Player Player;
    protected bool IsUseAbility = false;

    [SerializeField] private AbilityItem _abilityItem;
    [SerializeField] private Image _reloadingImage;
    [SerializeField] private ParticleSystem _effect;

    private float _animationTime;
    private Coroutine _delay;

    public AbilityItem Item => _abilityItem;
    public Sprite Icon => _abilityItem.ItemIcon;
    public Sprite ShopSprite => _abilityItem.ShopSprite;
    public int Price => _abilityItem.Price;
    public int UpgradePrice => _abilityItem.UpgradePrice;
    public float CurrentDelay => _abilityItem.CurrentDelay;
    public int CurrentAbilityValue => _abilityItem.CurrentAbilityValue;
    public int CurrentLevel => _abilityItem.CurrentLevel;
    public int MaxLevel => _abilityItem.AbilityLevels.Count;
    public bool IsBayed => _abilityItem.IsBuyedByDefault;
    public string Description => _abilityItem.Description;
    public string Name => _abilityItem.Name;
    public float AnimationTime => _animationTime;

    public void Initialize(Player player)
    {
        Player = player;
        gameObject.SetActive(true);
        _delay = StartCoroutine(Delay(_abilityItem.CurrentDelay));
    }

    //public void Upgrade()
    //{
    //    UpdateAbility(++_currentLevel);
    //}

    //public void GetNextLevelParam(int currentLevel, out string delay, out string abilityValue)
    //{
    //    if (_abilityLevels.Count > currentLevel)
    //    {
    //        delay = _abilityLevels[currentLevel].Delay.ToString();
    //        abilityValue = _abilityLevels[currentLevel].AbilityValue.ToString();
    //    }
    //    else
    //    {
    //        delay = _maxLevel;
    //        abilityValue = _maxLevel;
    //    }
    //}

    //private void UpdateAbility(int currentLevel)
    //{
    //    foreach (var ability in _abilityLevels)
    //    {
    //        if (ability.Level == currentLevel)
    //        {
    //            _currentDelay = ability.Delay;
    //            _currentAbilityValue = ability.AbilityValue;
    //        }
    //    }
    //}

    protected virtual void Use() { }

    protected void ApplyAbility(float delay)
    {
        _effect.Play();
        Player.PlayerSounds.AudioPlayerAbility.PlayOneShot(_abilityItem.Sound);
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