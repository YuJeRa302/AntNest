using Lean.Localization;
using UnityEngine;
using UnityEngine.UI;
public class ChangeLanguage : Panels
{
    [Header("[Menu]")]
    [SerializeField] private MenuPanel _menuPanel;
    [Header("[CurrentLanguage]")]
    [SerializeField] private Button _currentLanguageButton;
    [Header("[LeanLocalization]")]
    [SerializeField] private LeanLocalization _leanLocalization;
    [Header("[LoadConfig]")]
    [SerializeField] private LoadConfig _loadConfig;
    [Header("[CurrentLanguageSprit]")]
    [SerializeField] private Sprite _sprite;

    public void SelectLanguage(string value)
    {
        _leanLocalization.SetCurrentLanguage(value);
        _loadConfig.SetCurrentLanguage(value);
        _menuPanel.Initialized();
    }
}