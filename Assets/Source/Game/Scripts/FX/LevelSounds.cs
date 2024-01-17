using UnityEngine;

public class LevelSounds : MonoBehaviour
{
    [Header("[Audio Source]")]
    [SerializeField] private AudioSource _audioSource;

    public AudioSource AudioSource => _audioSource;

    public void SetValueVolume(float value)
    {
        _audioSource.volume = value;
    }
}