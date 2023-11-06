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

    public void SetMuteButtonImage(bool state)
    {
        MuteImageButton.sprite = state == true ? _muteButton : _unmuteButton;
    }

    public void Initialized() => _menu.Initialized(); 

    protected override void UpdateInfo()
    {
        _questPanel.Initialized(_menu.LoadConfig);
    }
}