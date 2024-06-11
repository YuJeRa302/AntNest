using Lean.Localization;
using UnityEngine;
using Agava.YandexGames;
using System.Collections;
using Agava.WebUtility;

public class MenuPanel : MonoBehaviour
{
    private const string EnglishCode = "English";
    private const string RussianCode = "Russian";
    private const string TurkishCode = "Turkish";
    private const string English = "en";
    private const string Russian = "ru";
    private const string Turkish = "tr";

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
    public LoadConfig Config => _config;
    public SaveProgress SaveProgress => _saveProgress;

    private void Awake()
    {
        YandexGamesSdk.GameReady();
        StartCoroutine(LoadScene());
        _settingsPanel.LanguageChanged += OnLanguageChanged;
    }

    private void OnDestroy()
    {
        _settingsPanel.LanguageChanged -= OnLanguageChanged;
    }

    private IEnumerator LoadScene()
    {
        yield return _saveProgress.GetLoad(_config);
        _menuSound.Initialize();

        if (Device.IsMobile)
            _config.SetCurrentDevice(TypeDevice.Mobile);
        else
            _config.SetCurrentDevice(TypeDevice.Desktop);

        if (_config.Language != null)
            _leanLocalization.SetCurrentLanguage(_config.Language);
        else
            SetLanguage();
    }

    private void SetLanguage()
    {
        string languageCode = YandexGamesSdk.Environment.i18n.lang;

        switch (languageCode)
        {
            case English:
                _leanLocalization.SetCurrentLanguage(EnglishCode);
                break;
            case Russian:
                _leanLocalization.SetCurrentLanguage(RussianCode);
                break;
            case Turkish:
                _leanLocalization.SetCurrentLanguage(TurkishCode);
                break;
        }
    }

    private void OnLanguageChanged(string value)
    {
        _leanLocalization.SetCurrentLanguage(value);
        _config.SetCurrentLanguage(value);
    }
}