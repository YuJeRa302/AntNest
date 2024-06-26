using UnityEngine;
using UnityEngine.UI;

public class GuidSoundController : MonoBehaviour
{
    [Header("[Level Entities]")]
    [SerializeField] private GuidObserver _guidObserver;
    [Header("[Audio Source]")]
    [SerializeField] private AudioSource _ambientAudioSource;
    [SerializeField] private AudioSource _interfaceAudioSource;
    [Header("[Buttons Audio Clips]")]
    [SerializeField] private AudioClip _audioButtonHover;
    [SerializeField] private AudioClip _audioButtonClick;
    [Header("[Sliders]")]
    [SerializeField] private Slider _ambientSoundsSlider;
    [SerializeField] private Slider _buttonFXSlider;

    private readonly float _pauseValue = 0;
    private readonly float _resumeValue = 1f;

    private LoadConfig _loadConfig;

    private void Awake()
    {
        _guidObserver.SoundMuted += OnMuted;
        _ambientSoundsSlider.onValueChanged.AddListener(AmbientVolumeChanged);
        _buttonFXSlider.onValueChanged.AddListener(ButtonVolumeChanged);
    }

    private void OnDisable()
    {
        _guidObserver.SoundMuted -= OnMuted;
        _ambientSoundsSlider.onValueChanged.RemoveListener(AmbientVolumeChanged);
        _buttonFXSlider.onValueChanged.RemoveListener(ButtonVolumeChanged);
    }

    public void Initialize(LoadConfig loadConfig)
    {
        _loadConfig = loadConfig;
        SetSliderValue(_loadConfig);
        _ambientAudioSource.volume = loadConfig.AmbientVolume;
        _interfaceAudioSource.volume = loadConfig.InterfaceVolume;
        _ambientAudioSource.Play();
        SetAudioListenerValue(loadConfig.IsSoundOn);
    }

    public void HoverSound()
    {
        _interfaceAudioSource.PlayOneShot(_audioButtonHover);
    }

    public void ClickSound()
    {
        _interfaceAudioSource.PlayOneShot(_audioButtonClick);
    }

    private void OnMuted(bool state)
    {
        SetAudioListenerValue(state);
    }

    private void AmbientVolumeChanged(float value)
    {
        _ambientAudioSource.volume = value;
        _loadConfig.SetIterfaceVolume(value);
    }

    private void ButtonVolumeChanged(float value)
    {
        _interfaceAudioSource.volume = value;
        _loadConfig.SetIterfaceVolume(value);
    }

    private void SetAudioListenerValue(bool state)
    {
        AudioListener.pause = !state;
        AudioListener.volume = !state == true ? _pauseValue : _resumeValue;
    }

    private void SetSliderValue(LoadConfig loadConfig)
    {
        _ambientSoundsSlider.value = loadConfig.AmbientVolume;
        _buttonFXSlider.value = loadConfig.InterfaceVolume;
    }
}
