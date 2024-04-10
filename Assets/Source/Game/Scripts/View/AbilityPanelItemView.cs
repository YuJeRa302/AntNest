using UnityEngine;
using UnityEngine.UI;
using Lean.Localization;
using System;

public class AbilityPanelItemView : MonoBehaviour
{
    [Header("[Ability View]")]
    [SerializeField] private Text _itemPrice;
    [SerializeField] private Image _itemIcon;
    [SerializeField] private Button _buyButton;
    [SerializeField] private LeanLocalizedText _itemName;
    [SerializeField] private Text _currentDelay;
    [SerializeField] private Text _currentAbilityValue;
    [SerializeField] private Button _upgradeButton;
    [SerializeField] private Image _shopImage;
    [Header("[Ability Upgrade Param]")]
    [SerializeField] private Text _nextLevelDelay;
    [SerializeField] private Text _nextLevelAbilityValue;
    [Header("[Ability Upgrade Stats]")]
    [SerializeField] private GameObject _upgradeStats;
    [Header("[LeanLocalizedText]")]
    [SerializeField] private LeanLocalizedText _description;

    private AbilityState _abilityState;

    public event Action<AbilityPanelItemView> BuyButtonClick;
    public event Action<AbilityPanelItemView> UpgradeButtonClick;

    public AbilityState AbilityState => _abilityState;

    private void OnDestroy()
    {
        _buyButton.onClick.RemoveListener(OnButtonClick);
        _buyButton.onClick.RemoveListener(TryLockAbility);
        _upgradeButton.onClick.AddListener(OnUpgradeButtonClick);
    }

    public void Initialize(AbilityState abilityState, Player player)
    {
        _abilityState = abilityState;
        AddListener();
        Fill(abilityState);
        UpdateStats(_abilityState);
        CheckAbilityState(player);
        TryLockAbility();
    }

    public void Hover()
    {
        _upgradeStats.SetActive(true);
    }

    public void Exit()
    {
        _upgradeStats.SetActive(false);
    }

    private void Fill(AbilityState abilityState)
    {
        _itemPrice.text = abilityState.AbilityData.Price.ToString();
        _itemIcon.sprite = abilityState.AbilityData.ItemIcon;
        _itemName.TranslationName = abilityState.AbilityData.Name;
        _shopImage.sprite = abilityState.AbilityData.ShopSprite;
        _description.TranslationName = abilityState.AbilityData.Description;
        _currentDelay.text = abilityState.AbilityData.CurrentDelay.ToString();
        _currentAbilityValue.text = abilityState.AbilityData.CurrentAbilityValue.ToString();
    }

    private void AddListener()
    {
        _buyButton.onClick.AddListener(OnButtonClick);
        _buyButton.onClick.AddListener(TryLockAbility);
        _upgradeButton.onClick.AddListener(OnUpgradeButtonClick);
    }

    private void CheckAbilityState(Player player)
    {
        _buyButton.gameObject.SetActive(player.PlayerStats.PlayerAbility.AbilityItemGameObject == null);
    }

    private void TryLockAbility()
    {
        if (_abilityState.IsBuyed == false)
            return;

        _buyButton.gameObject.SetActive(false);
        _upgradeButton.gameObject.SetActive(true);
    }

    private void OnButtonClick()
    {
        BuyButtonClick?.Invoke(this);
    }

    private void OnUpgradeButtonClick()
    {
        UpgradeButtonClick?.Invoke(this);
        UpdateStats(_abilityState);
    }

    private void UpdateStats(AbilityState abilityState)
    {
        abilityState.AbilityData.GetNextLevelAbilityValue(abilityState.AbilityData.CurrentLevel, out string delay, out string abilityvalue);
        _currentDelay.text = abilityState.AbilityData.CurrentDelay.ToString();
        _currentAbilityValue.text = abilityState.AbilityData.CurrentAbilityValue.ToString();
        _nextLevelDelay.text = delay;
        _nextLevelAbilityValue.text = abilityvalue;
    }
}