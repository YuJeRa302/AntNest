using UnityEngine;
using UnityEngine.UI;

public class GuidPanel : MenuTab
{
    [Header("[PlayerInfo]")]
    [SerializeField] private GameObject _playerInfo;
    [SerializeField] private Button _playerButton;
    [SerializeField] private ScrollRect _scrollPlayerInfo;
    [Header("[ShopItem Info]")]
    [SerializeField] private GameObject _shopItem;
    [SerializeField] private Button _shopButton;
    [SerializeField] private ScrollRect _scrollShopItemInfo;
    [Header("[Enemy Info]")]
    [SerializeField] private GameObject _enemy;
    [SerializeField] private Button _enemyButton;
    [SerializeField] private ScrollRect _scrollEnemyInfo;
    [Header("[Menu]")]
    [SerializeField] private MenuPanel _menuPanel;
    [Header("[DotView]")]
    [SerializeField] private DotView _dotView;

    private new void Awake()
    {
        base.Awake();
        _dotView.SetScrollRect(_scrollPlayerInfo);
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
        _dotView.SetScrollRect(_scrollPlayerInfo);
    }

    private void ShowShopGuide()
    {
        HideAllGuide();
        _shopItem.SetActive(true);
        _dotView.SetScrollRect(_scrollShopItemInfo);
    }

    private void ShowEnemyGuide()
    {
        HideAllGuide();
        _enemy.SetActive(true);
        _dotView.SetScrollRect(_scrollEnemyInfo);
    }

    private void HideAllGuide()
    {
        _playerInfo.SetActive(false);
        _shopItem.SetActive(false);
        _enemy.SetActive(false);
    }
}