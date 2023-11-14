using UnityEngine;

public class EnemySound : MonoBehaviour
{
    [Header("[Audio Source]")]
    [SerializeField] private AudioSource _audioSource;
    [Header("[Audio Clip]")]
    [SerializeField] private AudioClip _audioClipDie;
    [SerializeField] private AudioClip _hitPlayer;

    public AudioClip AudioClipDie => _audioClipDie;
    public AudioClip HitPlayer => _hitPlayer;
    public AudioSource AudioSource => _audioSource;
}