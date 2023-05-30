using UnityEngine;
using TMPro;

public class ShopPanel : MonoBehaviour
{
    [Header("[Panels]")]
    [SerializeField] private EquipmentPanel _equipmentPanel;
    [SerializeField] private ConsumablesPanel _consumablesPanel;
    [SerializeField] private ShopPanel _shopPanel;
    [SerializeField] private PauseMenuView _pauseMenuView;
    [Header("[Coins]")]
    [SerializeField] private TMP_Text _coins;

    protected Player Player;

    protected virtual void FillingItems(Player player) { }

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
    }

    protected void UpdateCoins()
    {
        _coins.text = Player.Coins.ToString();
    }

    private void UpdatePanelInfo()
    {
        CloseAllPanels();
        UpdateCoins();
        _equipmentPanel.FillingItems(Player);
    }
}