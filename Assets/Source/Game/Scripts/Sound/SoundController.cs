using UnityEngine;

namespace Assets.Source.Game.Scripts
{
    public class SoundController : MonoBehaviour
    {
        private readonly float _pauseValue = 0;
        private readonly float _resumeValue = 1f;

        [Header("[AudioSources]")]
        [SerializeField] private AudioSource _ambientAudioSource;
        [SerializeField] private AudioSource _interfaceAudioSource;
        [SerializeField] private AudioSource _rewardAudioSource;
        [Header("[Level Entities]")]
        [SerializeField] private LevelObserver _levelObserver;
        [SerializeField] private PausePanel _pausePanel;
        [SerializeField] private RewardPanel _rewardPanel;
        [Header("[AudioClips]")]
        [SerializeField] private AudioClip _audioButtonHover;
        [SerializeField] private AudioClip _audioButtonClick;
        [SerializeField] private AudioClip _audioAmbient;
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
            _rewardAudioSource.PlayOneShot(_rewardAudio);
        }

        private void SetAudioListenerValue(bool state)
        {
            AudioListener.pause = !state;
            AudioListener.volume = !state == true ? _pauseValue : _resumeValue;
        }
    }
}