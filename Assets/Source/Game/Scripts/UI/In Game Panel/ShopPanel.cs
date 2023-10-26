using UnityEngine;
using UnityEngine.UI;

public class ShopPanel : MonoBehaviour
{
    [Header("[Panels]")]
    [SerializeField] private EquipmentPanel _equipmentPanel;
    [SerializeField] private ConsumablesPanel _consumablesPanel;
    [SerializeField] private AbilityPanel _abilityPanel;
    [SerializeField] private ShopPanel _shopPanel;
    [SerializeField] private PauseMenuView _pauseMenuView;
    [SerializeField] private DialogPanel _dialogPanel;
    [Header("[Coins]")]
    [SerializeField] private Text _coins;
    [Header("[Level Points]")]
    [SerializeField] private Text _points;

    protected Player Player;
    protected DialogPanel DialogPanel => _dialogPanel;

    protected virtual void Filling(Player player) { }

    public void OpenShopPanel()
    {
        Player = FindObjectOfType<Player>();
        UpdatePanelInfo();
        _pauseMenuView.gameObject.SetActive(false);
        gameObject.SetActive(true);
    }

    public void CloseShopPanel()
    {
        _pauseMenuView.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public virtual void OpenPanel()
    {
        CloseAllPanels();
        gameObject.SetActive(true);
    }

    public virtual void ClosePanel()
    {
        CloseAllPanels();
        gameObject.SetActive(false);
    }

    protected void CloseAllPanels()
    {
        _equipmentPanel.gameObject.SetActive(false);
        _consumablesPanel.gameObject.SetActive(false);
        _equipmentPanel.ArmorPanel.gameObject.SetActive(false);
        _equipmentPanel.WeaponPanel.gameObject.SetActive(false);
        _abilityPanel.gameObject.SetActive(false);
    }

    protected void UpdatePlayerStats()
    {
        _coins.text = Player.Coins.ToString();
        _points.text = Player.PlayerStats.AbilityPoints.ToString();
    }

    private void UpdatePanelInfo()
    {
        CloseAllPanels();
        UpdatePlayerStats();
        _equipmentPanel.Filling(Player);
        _abilityPanel.Filling(Player);
    }
}