using UnityEngine;

public class Menu : MonoBehaviour
{
    [Header("[MenuView]")]
    [SerializeField] private MenuPanel _menuPanel;
    [Header("[MenuSound]")]
    [SerializeField] private MenuSound _menuSound;
    [Header("[SaveProgress]")]
    [SerializeField] private SaveProgress _saveProgress;
    [Header("[Config]")]
    [SerializeField] private LoadConfig _config;

    public LoadConfig LoadConfig => _config;

    public void Initialize()
    {
        //GetLoad();
        SetStateMuteButton(false);
    }

    public void GetLoad()
    {
        _saveProgress.GetLoad(_config);
        _menuPanel.SetSliderValue(_config.AmbientVolume, _config.InterfaceVolume);
        _menuSound.Initialized();
    }

    public void ApplyChanges()
    {
        _config.SetSoundParameters(_menuPanel.ButtonFXSlider, _menuPanel.ButtonFXSlider);
        _menuSound.SetValueVolume(_menuPanel.AmbientSoundsSlider.value, _menuPanel.ButtonFXSlider.value);
    }

    public void SetSoundValue()
    {
        var state = _menuSound.AmbientSounds.mute != true;
        SetStateMuteButton(state);
    }

    private void SetStateMuteButton(bool state)
    {
        _menuSound.SetStateMuteSound(state);
        _menuPanel.SetMuteButtonImage(state);
    }
}