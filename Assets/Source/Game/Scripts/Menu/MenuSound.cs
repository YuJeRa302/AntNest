using UnityEngine;

public class MenuSound : MonoBehaviour
{
    [Header("[Sound]")]
    [SerializeField] private AudioSource _ambientSounds;
    [SerializeField] private AudioSource _buttonFX;

    public AudioSource AmbientSounds => _ambientSounds;
    public AudioSource ButtonFX => _buttonFX;

    public void Initialized()
    {
        _ambientSounds.Play();
    }

    public void SetValueVolume(float ambientSoundsValue, float buttonFXValue)
    {
        _ambientSounds.volume = ambientSoundsValue;
        _buttonFX.volume = buttonFXValue;
    }

    public void SetStateMuteSound(bool state)
    {
        _ambientSounds.mute = state;
    }
}
