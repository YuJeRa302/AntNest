using UnityEngine;
using UnityEngine.UI;
using YG;

public class Menu : MonoBehaviour
{
    [Header("[MenuView]")]
    [SerializeField] private MenuView _menuView;
    [Header("[Sound]")]
    [SerializeField] private AudioSource _ambientSounds;
    [SerializeField] private AudioSource _buttonFX;
    [Header("[SaveProgress]")]
    [SerializeField] private SaveProgress _saveProgress;

    private LoadConfig _config;

    public LoadConfig LoadConfig => _config;

    public void ApplyChanges()
    {
        _config.SetSoundParameters(_menuView.ButtonFXSlider, _menuView.ButtonFXSlider);
        SetValueVolume(_menuView.AmbientSoundsSlider, _menuView.ButtonFXSlider);
    }

    public void SetSoundValue()
    {
        var state = _ambientSounds.mute != true;
        SetStateMuteButton(state);
    }

    public void GetLoad()
    {
        _config = Resources.Load<LoadConfig>("LevelConfig/LevelConfig");
        _menuView.AmbientSoundsSlider.value = _config.AmbientVolume;
        _menuView.ButtonFXSlider.value = _config.InterfaceVolume;
        _ambientSounds.Play();
       // _saveProgress.GetLoad(_config);
    }

    public void Initialized()
    {
        if (YandexGame.SDKEnabled == true) GetLoad();
        SetStateMuteButton(false);
    }

    private void Awake()
    {
        YandexGame.Instance.InitializationSDK();
    }

    private void OnEnable() => YandexGame.GetDataEvent += GetLoad;

    private void OnDisable() => YandexGame.GetDataEvent -= GetLoad;

    private void SetStateMuteButton(bool state)
    {
        _ambientSounds.mute = state;
        _menuView.SetMuteButtonImage(state);
    }

    private void SetValueVolume(Slider ambientSoundsSlider, Slider buttonFXSlider)
    {
        _ambientSounds.volume = ambientSoundsSlider.value;
        _buttonFX.volume = buttonFXSlider.value;
    }
}
