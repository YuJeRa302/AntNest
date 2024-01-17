using UnityEngine;
using UnityEngine.UI;

public class ShopView : MonoBehaviour
{
    [Header("[Panels]")]
    [SerializeField] private ArmorPanel _armorPanel;
    [SerializeField] private WeaponPanel _weaponPanel;
    [SerializeField] private ConsumablesPanel _consumablesPanel;
    [SerializeField] private AbilityPanel _abilityPanel;
    [SerializeField] private PauseMenuView _pauseMenuView;
    [SerializeField] private DialogPanel _dialogPanel;
    [Header("[Coins]")]
    [SerializeField] private Text _coins;
    [Header("[Level Points]")]
    [SerializeField] private Text _points;

    public ArmorPanel ArmorPanel => _armorPanel;
    public WeaponPanel WeaponPanel => _weaponPanel;
    public ConsumablesPanel ConsumablesPanel => _consumablesPanel;
    public AbilityPanel AbilityPanel => _abilityPanel;
    public PauseMenuView PauseMenuView => _pauseMenuView;
    public DialogPanel DialogPanel => _dialogPanel;

    public void CloseAllPanels()
    {
        _consumablesPanel.gameObject.SetActive(false);
        _armorPanel.gameObject.SetActive(false);
        _weaponPanel.gameObject.SetActive(false);
        _abilityPanel.gameObject.SetActive(false);
    }

    public void UpdatePlayerStats(int coins, int abilityPoints)
    {
        _coins.text = coins.ToString();
        _points.text = abilityPoints.ToString();
    }
}
