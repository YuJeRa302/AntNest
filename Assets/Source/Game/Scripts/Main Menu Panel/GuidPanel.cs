using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GuidPanel : MenuTab
{
    private readonly string _guidScene = "Guid";

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
    [SerializeField] private Button _openGuidButton;
    [SerializeField] private CanvasLoader _canvasLoader;

    private AsyncOperation _load;

    private new void Awake()
    {
        base.Awake();
        _dotView.SetScrollRect(_scrollPlayerInfo);
        _openGuidButton.onClick.AddListener(LoadGuidLevel);
        _playerButton.onClick.AddListener(ShowPlayerGuide);
        _shopButton.onClick.AddListener(ShowShopGuide);
        _enemyButton.onClick.AddListener(ShowEnemyGuide);
    }

    private new void OnDestroy()
    {
        base.OnDestroy();
        _openGuidButton.onClick.RemoveListener(LoadGuidLevel);
        _playerButton.onClick.RemoveListener(ShowPlayerGuide);
        _shopButton.onClick.RemoveListener(ShowShopGuide);
        _enemyButton.onClick.RemoveListener(ShowEnemyGuide);
    }

    protected override void OpenTab()
    {
        base.OpenTab();
        ShowPlayerGuide();
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

    private IEnumerator LoadScreenLevel(AsyncOperation asyncOperation)
    {
        if (_load != null)
            yield break;

        _load = asyncOperation;
        _load.allowSceneActivation = false;
        _canvasLoader.gameObject.SetActive(true);

        while (_load.progress < 0.9f)
        {
            yield return null;
        }

        _load.allowSceneActivation = true;
        _load = null;
    }

    private void LoadGuidLevel()
    {
        StartCoroutine(LoadScreenLevel(SceneManager.LoadSceneAsync(_guidScene)));
    }
}