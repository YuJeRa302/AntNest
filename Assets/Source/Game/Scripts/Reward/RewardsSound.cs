using UnityEngine;

public class RewardsSound : MonoBehaviour
{
    [Header("[Audio Source]")]
    [SerializeField] private AudioSource _audioSource;
    [Header("[Audio Clip]")]
    [SerializeField] private AudioClip _audioClipWin;
    [SerializeField] private AudioClip _audioClipLose;

    public AudioSource AudioSource => _audioSource;
    public AudioClip AudioClipWin => _audioClipWin;
    public AudioClip AudioClipLose => _audioClipLose;
}
