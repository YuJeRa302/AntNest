using UnityEngine;

namespace Assets.Source.Game.Scripts
{
    public class FieldMine : Item
    {
        [Header("[Attack Point Parameters]")]
        [SerializeField] private Transform _attackPoint;
        [SerializeField] private float _attackRange = 0.5f;
        [SerializeField] private LayerMask _enemyLayers;
        [SerializeField] private Transform _particleContainer;
        [Header("[Sound]")]
        [SerializeField] private AudioSource _audioSource;

        private AudioClip _explosionAudio;
        private int _damage;
        private ParticleSystem _explosion;
        private bool _isEnemyExist = false;

        private void OnDrawGizmosSelected()
        {
            if (_attackPoint != null)
                Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.TryGetComponent(out Enemy enemy) && _isEnemyExist == false)
            {
                _isEnemyExist = true;
                FindAttackedEnemy();
            }
        }

        public void Initialize(ConsumableItemData consumableItemData)
        {
            _damage = consumableItemData.Value;
            _explosionAudio = consumableItemData.ConsumableAudioClip;
            _explosion = Instantiate(consumableItemData.Effect, _particleContainer);
        }

        private void FindAttackedEnemy()
        {
            Collider[] coliderEnemy = Physics.OverlapSphere(_attackPoint.position, _attackRange, _enemyLayers);

            foreach (Collider collider in coliderEnemy)
            {
                if (collider.TryGetComponent(out Enemy enemy))
                {
                    enemy.TakeDamage(_damage);
                    _explosion.Play();
                    _audioSource.PlayOneShot(_explosionAudio);
                    Destroy(gameObject, _explosion.duration);
                }
            }
        }
    }
}