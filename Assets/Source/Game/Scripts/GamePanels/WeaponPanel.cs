public class WeaponPanel : ShopTab
{
    //[SerializeField] private Shop _shop;
    //[SerializeField] private WeaponView _weaponView;
    //[SerializeField] private GameObject _weaponContainer;
    //[SerializeField] private Button _button;

    //private Player _player;

    //private void Awake()
    //{
    //    _shop.Initialized += OnShopInitialized;
    //    _shop.PanelClosed += OnCloseShop;
    //}

    //private void OnDestroy()
    //{
    //    _button.onClick.RemoveListener(OpenShopPanel);
    //    _shop.Initialized -= OnShopInitialized;
    //    _shop.PanelClosed -= OnCloseShop;
    //}

    //protected override void FillPanel()
    //{
    //    _weapons = _player.PlayerStats.PlayerDamage.GetListWeapon();
    //    AddEquipment(_weapons);
    //}

    //private void OnShopInitialized(Player player, LevelObserver levelObserver)
    //{
    //    _player = player;
    //    _button.onClick.AddListener(OpenShopPanel);
    //    FillPanel();
    //}

    //private void AddWeapon(Weapon weapon)
    //{
    //    var view = Instantiate(_weaponView, _weaponContainer.transform);
    //    view.BuyButtonClick += OnBuyWeapon;
    //    view.ChangeWeaponButtonClick += OnChangeWeapon;
    //    view.Initialize(weapon, _player);
    //    _weaponViews.Add(view);
    //}

    //private void AddEquipment(List<Weapon> weapons)
    //{
    //    for (int i = 0; i < weapons.Count; i++)
    //    {
    //        AddWeapon(weapons[i]);
    //    }
    //}

    //private void OnBuyWeapon(Weapon weapon, WeaponView weaponView)
    //{
    //    TryBuyWeapon(weapon, weaponView);
    //}

    //private void OnChangeWeapon(Weapon weapon)
    //{
    //    _player.PlayerStats.PlayerDamage.ChangeCurrentWeapon(weapon);
    //}

    //private void OnCloseShop()
    //{
    //    foreach (var view in _weaponViews)
    //    {
    //        view.BuyButtonClick -= OnBuyWeapon;
    //        view.ChangeWeaponButtonClick -= OnChangeWeapon;
    //        Destroy(view.gameObject);
    //    }

    //    _weaponViews.Clear();
    //}

    //private void TryBuyWeapon(Weapon weapon, WeaponView weaponView)
    //{
    //    if (weapon.Price <= _player.Wallet.Coins)
    //    {
    //        _player.PlayerStats.PlayerDamage.BuyWeapon(weapon);
    //        //weapon.Buy();
    //        weaponView.BuyButtonClick -= OnBuyWeapon;
    //        UpdatePlayerStats();
    //    }
    //    else DialogPanel.Open();
    //}
}