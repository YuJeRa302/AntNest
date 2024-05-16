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

    public AudioSource AmbientSounds => _ambientAudioSource;
    public AudioSource InterfaceAudioSource => _interfaceAudioSource;
    public AudioClip AudioButtonHover => _audioButtonHover;
    public AudioClip AudioButtonClick => _audioButtonClick;

    private void OnEnable()
    {
        _menuPanel.SettingsPanel.AmbientSoundVolumeChanged += OnAmbientVolumeChanged;
        _menuPanel.SettingsPanel.ButtonSoundVolumeChanged += OnButtonVolumeChanged;
    }

    private void OnDisable()
    {
        _menuPanel.SettingsPanel.AmbientSoundVolumeChanged -= OnAmbientVolumeChanged;
        _menuPanel.SettingsPanel.ButtonSoundVolumeChanged -= OnButtonVolumeChanged;
    }

    public void Initialize()
    {
        SetValueVolume(_menuPanel.Config.AmbientVolume, _menuPanel.Config.InterfaceVolume);
        _ambientAudioSource.clip = _audioAmbient;
        _ambientAudioSource.Play();
    }

    public void SetStateMuteSound(bool state)
    {
        _ambientAudioSource.mute = state;
    }

    public void HoverSound()
    {
        _interfaceAudioSource.PlayOneShot(_audioButtonHover);
    }

    public void ClickSound()
    {
        _interfaceAudioSource.PlayOneShot(_audioButtonClick);
    }

    private void OnAmbientVolumeChanged(float value)
    {
        _ambientAudioSource.volume = value;
    }

    private void OnButtonVolumeChanged(float value)
    {
        _interfaceAudioSource.volume = value;
    }

    private void SetValueVolume(float ambientSoundsValue, float buttonFXValue)
    {
        _ambientAudioSource.volume = ambientSoundsValue;
        _interfaceAudioSource.volume = buttonFXValue;
    }
}
