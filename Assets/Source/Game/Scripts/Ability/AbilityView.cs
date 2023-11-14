using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
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
    [Header("[Description]")]
    [SerializeField] private LeanLocalizedText _description;
    [Header("[Name]")]
    [SerializeField] private LeanLocalizedText _name;

    private Ability _ability;

    public event UnityAction<Ability, AbilityView> SellButtonClick;
    public event UnityAction<Ability, AbilityView> UpgradeButtonClick;

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

    public void TryLockItem()
    {
        if (_ability.IsBayed)
        {
            _sellButton.gameObject.SetActive(false);
            _upgradeButton.gameObject.SetActive(true);
        }
    }

    public void OnButtonClick()
    {
        SellButtonClick?.Invoke(_ability, this);
        UpdateStats(_ability);
    }

    public void OnUpgradeButtonClick()
    {
        UpgradeButtonClick?.Invoke(_ability, this);
        UpdateStats(_ability);
    }

    public void Hover()
    {
        _upgradeStats.SetActive(true);
    }

    public void Exit()
    {
        _upgradeStats.SetActive(false);
    }

    public void LockedAbility()
    {
        _sellButton.gameObject.SetActive(false);
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