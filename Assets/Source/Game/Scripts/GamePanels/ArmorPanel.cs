public class ArmorPanel : ShopTab
{
    //[SerializeField] private Shop _shop;
    //[SerializeField] private ArmorView _armorView;
    //[SerializeField] private GameObject _armorContainer;
    //[SerializeField] private Button _button;

    //private List<Armor> _armors;
    //private List<ArmorView> _armorViews = new();
    //private Player _player;
    //private LevelObserver _levelObserver;

    //private void Awake()
    //{
    //    _shop.Initialized += OnShopInitialized;
    //}

    //private void OnDestroy()
    //{
    //    _button.onClick.RemoveListener(OpenShopPanel);
    //    _shop.Initialized -= OnShopInitialized;
    //    _levelObserver.GameClosed -= OnCloseGame;
    //}

    //protected override void FillPanel()
    //{
    //    if (_armors != null)
    //    {
    //        return;
    //    }
    //    else
    //    {
    //        _armors = _player.PlayerStats.PlayerArmor.GetListArmor();
    //        AddEquipment(_armors);
    //    }

    //    TryUnlockBuyButton();
    //}

    //private void OnShopInitialized(Player player, LevelObserver levelObserver)
    //{
    //    _player = player;
    //    _levelObserver = levelObserver;
    //    _button.onClick.AddListener(OpenShopPanel);
    //    _levelObserver.GameClosed += OnCloseGame;
    //    FillPanel();
    //}

    //private void AddArmor(Armor armor)
    //{
    //    var view = Instantiate(_armorView, _armorContainer.transform);
    //    view.BuyButtonClick += OnBuyArmor;
    //    view.ChangeArmorButtonClick += OnChangeArmor;
    //    view.Render(armor);
    //    _armorViews.Add(view);
    //}

    //private void OnBuyArmor(Armor armor, ArmorView view)
    //{
    //    TryBuyArmor(armor, view);
    //}

    //private void OnChangeArmor(Armor armor)
    //{
    //    _player.PlayerStats.PlayerArmor.ChangeCurrentArmor(armor);
    //}

    //private void TryBuyArmor(Armor armor, ArmorView view)
    //{
    //    if (armor.Price <= _player.Wallet.Coins)
    //    {
    //        _player.PlayerStats.PlayerArmor.BuyArmor(armor);
    //        armor.Buy();
    //        view.BuyButtonClick -= OnBuyArmor;
    //        UpdatePlayerStats();
    //    }
    //    else DialogPanel.Open();
    //}

    //private void AddEquipment(List<Armor> armors)
    //{
    //    for (int i = 1; i < armors.Count; i++)
    //    {
    //        AddArmor(armors[i]);
    //    }
    //}

    //private void TryUnlockBuyButton()
    //{
    //    foreach (var view in _armorViews)
    //    {
    //        view.TryUnlockBuyButton(_player);
    //    }
    //}

    //private void OnCloseGame()
    //{
    //    if (_armorViews.Count > 0)
    //    {
    //        foreach (var view in _armorViews)
    //        {
    //            view.BuyButtonClick -= OnBuyArmor;
    //            view.ChangeArmorButtonClick -= OnChangeArmor;
    //        }
    //    }
    //}
}