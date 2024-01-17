using UnityEngine;
using UnityEngine.UI;

public class Shop : GamePanels
{
    [Header("[Shop View]")]
    [SerializeField] private ShopView _shopView;
    [Header("[Shop Panels]")]
    [SerializeField] private ArmorPanel _armorPanel;
    [SerializeField] private WeaponPanel _weaponPanel;
    [SerializeField] private ConsumablesPanel _consumablesPanel;
    [SerializeField] private AbilityPanel _abilityPanel;
    [SerializeField] private DialogPanel _dialogPanel;
    [Header("[Text]")]
    [SerializeField] private Text _coins;
    [SerializeField] private Text _points;
    [Header("[Buttons]")]
    [SerializeField] private Button _openPanel;
    [SerializeField] private Button _closePanel;

    protected DialogPanel DialogPanel => _dialogPanel;

    private void OnEnable()
    {
        _openPanel.onClick.AddListener(OpenPanel);
        _closePanel.onClick.AddListener(ClosePanel);
    }

    private void OnDisable()
    {
        _openPanel.onClick.RemoveListener(OpenPanel);
        _closePanel.onClick.RemoveListener(ClosePanel);
    }

    protected override void OpenPanel()
    {
        gameObject.SetActive(true);
        UpdateShopInfo();
        PanelOpened?.Invoke();
    }

    protected virtual void Initialized() { }

    protected virtual void OpenShopPanel()
    {
        CloseAllPanels();
        gameObject.SetActive(true);
    }

    protected void UpdatePlayerStats()
    {
        SetPlayerStats(Player.Wallet.GetCoins(), Player.PlayerStats.PlayerAbility.Points);
    }

    private void SetPlayerStats(int coins, int abilityPoints)
    {
        _coins.text = coins.ToString();
        _points.text = abilityPoints.ToString();
    }

    private void CloseAllPanels()
    {
        _consumablesPanel.gameObject.SetActive(false);
        _armorPanel.gameObject.SetActive(false);
        _weaponPanel.gameObject.SetActive(false);
        _abilityPanel.gameObject.SetActive(false);
    }

    private void UpdateShopInfo()
    {
        CloseAllPanels();
        FillingPanels();
        UpdatePlayerStats();
    }

    private void FillingPanels()
    {
        _shopView.AbilityPanel.Initialized();
        _shopView.ArmorPanel.Initialized();
        _shopView.WeaponPanel.Initialized();
        _shopView.ConsumablesPanel.Initialized();
    }
}