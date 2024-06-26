using UnityEngine;

public class MenuSound : MonoBehaviour
{
    [SerializeField] private MenuPanel _menuPanel;
    [Header("[AudioSource]")]
    [SerializeField] private AudioSource _ambientAudioSource;
    [SerializeField] private AudioSource _interfaceAudioSource;
    [Header("[Buttons Audio Clips]")]
    [SerializeField] private AudioClip _audioButtonHover;
    [SerializeField] private AudioClip _audioButtonClick;
    [Header("[Ambient Audio Clips]")]
    [SerializeField] private AudioClip _audioAmbient;

    private readonly float _pauseValue = 0;
    private readonly float _resumeValue = 1f;

    public AudioSource InterfaceAudioSource => _interfaceAudioSource;
    public AudioClip AudioButtonHover => _audioButtonHover;
    public AudioClip AudioButtonClick => _audioButtonClick;

    private void OnEnable()
    {
        _menuPanel.SettingsPanel.AmbientSoundVolumeChanged += OnAmbientVolumeChanged;
        _menuPanel.SettingsPanel.ButtonSoundVolumeChanged += OnButtonVolumeChanged;
        _menuPanel.MenuView.SoundStateChanged += OnSoundStateChanged;
    }

    private void OnDisable()
    {
        _menuPanel.SettingsPanel.AmbientSoundVolumeChanged -= OnAmbientVolumeChanged;
        _menuPanel.SettingsPanel.ButtonSoundVolumeChanged -= OnButtonVolumeChanged;
        _menuPanel.MenuView.SoundStateChanged -= OnSoundStateChanged;
    }

    public void Initialize()
    {
        SetValueVolume(_menuPanel.Config.AmbientVolume, _menuPanel.Config.InterfaceVolume);
        SetAudioListenerValue(_menuPanel.Config.IsSoundOn);
        _ambientAudioSource.clip = _audioAmbient;
        _ambientAudioSource.Play();
    }

    public void HoverSound()
    {
        _interfaceAudioSource.PlayOneShot(_audioButtonHover);
    }

    public void ClickSound()
    {
        _interfaceAudioSource.PlayOneShot(_audioButtonClick);
    }

    private void OnSoundStateChanged(bool state)
    {
        _menuPanel.Config.SetSoundState(state);
        SetAudioListenerValue(state);
    }

    private void OnAmbientVolumeChanged(float value)
    {
        _ambientAudioSource.volume = value;
    }

    private void OnButtonVolumeChanged(float value)
    {
        _interfaceAudioSource.volume = value;
    }

    private void SetAudioListenerValue(bool state)
    {
        AudioListener.pause = !state;
        AudioListener.volume = !state == true ? _pauseValue : _resumeValue;
    }

    private void SetValueVolume(float ambientSoundsValue, float buttonFXValue)
    {
        _ambientAudioSource.volume = ambientSoundsValue;
        _interfaceAudioSource.volume = buttonFXValue;
    }
}
