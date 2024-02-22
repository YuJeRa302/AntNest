using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityPanel : Shop
{
    [SerializeField] private Shop _shop;
    [SerializeField] private AbilityView _abilityView;
    [SerializeField] private GameObject _abilityContainer;
    [SerializeField] private Button _button;

    private List<Ability> _abilities;
    private List<AbilityView> _abilityViews = new();
    private Player _player;
    private LevelObserver _levelObserver;

    private void Awake()
    {
        //_shop.Initialized += OnShopInitialized;
    }

    private void OnDestroy()
    {
        // _button.onClick.RemoveListener(OpenShopTab);
        //_shop.Initialized -= OnShopInitialized;
        _levelObserver.GameClosed -= OnCloseGame;
    }

    //protected override void FillPanel()
    //{
    //    if (_abilities != null)
    //    {
    //        return;
    //    }
    //    else
    //    {
    //        _abilities = _player.PlayerStats.PlayerAbility.GetListAbility();

    //        for (int i = 0; i < _abilities.Count; i++)
    //        {
    //            AddAbility(_abilities[i]);
    //        }
    //    }
    //}

    private void OnShopInitialized(Player player, LevelObserver levelObserver)
    {
        _player = player;
        _levelObserver = levelObserver;
        //_button.onClick.AddListener(OpenShopTab);
        _levelObserver.GameClosed += OnCloseGame;
        //FillPanel();
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
        if (ability.Price <= _player.PlayerStats.PlayerAbility.Points)
        {
            _player.PlayerStats.PlayerAbility.BuyAbility(ability);
            ability.Buy();
            UpdatePlayerResource();
            view.SellButtonClick -= OnSellButton;
        }
        else DialogPanel.OpenPanel();
    }

    private void TryUpgradeAbility(Ability ability, AbilityView view)
    {
        if (ability.UpgradePrice <= _player.PlayerStats.PlayerAbility.Points)
        {
            _player.PlayerStats.PlayerAbility.UpgradeAbility(ability);
            ability.Upgrade();
            UpdatePlayerResource();
        }
        else DialogPanel.OpenPanel();

        if (ability.MaxLevel == _player.PlayerStats.PlayerAbility.Ability[0].CurrentLevel) view.UpgradeButtonClick -= OnUpgradeButton;
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