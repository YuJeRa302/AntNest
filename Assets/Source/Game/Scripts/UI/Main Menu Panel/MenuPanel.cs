using Lean.Localization;
using System.Collections;
using UnityEngine;
using Agava.YandexGames;

public class MenuPanel : Panels
{
    [SerializeField] private SaveProgress _saveProgress;
    [SerializeField] private LoadConfig _config;
    [Header("[Menu Entities]")]
    [SerializeField] private MenuSound _menuSound;
    [SerializeField] private MenuView _menuView;
    [SerializeField] private SettingsPanel _settingsPanel;
    [Header("[LeanLocalization]")]
    [SerializeField] private LeanLocalization _leanLocalization;

    public MenuSound MenuSound => _menuSound;
    public SettingsPanel SettingsPanel => _settingsPanel;

    private void Awake()
    {
        YandexGamesSdk.CallbackLogging = true;
        _settingsPanel.LanguageChanged += OnLanguageChanged;
    }

    private void OnDestroy()
    {
        _settingsPanel.LanguageChanged -= OnLanguageChanged;
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
        TestLoad();
#endif
    }

    private void OnInitialize()
    {
        gameObject.SetActive(true);
#if UNITY_WEBGL && !UNITY_EDITOR
                GetLoad();
#endif
    }

    private void OnLanguageChanged(string value)
    {
        _leanLocalization.SetCurrentLanguage(value);
        _config.SetCurrentLanguage(value);
    }

    private void TestLoad()
    {
        gameObject.SetActive(true);
        _config.SetCurrentLanguage("ru");
        _leanLocalization.SetCurrentLanguage("ru");
        _settingsPanel.SetSliderValue(_config);
        _menuSound.SetValueVolume(_config.AmbientVolume, _config.InterfaceVolume);
        _menuSound.Initialize();
    }

    private void GetLoad()
    {
        _leanLocalization.SetCurrentLanguage(YandexGamesSdk.Environment.i18n.lang);
        _config.SetCurrentLanguage(YandexGamesSdk.Environment.i18n.lang);
        _saveProgress.GetLoad(_config);
        _settingsPanel.SetSliderValue(_config);
        _menuSound.SetValueVolume(_config.AmbientVolume, _config.InterfaceVolume);
        _menuSound.Initialize();
    }
}