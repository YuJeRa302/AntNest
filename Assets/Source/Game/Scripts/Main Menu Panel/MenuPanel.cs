using Lean.Localization;
using UnityEngine;
using Agava.YandexGames;

public class MenuPanel : MonoBehaviour
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
    public LoadConfig Config => _config;

    private void Awake()
    {
        LoadScene();
        _settingsPanel.LanguageChanged += OnLanguageChanged;
    }

    private void OnDestroy()
    {
        _settingsPanel.LanguageChanged -= OnLanguageChanged;
    }

    private void LoadScene()
    {
        YandexGamesSdk.GameReady();
        _saveProgress.GetLoad(_config);
        _menuSound.Initialize();

        if (_config.IsFirstSession == true)
        {
            _config.SetSessionState(false);
            _leanLocalization.SetCurrentLanguage(YandexGamesSdk.Environment.i18n.lang);
        }
        else
            _leanLocalization.SetCurrentLanguage(_config.Language);
    }

    private void OnLanguageChanged(string value)
    {
        _leanLocalization.SetCurrentLanguage(value);
        _config.SetCurrentLanguage(value);
    }
}