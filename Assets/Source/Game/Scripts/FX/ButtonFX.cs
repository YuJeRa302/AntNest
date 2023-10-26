using UnityEngine;

public class ButtonFX : MonoBehaviour
{
    [Header("[Audio Source]")]
    [SerializeField] private AudioSource _audioSource;
    [Header("[Audio Clips]")]
    [SerializeField] private AudioClip _audioHover;
    [SerializeField] private AudioClip _audioClick;

    public void HoverSound()
    {
        _audioSource.PlayOneShot(_audioHover);
    }

    public void SetValueVolume(float value)
    {
        _audioSource.volume = value;
    }

    public void ClickSound()
    {
        _audioSource.PlayOneShot(_audioClick);
    }
}