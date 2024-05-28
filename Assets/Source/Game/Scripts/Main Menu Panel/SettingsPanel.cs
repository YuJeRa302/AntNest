using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MenuTab
{
    [Header("[LanguageButton Entities]")]
    [SerializeField] private DefaultLanguageButtonState _defaultLanguageButtonState;
    [SerializeField] private LanguageButtonView _languageButtonView;
    [SerializeField] private GameObject _buttonsContainer;
    [Header("[Sliders]")]
    [SerializeField] private Slider _ambientSoundsSlider;
    [SerializeField] private Slider _buttonFXSlider;

    private List<LanguageButtonView> _languageButtonViews = new();
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
        Fill();
    }

    private new void OnDestroy()
    {
        base.OnDestroy();
        _ambientSoundsSlider.onValueChanged.RemoveListener(OnAmbientSoundVolumeChanged);
        _buttonFXSlider.onValueChanged.RemoveListener(OnButtonSoundVolumeChanged);
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
            view.Initialize(languageButtonState, MenuPanel.MenuSound.InterfaceAudioSource,
                MenuPanel.MenuSound.AudioButtonClick, MenuPanel.MenuSound.AudioButtonHover);
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

    private void SetSliderValue(LoadConfig loadConfig)
    {
        _ambientSoundsSlider.value = loadConfig.AmbientVolume;
        _buttonFXSlider.value = loadConfig.InterfaceVolume;
    }

    private void OnLanguageChanged(string value)
    {
        LanguageChanged.Invoke(value);
        MenuPanel.Config.SetCurrentLanguage(value);
    }

    private void OnAmbientSoundVolumeChanged(float value)
    {
        AmbientSoundVolumeChanged.Invoke(value);
        MenuPanel.Config.SetAmbientVolume(value);
    }

    private void OnButtonSoundVolumeChanged(float value)
    {
        ButtonSoundVolumeChanged.Invoke(value);
        MenuPanel.Config.SetIterfaceVolume(value);
    }
}

[Serializable]
public struct DefaultLanguageButtonState
{
    public List<LanguageButtonState> LanguageButtonState;
}