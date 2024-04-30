using UnityEngine;

public class MenuPanel : Panels
{
    [SerializeField] private SaveProgress _saveProgress;
    [SerializeField] private LoadConfig _config;
    [Header("[Menu Entities]")]
    [SerializeField] private MenuSound _menuSound;
    [SerializeField] private MenuView _menuView;
    [SerializeField] private SettingsPanel _settingsPanel;

    public MenuSound MenuSound => _menuSound;
    public LoadConfig LoadConfig => _config;

    public void Initialize()
    {
        gameObject.SetActive(true);
#if UNITY_WEBGL && !UNITY_EDITOR
                GetLoad();
#endif
    }

    private void GetLoad()
    {
        _saveProgress.GetLoad(_config);
        _settingsPanel.SettingsView.SetSliderValue(_config);
        _menuSound.Initialize();
    }
}