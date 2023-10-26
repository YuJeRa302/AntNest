using System.Collections.Generic;
using UnityEngine;

public class AbilityPanel : ShopPanel
{
    [Header("[Views]")]
    [SerializeField] private AbilityView _abilityView;
    [Header("[Containers]")]
    [SerializeField] private GameObject _abilityContainer;

    private List<Ability> _abilities;
    private PlayerAbility _playerAbility;
    private AbilityView[] _abilityViews;

    protected override void Filling(Player player)
    {
        _playerAbility = player.GetComponent<PlayerAbility>();

        if (_abilities != null)
        {
            return;
        }
        else
        {
            Player = player;
            _abilities = _playerAbility.GetListAbility();

            for (int i = 0; i < _abilities.Count; i++)
            {
                AddAbility(_abilities[i]);
            }
        }
    }

    public void AddAbility(Ability ability)
    {
        var view = Instantiate(_abilityView, _abilityContainer.transform);
        view.SellButtonClick += OnSellButton;
        view.UpgradeButtonClick += OnUpgradeButton;
        view.Render(ability);
    }

    public void OnSellButton(Ability ability, AbilityView view)
    {
        TrySellAbility(ability, view);
        LockAbility(view);
    }

    public void OnUpgradeButton(Ability ability, AbilityView view)
    {
        TryUpgradeAbility(ability, view);
    }

    public void TrySellAbility(Ability ability, AbilityView view)
    {
        if (ability.Price <= Player.PlayerStats.AbilityPoints)
        {
            _playerAbility.BuyAbility(ability);
            ability.Buy();
            UpdatePlayerStats();
            view.SellButtonClick -= OnSellButton;
        }
    }

    public void TryUpgradeAbility(Ability ability, AbilityView view)
    {
        if (ability.UpgradePrice <= Player.PlayerStats.AbilityPoints)
        {
            _playerAbility.UpgradeAbility(ability);
            ability.Upgrade();
            UpdatePlayerStats();
            view.UpgradeButtonClick -= OnUpgradeButton;
        }
    }

    public override void ClosePanel()
    {
        gameObject.SetActive(false);
    }

    private void GetAbilitiesView()
    {
        _abilityViews = new AbilityView[_abilityContainer.transform.childCount];

        for (int i = 0; i < _abilityContainer.transform.childCount; i++)
        {
            _abilityViews[i] = _abilityContainer.transform.GetChild(i).GetComponent<AbilityView>();
        }
    }

    private void LockAbility(AbilityView abilityView)
    {
        GetAbilitiesView();

        foreach (var ability in _abilityViews)
        {
            if (ability != abilityView)
            {
                ability.LockedAbility();
            }
        }
    }
}