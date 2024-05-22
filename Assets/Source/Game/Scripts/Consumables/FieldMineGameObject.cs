using UnityEngine;

public class FieldMineGameObject : ItemGameObject
{
    [SerializeField] private Transform _particleContainer;
    [Header("[Sound]")]
    [SerializeField] private AudioSource _audioSource;

    private AudioClip _explosionAudio;
    private int _damage;
    private ParticleSystem _explosion;

    [System.Obsolete]
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(_damage);
            _explosion.Play();
            _audioSource.PlayOneShot(_explosionAudio);
            Destroy(gameObject, _explosion.duration);
        }
    }

    [System.Obsolete]
    public void Initialize(ConsumableItemData consumableItemData)
    {
        _damage = consumableItemData.Value;
        _explosionAudio = consumableItemData.UseConsumable;
        _explosion = Instantiate(consumableItemData.Effect, _particleContainer);
    }
}