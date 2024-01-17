using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Ability : MonoBehaviour
{
    protected Player Player;
    protected bool IsUseAbility = false;

    private readonly string _maxLevel = "MAX";

    [Header("[Ability View]")]
    [SerializeField] private Sprite _sprite;
    [SerializeField] private Sprite _abilityValueSprite;
    [SerializeField] private int _price;
    [SerializeField] private int _upgradePrice;
    [SerializeField] private bool _isBayed;
    [Header("[Level Ability]")]
    [SerializeField] private List<AbilityLevel> _abilityLevels = new();
    [Header("[Current Ability Stats]")]
    [SerializeField] private int _currentLevel = 0;
    [SerializeField] private float _currentDelay = 0;
    [SerializeField] private int _currentAbilityValue = 0;
    [Header("[CoolDown Image]")]
    [SerializeField] private Image _coolDown;
    [Header("[Description]")]
    [SerializeField] private string _description;
    [Header("[Name]")]
    [SerializeField] private string _name;
    [Header("[Effect]")]
    [SerializeField] private ParticleSystem _effect;
    [Header("[Sound]")]
    [SerializeField] private AudioClip _sound;

    private float _animationTime;
    private Coroutine _delay;

    public ParticleSystem Effect => _effect;
    public Sprite AbilityIcon => _sprite;
    public Sprite AbilityValueSprite => _abilityValueSprite;
    public int Price => _price;
    public int UpgradePrice => _upgradePrice;
    public float CurrentDelay => _currentDelay;
    public int CurrentAbilityValue => _currentAbilityValue;
    public int CurrentLevel => _currentLevel;
    public int MaxLevel => _abilityLevels.Count;
    public bool IsBayed => _isBayed;
    public string Description => _description;
    public string Name => _name;
    public float AnimationTime => _animationTime;

    public void Buy()
    {
        gameObject.SetActive(true);
        UpdateAbility(++_currentLevel);
        _isBayed = true;
    }

    public void Upgrade()
    {
        UpdateAbility(++_currentLevel);
    }

    public void UpdateAbility(int currentLevel)
    {
        foreach (var ability in _abilityLevels)
        {
            if (ability.Level == currentLevel)
            {
                _currentDelay = ability.Delay;
                _currentAbilityValue = ability.AbilityValue;
            }
        }
    }

    public void GetNextLevelParam(int currentLevel, out string delay, out string abilityValue)
    {
        if (_abilityLevels.Count > currentLevel)
        {
            delay = _abilityLevels[currentLevel].Delay.ToString();
            abilityValue = _abilityLevels[currentLevel].AbilityValue.ToString();
        }
        else
        {
            delay = _maxLevel;
            abilityValue = _maxLevel;
        }
    }

    public void ResumeCooldown(float delay)
    {
        if (gameObject.activeSelf == true)
        {
            if (_delay != null) StopCoroutine(_delay);

            _delay = StartCoroutine(Delay(delay));
        }
        else return;
    }

    protected virtual void Use() { }

    protected void ApplyAbility(float delay)
    {
        SetAbilityParameters(delay);
        ResumeCooldown(delay);
    }

    private void Start()
    {
        _delay = StartCoroutine(Delay(_currentDelay));
        Player = FindObjectOfType<Player>();
    }

    private IEnumerator Delay(float delay)
    {
        _animationTime = delay;

        while (_animationTime > 0)
        {
            _animationTime -= Time.deltaTime;
            _coolDown.fillAmount = _animationTime / delay;
            yield return null;
        }

        UpdateAbility(false, 0);
    }

    private void UpdateAbility(bool state, float delay)
    {
        IsUseAbility = state;
        _coolDown.fillAmount = delay;
    }

    private void SetAbilityParameters(float delay)
    {
        UpdateAbility(true, delay);
        _effect.Play();
        Player.PlayerSounds.AudioPlayerAbility.PlayOneShot(_sound);
    }
}