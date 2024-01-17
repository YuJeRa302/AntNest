using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityPanel : Shop
{
    [SerializeField] private AbilityView _abilityView;
    [SerializeField] private GameObject _abilityContainer;
    [SerializeField] private Button _button;

    private List<Ability> _abilities;
    private List<AbilityView> _abilityViews;
    //private AbilityView[] _abilityViews;

    private void OnEnable()
    {
        _button.onClick.AddListener(OpenShopPanel);
        LevelObserver.GameClosed += OnCloseGame;
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OpenShopPanel);
        LevelObserver.GameClosed -= OnCloseGame;
    }

    protected override void Initialized()
    {
        if (_abilities != null)
        {
            return;
        }
        else
        {
            _abilities = Player.PlayerStats.PlayerAbility.GetListAbility();

            for (int i = 0; i < _abilities.Count; i++)
            {
                AddAbility(_abilities[i]);
            }
        }
    }

    private void AddAbility(Ability ability)
    {
        var view = Instantiate(_abilityView, _abilityContainer.transform);
        view.SellButtonClick += OnSellButton;
        view.UpgradeButtonClick += OnUpgradeButton;
        view.Render(ability);
        _abilityViews.Add(view);
    }

    private void OnSellButton(Ability ability, AbilityView view)
    {
        TrySellAbility(ability, view);
        LockAbility(view);
    }

    private void OnUpgradeButton(Ability ability, AbilityView view)
    {
        TryUpgradeAbility(ability, view);
    }

    private void TrySellAbility(Ability ability, AbilityView view)
    {
        if (ability.Price <= Player.PlayerStats.PlayerAbility.Points)
        {
            Player.PlayerStats.PlayerAbility.BuyAbility(ability);
            ability.Buy();
            UpdatePlayerStats();
            view.SellButtonClick -= OnSellButton;
        }
        else DialogPanel.Opened?.Invoke();
    }

    private void TryUpgradeAbility(Ability ability, AbilityView view)
    {
        if (ability.UpgradePrice <= Player.PlayerStats.PlayerAbility.Points)
        {
            Player.PlayerStats.PlayerAbility.UpgradeAbility(ability);
            ability.Upgrade();
            UpdatePlayerStats();
        }
        else DialogPanel.Opened?.Invoke();

        if (ability.MaxLevel == Player.PlayerStats.PlayerAbility.Ability[0].CurrentLevel) view.UpgradeButtonClick -= OnUpgradeButton;
    }

    //private void GetAbilitiesView()
    //{
    //    _abilityViews = new AbilityView[_abilityContainer.transform.childCount];

    //    for (int i = 0; i < _abilityContainer.transform.childCount; i++)
    //    {
    //        _abilityViews[i] = _abilityContainer.transform.GetChild(i).GetComponent<AbilityView>();
    //    }
    //}

    private void LockAbility(AbilityView abilityView)
    {
        //GetAbilitiesView();

        foreach (var ability in _abilityViews)
        {
            if (ability != abilityView) ability.LockedAbility();
        }
    }

    private void OnCloseGame()
    {
        if (_abilityViews.Count > 0)
        {
            foreach (var view in _abilityViews)
            {
                view.SellButtonClick -= OnSellButton;
                view.UpgradeButtonClick -= OnUpgradeButton;
            }
        }
    }
}