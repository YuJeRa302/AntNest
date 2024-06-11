using UnityEngine;

public class SoundController : MonoBehaviour
{
    [Header("[Audio Source]")]
    [SerializeField] private AudioSource _ambientAudioSource;
    [SerializeField] private AudioSource _interfaceAudioSource;
    [SerializeField] private AudioSource _rewardAudioSource;
    [Header("[Level Entities]")]
    [SerializeField] private LevelObserver _levelObserver;
    [SerializeField] private PausePanel _pausePanel;
    [SerializeField] private RewardPanel _rewardPanel;
    [Header("[Buttons Audio Clips]")]
    [SerializeField] private AudioClip _audioButtonHover;
    [SerializeField] private AudioClip _audioButtonClick;
    [Header("[Ambient Audio Clips]")]
    [SerializeField] private AudioClip _audioAmbient;
    [Header("[Reward Audio Clips]")]
    [SerializeField] private AudioClip _rewardAudio;
    [SerializeField] private AudioClip _winAudio;
    [SerializeField] private AudioClip _loseAudio;

    public AudioSource InterfaceAudioSource => _interfaceAudioSource;
    public AudioClip AudioButtonHover => _audioButtonHover;
    public AudioClip AudioButtonClick => _audioButtonClick;

    private void OnEnable()
    {
        _levelObserver.SoundMuted += OnMuted;
        _pausePanel.AmbientSoundVolumeChanged += OnAmbientVolumeChanged;
        _pausePanel.ButtonSoundVolumeChanged += OnButtonVolumeChanged;
        _rewardPanel.RewardScreenOpened += OnRewardScreenOpen;
        _rewardPanel.RewardPanelOpened += OnRewardPanelOpen;
    }

    private void OnDisable()
    {
        _levelObserver.SoundMuted -= OnMuted;
        _pausePanel.AmbientSoundVolumeChanged -= OnAmbientVolumeChanged;
        _pausePanel.ButtonSoundVolumeChanged -= OnButtonVolumeChanged;
        _rewardPanel.RewardScreenOpened -= OnRewardScreenOpen;
        _rewardPanel.RewardPanelOpened -= OnRewardPanelOpen;
    }

    public void Initialize(LoadConfig loadConfig)
    {
        _ambientAudioSource.volume = loadConfig.AmbientVolume;
        _interfaceAudioSource.volume = loadConfig.InterfaceVolume;
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

    private void OnMuted(bool state)
    {
        _ambientAudioSource.mute = state;
        _interfaceAudioSource.mute = state;
    }

    private void OnAmbientVolumeChanged(float value)
    {
        _ambientAudioSource.volume = value;
    }

    private void OnButtonVolumeChanged(float value)
    {
        _interfaceAudioSource.volume = value;
    }

    private void OnRewardPanelOpen(bool state)
    {
        _ambientAudioSource.mute = true;
        AudioClip audioClip = state == false ? _loseAudio : _winAudio;
        _rewardAudioSource.PlayOneShot(audioClip);
    }

    private void OnRewardScreenOpen(int value)
    {
        _ambientAudioSource.mute = true;
        AudioListener.pause = false;
        _rewardAudioSource.PlayOneShot(_rewardAudio);
    }
}