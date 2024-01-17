using UnityEngine;
using UnityEngine.UI;
using System;
using Lean.Localization;

public class AbilityView : MonoBehaviour
{
    [Header("[Ability View]")]
    [SerializeField] private Text _price;
    [SerializeField] private Text _currentDelay;
    [SerializeField] private Text _currentAbilityValue;
    [SerializeField] private Image _iconAbility;
    [SerializeField] private Button _sellButton;
    [SerializeField] private Button _upgradeButton;
    [SerializeField] private Image _abilityValueImage;
    [Header("[Ability Upgrade Param]")]
    [SerializeField] private Text _nextLevelDelay;
    [SerializeField] private Text _nextLevelAbilityValue;
    [Header("[Ability Upgrade Stats]")]
    [SerializeField] private GameObject _upgradeStats;
    [Header("[LeanLocalizedText]")]
    [SerializeField] private LeanLocalizedText _description;
    [SerializeField] private LeanLocalizedText _name;

    private Ability _ability;

    public Action<Ability, AbilityView> SellButtonClick;
    public Action<Ability, AbilityView> UpgradeButtonClick;

    private void OnEnable()
    {
        _sellButton.onClick.AddListener(OnButtonClick);
        _sellButton.onClick.AddListener(TryLockItem);
        _upgradeButton.onClick.AddListener(OnUpgradeButtonClick);
    }

    private void OnDisable()
    {
        _sellButton.onClick.RemoveListener(OnButtonClick);
        _sellButton.onClick.RemoveListener(TryLockItem);
        _upgradeButton.onClick.RemoveListener(OnUpgradeButtonClick);
    }

    public void Render(Ability ability)
    {
        ability.UpdateAbility(ability.CurrentLevel);
        _ability = ability;
        _price.text = ability.Price.ToString();
        _iconAbility.sprite = ability.AbilityIcon;
        _abilityValueImage.sprite = ability.AbilityValueSprite;
        _description.TranslationName = ability.Description;
        _name.TranslationName = ability.Name;
        UpdateStats(_ability);
    }

    public void LockedAbility()
    {
        _sellButton.gameObject.SetActive(false);
    }

    public void Hover()
    {
        _upgradeStats.SetActive(true);
    }

    public void Exit()
    {
        _upgradeStats.SetActive(false);
    }

    private void TryLockItem()
    {
        if (_ability.IsBayed)
        {
            _sellButton.gameObject.SetActive(false);
            _upgradeButton.gameObject.SetActive(true);
        }
    }

    private void OnButtonClick()
    {
        SellButtonClick?.Invoke(_ability, this);
        UpdateStats(_ability);
    }

    private void OnUpgradeButtonClick()
    {
        UpgradeButtonClick?.Invoke(_ability, this);
        UpdateStats(_ability);
    }

    private void UpdateStats(Ability ability)
    {
        ability.GetNextLevelParam(ability.CurrentLevel, out string delay, out string abilityvalue);
        _currentDelay.text = ability.CurrentDelay.ToString();
        _currentAbilityValue.text = ability.CurrentAbilityValue.ToString();
        _nextLevelDelay.text = delay;
        _nextLevelAbilityValue.text = abilityvalue;
    }
}