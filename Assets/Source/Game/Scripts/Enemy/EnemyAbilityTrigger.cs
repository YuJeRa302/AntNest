using UnityEngine;

public class EnemyAbilityTrigger : MonoBehaviour
{
    [Header("[Enemy]")]
    [SerializeField] private Enemy _enemy;

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            player.TakeDamage(_enemy.AbilityDamage);
        }
        else return;
    }
}