using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Game.Scripts
{
    public class SettingsPanel : MenuTab
    {
        [SerializeField] private ObjectDisabler _objectDisabler;
        [Header("[LanguageButton Entities]")]
        [SerializeField] private DefaultLanguageButtonState _defaultLanguageButtonState;
        [SerializeField] private LanguageButtonView _languageButtonView;
        [SerializeField] private GameObject _buttonsContainer;
        [Header("[Sliders]")]
        [SerializeField] private Slider _ambientSoundsSlider;
        [SerializeField] private Slider _buttonFXSlider;
        [SerializeField] private Button _saveButton;

        private List<LanguageButtonView> _languageButtonViews = new ();
        private DefaultLanguageButtonState _languageButtonState;

        public event Action<string> LanguageChanged;
        public event Action<float> AmbientSoundVolumeChanged;
        public event Action<float> ButtonSoundVolumeChanged;

        private new void Awake()
        {
            base.Awake();
            _languageButtonState = _defaultLanguageButtonState;
            _ambientSoundsSlider.onValueChanged.AddListener(OnAmbientSoundVolumeChanged);
            _buttonFXSlider.onValueChanged.AddListener(OnButtonSoundVolumeChanged);
            _saveButton.onClick.AddListener(Save);
            Fill();
        }

        private new void OnDestroy()
        {
            base.OnDestroy();
            _ambientSoundsSlider.onValueChanged.RemoveListener(OnAmbientSoundVolumeChanged);
            _buttonFXSlider.onValueChanged.RemoveListener(OnButtonSoundVolumeChanged);
            _saveButton.onClick.RemoveListener(Save);
            Clear();
        }

        protected override void OpenTab()
        {
            base.OpenTab();
            SetSliderValue(MenuPanel.Config);
        }

        private void Fill()
        {
            foreach (LanguageButtonState languageButtonState in _languageButtonState.LanguageButtonState)
            {
                LanguageButtonView view = Instantiate(_languageButtonView, _buttonsContainer.transform);
                _languageButtonViews.Add(view);
                view.Initialize(languageButtonState,
                    MenuPanel.MenuSoundPlayer.InterfaceAudioSource,
                    MenuPanel.MenuSoundPlayer.AudioButtonClick,
                    MenuPanel.MenuSoundPlayer.AudioButtonHover);
                view.LanguageSelected += OnLanguageChanged;
            }
        }

        private void Save()
        {
            StartCoroutine(SaveApplicationParameters());
        }

        private IEnumerator SaveApplicationParameters()
        {
            _objectDisabler.gameObject.SetActive(true);
            yield return MenuPanel.SaveProgress.SaveApplicationParameters(MenuPanel.Config);
            _objectDisabler.gameObject.SetActive(false);
            SetSliderValue(MenuPanel.Config);
        }

        private void Clear()
        {
            foreach (LanguageButtonView view in _languageButtonViews)
            {
                view.LanguageSelected -= OnLanguageChanged;
                Destroy(view.gameObject);
            }

            _languageButtonViews.Clear();
        }

        private void SetSliderValue(LoadConfig loadConfig)
        {
            _ambientSoundsSlider.value = loadConfig.AmbientVolume;
            _buttonFXSlider.value = loadConfig.InterfaceVolume;
        }

        private void OnLanguageChanged(string value)
        {
            if (LanguageChanged != null)
                LanguageChanged.Invoke(value);

            MenuPanel.Config.SetCurrentLanguage(value);
        }

        private void OnAmbientSoundVolumeChanged(float value)
        {
            if (AmbientSoundVolumeChanged != null)
                AmbientSoundVolumeChanged.Invoke(value);

            MenuPanel.Config.SetAmbientVolume(value);
        }

        private void OnButtonSoundVolumeChanged(float value)
        {
            if (ButtonSoundVolumeChanged != null)
                ButtonSoundVolumeChanged.Invoke(value);

            MenuPanel.Config.SetIterfaceVolume(value);
        }
    }
}