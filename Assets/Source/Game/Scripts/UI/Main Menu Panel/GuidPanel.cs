using UnityEngine;
using UnityEngine.UI;

public class GuidPanel : MenuTab
{
    [Header("[PlayerInfo]")]
    [SerializeField] private GameObject _playerInfo;
    [SerializeField] private Button _playerButton;
    [Header("[ShopItem Info]")]
    [SerializeField] private GameObject _shopItem;
    [SerializeField] private Button _shopButton;
    [Header("[Enemy Info]")]
    [SerializeField] private GameObject _enemy;
    [SerializeField] private Button _enemyButton;
    [Header("[Menu]")]
    [SerializeField] private MenuPanel _menuPanel;

    private new void Awake()
    {
        base.Awake();
        _playerButton.onClick.AddListener(ShowPlayerGuide);
        _shopButton.onClick.AddListener(ShowShopGuide);
        _enemyButton.onClick.AddListener(ShowEnemyGuide);
    }

    private new void OnDestroy()
    {
        base.OnDestroy();
        _playerButton.onClick.RemoveListener(ShowPlayerGuide);
        _shopButton.onClick.RemoveListener(ShowShopGuide);
        _enemyButton.onClick.RemoveListener(ShowEnemyGuide);
    }

    private void ShowPlayerGuide()
    {
        HideAllGuide();
        _playerInfo.SetActive(true);
    }

    private void ShowShopGuide()
    {
        HideAllGuide();
        _shopItem.SetActive(true);
    }

    private void ShowEnemyGuide()
    {
        HideAllGuide();
        _enemy.SetActive(true);
    }

    private void HideAllGuide()
    {
        _playerInfo.SetActive(false);
        _shopItem.SetActive(false);
        _enemy.SetActive(false);
    }
}