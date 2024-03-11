using UnityEngine;
using UnityEngine.UI;
using Lean.Localization;
using System;

public class AbilityView : ItemView
{
    [Header("[Ability View]")]
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

    private Ability _ability;
    private Player _player;

    public event Action<ItemView> UpgradeButtonClick;

    public Ability Ability => _ability;

    //private void OnDestroy()
    //{
    //    BuyButton.onClick.RemoveListener(OnButtonClick);
    //    BuyButton.onClick.RemoveListener(TryLockItem);
    //    _upgradeButton.onClick.AddListener(OnUpgradeButtonClick);
    //}

    //public override void Initialize<Item>(Item item, Player player)
    //{
    //    _player = player;
    //    _ability = item as Ability;
    //    ItemDataPrice = _ability.Price;
    //    AddListener();
    //    Fill(_ability);
    //    TryLockItem();
    //    UpdateStats(_ability);
    //}

    //public void Hover()
    //{
    //    _upgradeStats.SetActive(true);
    //}

    //public void Exit()
    //{
    //    _upgradeStats.SetActive(false);
    //}

    //public void LockedAbility()
    //{
    //    BuyButton.gameObject.SetActive(false);
    //}

    //private void Fill(Ability ability)
    //{
    //    _ability = ability;
    //    ItemPrice.text = ability.Price.ToString();
    //    ItemName.TranslationName = ability.Name;
    //    _shopImage.sprite = ability.ShopSprite;
    //    _description.TranslationName = ability.Description;
    //    _currentDelay.text = ability.CurrentDelay.ToString();
    //    _currentAbilityValue.text = ability.CurrentAbilityValue.ToString();
    //}

    //private void AddListener()
    //{
    //    BuyButton.onClick.AddListener(OnButtonClick);
    //    BuyButton.onClick.AddListener(TryLockItem);
    //    _upgradeButton.onClick.AddListener(OnUpgradeButtonClick);
    //}

    //private void TryLockItem()
    //{
    //    if (_ability.IsBayed)
    //    {
    //        BuyButton.gameObject.SetActive(false);
    //        _upgradeButton.gameObject.SetActive(true);
    //        _ability.Initialize(_player);
    //    }
    //}

    //private void OnButtonClick()
    //{
    //    BuyButtonClick?.Invoke(this);
    //    UpdateStats(_ability);
    //}

    //private void OnUpgradeButtonClick()
    //{
    //    UpgradeButtonClick?.Invoke(this);
    //    UpdateStats(_ability);
    //}

    //private void UpdateStats(Ability ability)
    //{
    //   // ability.GetNextLevelParam(ability.CurrentLevel, out string delay, out string abilityvalue);
    //    _currentDelay.text = ability.CurrentDelay.ToString();
    //    _currentAbilityValue.text = ability.CurrentAbilityValue.ToString();
    //    //_nextLevelDelay.text = delay;
    //   // _nextLevelAbilityValue.text = abilityvalue;
    //}
}