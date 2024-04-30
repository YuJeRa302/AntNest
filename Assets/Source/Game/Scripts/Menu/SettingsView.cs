using UnityEngine;
using UnityEngine.UI;

public class SettingsView : MonoBehaviour
{
    [Header("[Sliders]")]
    [SerializeField] private Slider _ambientSoundsSlider;
    [SerializeField] private Slider _buttonFXSlider;
    [Header("[Settings Panel]")]
    [SerializeField] private SettingsPanel _settingsPanel;

    public Slider AmbientSoundsSlider => _ambientSoundsSlider;
    public Slider ButtonFXSlider => _buttonFXSlider;

    public void SetSliderValue(LoadConfig loadConfig)
    {
        _ambientSoundsSlider.value = loadConfig.AmbientVolume;
        _buttonFXSlider.value = loadConfig.InterfaceVolume;
    }
}