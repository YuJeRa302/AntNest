using UnityEngine;

namespace Assets.Source.Game.Scripts
{
    public class EnemySoundPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _audioClipDie;
        [SerializeField] private AudioClip _hitPlayer;
        [SerializeField] private Enemy _enemy;

        private void OnDestroy()
        {
            _enemy.Dying -= OnEnemyDying;
        }

        public void Initialize(float soundVolume, EnemyData enemyData)
        {
            _audioSource.volume = soundVolume;
            _audioClipDie = enemyData.AudioClipDie;
            _hitPlayer = enemyData.HitPlayer;
            _enemy.Dying += OnEnemyDying;
        }

        private void OnEnemyDying(Enemy enemy)
        {
            _audioSource.PlayOneShot(_audioClipDie);
        }

        private void PlayHitSound()
        {
            _audioSource.PlayOneShot(_hitPlayer);
        }
    }
}