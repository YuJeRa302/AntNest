using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [Header("[MenuView]")]
    [SerializeField] private MenuPanel _menuPanel;
    [Header("[Sound]")]
    [SerializeField] private AudioSource _ambientSounds;
    [SerializeField] private AudioSource _buttonFX;
    [Header("[SaveProgress]")]
    [SerializeField] private SaveProgress _saveProgress;
    [Header("[Config]")]
    [SerializeField] private LoadConfig _config;

    public LoadConfig LoadConfig => _config;

    public void ApplyChanges()
    {
        _config.SetSoundParameters(_menuPanel.ButtonFXSlider, _menuPanel.ButtonFXSlider);
        SetValueVolume(_menuPanel.AmbientSoundsSlider, _menuPanel.ButtonFXSlider);
    }

    public void SetSoundValue()
    {
        var state = _ambientSounds.mute != true;
        SetStateMuteButton(state);
    }

    public void GetLoad()
    {
        _menuPanel.AmbientSoundsSlider.value = _config.AmbientVolume;
        _menuPanel.ButtonFXSlider.value = _config.InterfaceVolume;
        _ambientSounds.Play();
        _saveProgress.GetLoad(_config);
    }

    public void Initialized()
    {
        _menuPanel.gameObject.SetActive(true);
        GetLoad();
        SetStateMuteButton(false);
    }

    private void SetStateMuteButton(bool state)
    {
        _ambientSounds.mute = state;
        _menuPanel.SetMuteButtonImage(state);
    }

    private void SetValueVolume(Slider ambientSoundsSlider, Slider buttonFXSlider)
    {
        _ambientSounds.volume = ambientSoundsSlider.value;
        _buttonFX.volume = buttonFXSlider.value;
    }
}
