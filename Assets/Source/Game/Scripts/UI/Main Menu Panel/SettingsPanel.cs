using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MenuTab
{
    [Header("[Settings View]")]
    [SerializeField] private SettingsView _settingsView;
    [Header("[Apply Button]")]
    [SerializeField] private Button _applyButton;

    public SettingsView SettingsView => _settingsView;

    private new void Awake()
    {
        base.Awake();
        _applyButton.onClick.AddListener(ApplyChanges);
    }

    private new void OnDestroy()
    {
        base.OnDestroy();
        _applyButton.onClick.RemoveListener(ApplyChanges);
    }

    private void ApplyChanges()
    {
        MenuPanel.LoadConfig.SetSoundParameters(_settingsView.ButtonFXSlider, _settingsView.ButtonFXSlider);
        MenuPanel.MenuSound.SetValueVolume(_settingsView.AmbientSoundsSlider.value, _settingsView.ButtonFXSlider.value);
    }
}