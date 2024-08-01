using System.Collections;
using UnityEngine;
using Lean.Localization;
using Agava.YandexGames;
using Agava.WebUtility;

namespace Assets.Source.Game.Scripts
{
    public class MenuPanel : MonoBehaviour
    {
        private const string English = "en";
        private const string Russian = "ru";
        private const string Turkish = "tr";

        private readonly int _nullStringLength = 0;

        [SerializeField] private SaveProgress _saveProgress;
        [SerializeField] private LoadConfig _config;
        [Header("[Menu Entities]")]
        [SerializeField] private MenuSoundPlayer _menuSoundPlayer;
        [SerializeField] private MenuView _menuView;
        [SerializeField] private SettingsPanel _settingsPanel;
        [SerializeField] private LeanLocalization _leanLocalization;

        public MenuView MenuView => _menuView;
        public MenuSoundPlayer MenuSoundPlayer => _menuSoundPlayer;
        public SettingsPanel SettingsPanel => _settingsPanel;
        public LoadConfig Config => _config;
        public SaveProgress SaveProgress => _saveProgress;

        private void Awake()
        {
            _config.SetPauseGameState(false);

#if UNITY_EDITOR
            _settingsPanel.LanguageChanged += OnLanguageChanged;
#else
        YandexGamesSdk.GameReady();
        _settingsPanel.LanguageChanged += OnLanguageChanged;
#endif
        }

        private void OnDestroy()
        {
            _settingsPanel.LanguageChanged -= OnLanguageChanged;
        }

        private IEnumerator Start()
        {
            yield return _saveProgress.GetLoad(_config);
            _menuView.Initialize();
            _menuSoundPlayer.Initialize();

#if UNITY_EDITOR
            _leanLocalization.SetCurrentLanguage(Russian);
            _config.SetCurrentDevice(TypeDevice.Desktop);
#else
        if (_config.Language.Length == _nullStringLength)
            SetLanguage();
        else
            _leanLocalization.SetCurrentLanguage(_config.Language);

        if (Device.IsMobile)
            _config.SetCurrentDevice(TypeDevice.Mobile);
        else
            _config.SetCurrentDevice(TypeDevice.Desktop);
#endif
        }

        private void SetLanguage()
        {
            string languageCode = YandexGamesSdk.Environment.i18n.lang;

            switch (languageCode)
            {
                case English:
                    _leanLocalization.SetCurrentLanguage(English);
                    break;
                case Russian:
                    _leanLocalization.SetCurrentLanguage(Russian);
                    break;
                case Turkish:
                    _leanLocalization.SetCurrentLanguage(Turkish);
                    break;
            }
        }

        private void OnLanguageChanged(string value)
        {
            _leanLocalization.SetCurrentLanguage(value);
            _config.SetCurrentLanguage(value);
        }
    }
}