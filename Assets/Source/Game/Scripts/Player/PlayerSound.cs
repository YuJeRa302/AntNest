using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    [Header("[Audio Source Axe]")]
    [SerializeField] private AudioSource _audioSourceAxe;
    [Header("[Audio Source Step]")]
    [SerializeField] private AudioSource _audioStep;
    [Header("[Audio Source Player State]")]
    [SerializeField] private AudioSource _audioPlayerState;
    [Header("[Audio Source Player Ability]")]
    [SerializeField] private AudioSource _audioPlayerAbility;
    [Header("[Audio Clips]")]
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
