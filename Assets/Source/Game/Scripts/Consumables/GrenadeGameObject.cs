using System.Collections;
using UnityEngine;

public class GrenadeGameObject : ItemGameObject
{
    [Header("[Attack Point Parameters]")]
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _attackRange = 0.5f;
    [SerializeField] private LayerMask _enemyLayers;
    [Header("[Grenade Parameters]")]
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _throwForce;
    [Header("[Sound]")]
    [SerializeField] private AudioSource _audioSource;

    private readonly float _lifeTime = 1f;

    private AudioClip _explosionAudio;
    private int _damage;
    private ParticleSystem _explosion;

    public Rigidbody Rigidbody => _rigidbody;
    public float ThrowForce => _throwForce;

    private void OnDrawGizmosSelected()
    {
        if (_attackPoint == null)
            return;
        else
            Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
    }

    [System.Obsolete]
    public void Initialize(ConsumableItemData consumableItemData)
    {
        _explosionAudio = consumableItemData.UseConsumable;
        _damage = consumableItemData.Value;
        _explosion = consumableItemData.Effect;
        StartCoroutine(Destroy());
    }

    [System.Obsolete]
    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(_lifeTime);
        FindAttackedEnemy();
        CreateEffect();
        _audioSource.PlayOneShot(_explosionAudio);
        Destroy(gameObject, _explosion.duration);
    }

    private void CreateEffect()
    {
        _explosion = Instantiate(_explosion, new Vector3(gameObject.transform.localPosition.x,
                gameObject.transform.localPosition.y, gameObject.transform.localPosition.z), Quaternion.identity);
        _explosion.Play();
    }

    private void FindAttackedEnemy()
    {
        Collider[] coliderEnemy = Physics.OverlapSphere(_attackPoint.position, _attackRange, _enemyLayers);

        foreach (Collider collider in coliderEnemy)
        {
            if (collider.TryGetComponent<Enemy>(out Enemy enemy))
                enemy.TakeDamage(_damage);
        }
    }
}
