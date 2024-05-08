using Agava.YandexGames;
using Lean.Localization;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ChangeLanguage : Panels
{
    [Header("[Menu]")]
    [SerializeField] private MenuPanel _menuPanel;
    [Header("[CurrentLanguage]")]
    [SerializeField] private Button _currentLanguageButton;
    [Header("[ButtonsLang]")]
    [SerializeField] private GameObject _buttonsLang;
    [Header("[LeanLocalization]")]
    [SerializeField] private LeanLocalization _leanLocalization;
    [Header("[LoadConfig]")]
    [SerializeField] private LoadConfig _loadConfig;
    [Header("[CurrentLanguageSprit]")]
    [SerializeField] private Sprite _sprite;

    private void Awake()
    {
        YandexGamesSdk.CallbackLogging = true;
    }

    private IEnumerator Start()
    {

#if UNITY_WEBGL && !UNITY_EDITOR
        if (YandexGamesSdk.IsInitialized == true) 
        {
            yield return null;
            OnInitialize();
        }
        else yield return YandexGamesSdk.Initialize(OnInitialize);
#endif
#if UNITY_EDITOR
        yield return null;
        OnInitialize();
#endif
    }

    public void SelectLanguage(string value)
    {
        _leanLocalization.SetCurrentLanguage(value);
        _loadConfig.SetCurrentLanguage(value);
        //_menuPanel.Initialize();
        _loadConfig.SetSessionState(false);
    }

    private void OnInitialize()
    {
       // YandexGamesSdk.Environment.i18n.lang; язык
#if UNITY_WEBGL && !UNITY_EDITOR
                YandexGamesSdk.GameReady();
#endif
        if (_loadConfig.IsFirstSession == false)
        {
            gameObject.SetActive(_loadConfig.IsFirstSession);
            //_menuPanel.Initialize();
        }
        else _buttonsLang.SetActive(true);
    }
}