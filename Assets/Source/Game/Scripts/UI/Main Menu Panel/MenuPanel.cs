using UnityEngine;
using UnityEngine.UI;

public class MenuPanel : Panels
{
    [Header("[Level Buttons]")]
    [SerializeField] private QuestPanel _questPanel;
    [Header("[Menu]")]
    [SerializeField] private Menu _menu;
    [Header("[Sliders]")]
    [SerializeField] private Slider _ambientSoundsSlider;
    [SerializeField] private Slider _buttonFXSlider;
    [Header("[Mute Button Image]")]
    [SerializeField] private Image _imageButton;
    [Header("[Sprite Mute Button]")]
    [SerializeField] private Sprite _muteButton;
    [SerializeField] private Sprite _unmuteButton;

    public Slider AmbientSoundsSlider => _ambientSoundsSlider;
    public Slider ButtonFXSlider => _buttonFXSlider;
    public Image MuteImageButton => _imageButton;

    public void Initialize()
    {
        _menu.Initialize();
        gameObject.SetActive(true);
    }

    public void SetMuteButtonImage(bool state)
    {
        MuteImageButton.sprite = state == true ? _muteButton : _unmuteButton;
    }

    public void SetSliderValue(float ambientSoundsSlider, float buttonFXSlider)
    {
        _ambientSoundsSlider.value = ambientSoundsSlider;
        _buttonFXSlider.value = buttonFXSlider;
    }

    protected override void UpdateInfo()
    {
        _questPanel.Initialize(_menu.LoadConfig);
    }
}