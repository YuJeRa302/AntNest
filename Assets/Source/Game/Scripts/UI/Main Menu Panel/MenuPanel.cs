using Lean.Localization;
using System.Collections;
using UnityEngine;
using Agava.YandexGames;

public class MenuPanel : Panels
{
    [SerializeField] private Canvas _canvasSceneLoader;
    [SerializeField] private SaveProgress _saveProgress;
    [SerializeField] private LoadConfig _config;
    [Header("[Menu Entities]")]
    [SerializeField] private MenuSound _menuSound;
    [SerializeField] private MenuView _menuView;
    [SerializeField] private SettingsPanel _settingsPanel;
    [Header("[LeanLocalization]")]
    [SerializeField] private LeanLocalization _leanLocalization;

    private float _timeLoadScene = 2f;
    private IEnumerator _sceneLoad;

    public MenuSound MenuSound => _menuSound;
    public SettingsPanel SettingsPanel => _settingsPanel;
    public LoadConfig Config => _config;

    private void Awake()
    {
        YandexGamesSdk.CallbackLogging = true;
        _sceneLoad = LoadScene();
        _settingsPanel.LanguageChanged += OnLanguageChanged;
    }

    private void OnDestroy()
    {
        _settingsPanel.LanguageChanged -= OnLanguageChanged;
    }

    private IEnumerator Start()
    {
        if (YandexGamesSdk.IsInitialized)
        {
            yield return null;
            LoadConfig();
        }
        else
            yield return YandexGamesSdk.Initialize(OnInitialize);
    }

    private void OnInitialize()
    {
        _saveProgress.GetLoad(_config);
        StartCoroutine(_sceneLoad);
    }

    private IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(_timeLoadScene);

        if (_config.IsFirstSession == true)
        {
            _config.SetSessionState(false);
            _config.SetCurrentLanguage(YandexGamesSdk.Environment.i18n.lang);
        }

        OnLanguageChanged(_config.Language);
        gameObject.SetActive(true);
        _settingsPanel.SetSliderValue(_config);
        _menuSound.Initialize();
        _canvasSceneLoader.gameObject.SetActive(false);
        StopCoroutine(_sceneLoad);
    }

    private void OnLanguageChanged(string value)
    {
        _leanLocalization.SetCurrentLanguage(value);
        _config.SetCurrentLanguage(value);
    }

    private void LoadConfig()
    {
        _canvasSceneLoader.gameObject.SetActive(false);
        OnLanguageChanged(_config.Language);
        gameObject.SetActive(true);
        _settingsPanel.SetSliderValue(_config);
        _menuSound.Initialize();
    }
}