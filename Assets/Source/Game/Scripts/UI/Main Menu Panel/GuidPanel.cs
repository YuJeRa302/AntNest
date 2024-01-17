using UnityEngine;
using UnityEngine.UI;

public class GuidPanel : Panels
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
    [Header("[Close Button]")]
    [SerializeField] private Button _closeButton;

    public void ShowPlayerGuide()
    {
        HideAllGuide();
        _playerInfo.SetActive(true);
    }

    public void ShowShopGuide()
    {
        HideAllGuide();
        _shopItem.SetActive(true);
    }

    public void ShowEnemyGuide()
    {
        HideAllGuide();
        _enemy.SetActive(true);
    }

    public void Close()
    {
        _menuPanel.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _playerButton.onClick.AddListener(ShowPlayerGuide);
        _shopButton.onClick.AddListener(ShowShopGuide);
        _enemyButton.onClick.AddListener(ShowEnemyGuide);
        _closeButton.onClick.AddListener(Close);
    }

    private void OnDisable()
    {
        _playerButton.onClick.RemoveListener(ShowPlayerGuide);
        _shopButton.onClick.RemoveListener(ShowShopGuide);
        _enemyButton.onClick.RemoveListener(ShowEnemyGuide);
        _closeButton.onClick.RemoveListener(Close);
    }

    private void HideAllGuide()
    {
        _playerInfo.SetActive(false);
        _shopItem.SetActive(false);
        _enemy.SetActive(false);
    }
}