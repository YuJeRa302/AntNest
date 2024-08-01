using UnityEngine;

namespace Assets.Source.Game.Scripts
{
    public class PlayerSoundPlayer : MonoBehaviour
    {
        [Header("[AudioSources]")]
        [SerializeField] private AudioSource _audioSourceAxe;
        [SerializeField] private AudioSource _audioStep;
        [SerializeField] private AudioSource _audioPlayerState;
        [SerializeField] private AudioSource _audioPlayerAbility;
        [Header("[AudioClips]")]
        [SerializeField] private AudioClip _axe;
        [SerializeField] private AudioClip _footStep;

        public AudioClip AxeSound => _axe;
        public AudioClip FootStep => _footStep;
        public AudioSource AudioSourceAxe => _audioSourceAxe;
        public AudioSource AudioSourceStep => _audioStep;
        public AudioSource AudioPlayerAbility => _audioPlayerAbility;

        public void SetSoundValue(float value)
        {
            _audioSourceAxe.volume = value;
            _audioStep.volume = value;
            _audioPlayerState.volume = value;
            _audioPlayerAbility.volume = value;
        }
    }
}