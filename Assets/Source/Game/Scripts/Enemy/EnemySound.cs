using UnityEngine;

public class EnemySound : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [Header("[Audio Source]")]
    [SerializeField] private AudioSource _audioSource;
    [Header("[Audio Clip]")]
    [SerializeField] private AudioClip _audioClipDie;
    [SerializeField] private AudioClip _hitPlayer;

    public void Initialize(float soundVolume, EnemyData enemyData)
    {
        _audioSource.volume = soundVolume;
        _audioClipDie = enemyData.AudioClipDie;
        _hitPlayer = enemyData.HitPlayer;
    }

    public void PlayHitSound()
    {
        _audioSource.PlayOneShot(_hitPlayer);
    }

    public void PlayDieSound()
    {
        _audioSource.PlayOneShot(_audioClipDie);
    }
}