using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Game.Scripts
{
    public class PausePanel : GamePanels
    {
        [Header("[UI]")]
        [SerializeField] private Button _openButton;
        [SerializeField] private Button _closeButton;
        [SerializeField] private Image _imageButton;
        [SerializeField] private Sprite _muteButton;
        [SerializeField] private Sprite _unmuteButton;
        [Header("[LanguageButton Entities]")]
        [SerializeField] private DefaultLanguageButtonState _defaultLanguageButtonState;
        [SerializeField] private LanguageButtonView _languageButtonView;
        [SerializeField] private GameObject _buttonsContainer;
        [Header("[Sliders]")]
        [SerializeField] private Slider _ambientSoundsSlider;
        [SerializeField] private Slider _buttonFX;

        private List<LanguageButtonView> _languageButtonViews = new ();
        private DefaultLanguageButtonState _languageButtonState;

        public event Action<string> LanguageChanged;
        public event Action<float> AmbientSoundVolumeChanged;
        public event Action<float> ButtonSoundVolumeChanged;

        private void Awake()
        {
            gameObject.SetActive(false);
            _languageButtonState = _defaultLanguageButtonState;
            Fill();
            AddListener();
        }

        private void OnDestroy()
        {
            RemoveListener();
            Clear();
        }

        public override void Initialize(Player player, LevelObserver levelObserver)
        {
            base.Initialize(player, levelObserver);
            _ambientSoundsSlider.value = LevelObserver.LoadConfig.AmbientVolume;
            _buttonFX.value = LevelObserver.LoadConfig.InterfaceVolume;
            _imageButton.sprite = levelObserver.LoadConfig.IsSoundOn == true ? _unmuteButton : _muteButton;
        }

        private void AddListener()
        {
            LevelObserver.SoundMuted += SetButtonImage;
            _openButton.onClick.AddListener(Open);
            _closeButton.onClick.AddListener(Close);
            _ambientSoundsSlider.onValueChanged.AddListener(OnAmbientSoundVolumeChanged);
            _buttonFX.onValueChanged.AddListener(OnButtonSoundVolumeChanged);
        }

        private void RemoveListener()
        {
            LevelObserver.SoundMuted -= SetButtonImage;
            _openButton.onClick.RemoveListener(Open);
            _closeButton.onClick.RemoveListener(Close);
            _ambientSoundsSlider.onValueChanged.RemoveListener(OnAmbientSoundVolumeChanged);
            _buttonFX.onValueChanged.RemoveListener(OnButtonSoundVolumeChanged);
        }

        private void Fill()
        {
            foreach (LanguageButtonState languageButtonState in _languageButtonState.LanguageButtonState)
            {
                LanguageButtonView view = Instantiate(_languageButtonView, _buttonsContainer.transform);
                _languageButtonViews.Add(view);
                view.Initialize(languageButtonState, LevelObserver.SoundController.InterfaceAudioSource,
                    LevelObserver.SoundController.AudioButtonClick, LevelObserver.SoundController.AudioButtonHover);
                view.LanguageSelected += OnLanguageChanged;
            }
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

        private void OnLanguageChanged(string value)
        {
            if (LanguageChanged != null)
                LanguageChanged.Invoke(value);

            LevelObserver.LoadConfig.SetCurrentLanguage(value);
        }

        private void OnAmbientSoundVolumeChanged(float value)
        {
            if (AmbientSoundVolumeChanged != null)
                AmbientSoundVolumeChanged.Invoke(value);

            LevelObserver.LoadConfig.SetAmbientVolume(value);
        }

        private void OnButtonSoundVolumeChanged(float value)
        {
            if (ButtonSoundVolumeChanged != null)
                ButtonSoundVolumeChanged.Invoke(value);

            LevelObserver.LoadConfig.SetIterfaceVolume(value);
        }

        private void SetButtonImage(bool state)
        {
            _imageButton.sprite = state == true ? _unmuteButton : _muteButton;
        }
    }
}