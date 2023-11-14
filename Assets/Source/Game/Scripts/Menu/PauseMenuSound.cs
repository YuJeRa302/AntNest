using UnityEngine;

public class PauseMenuSound : MonoBehaviour
{
    [Header("[Sound]")]
    [SerializeField] private AudioSource _ambientSounds;
    [SerializeField] private AudioSource _buttonFX;

    public AudioSource AudioSourceAmbient => _ambientSounds;
    public AudioSource AudioSourceButtons => _buttonFX;

    public void SetStateMute(bool state)
    {
        _ambientSounds.mute = state;
        _buttonFX.mute = state;
    }
}