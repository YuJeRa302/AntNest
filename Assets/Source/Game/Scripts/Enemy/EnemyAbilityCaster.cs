using System.Collections;
using UnityEngine;

namespace Assets.Source.Game.Scripts
{
    public class EnemyAbilityCaster : MonoBehaviour
    {
        private readonly int _minValue = 0;
        private readonly int _maxValue = 1;

        [SerializeField] private float _coolDown;
        [SerializeField] private int _damage;
        [SerializeField] private Enemy _enemy;

        private ParticleSystem _castAbilityParticle;
        private bool _isUseAbility = true;

        private void Start()
        {
            StartCoroutine(AbilityCoolDown(_coolDown));
        }

        private void OnTriggerStay(Collider collision)
        {
            if (collision.gameObject.TryGetComponent(out Player player))
            {
                if (_isUseAbility == false)
                    CastAbility(player);
            }
        }

        public void Initialize(EnemyData enemyData, ParticleSystem castAbilityParticle)
        {
            _castAbilityParticle = castAbilityParticle;
            _coolDown = enemyData.AbilityCoolDown;
            _damage = enemyData.AbilityDamage;
        }

        private IEnumerator AbilityCoolDown(float coolDown)
        {
            float animationTime = coolDown;

            while (animationTime > _minValue)
            {
                animationTime -= Time.deltaTime;
                _enemy.EnemyHealthBar.CoolDownImage.fillAmount = animationTime / coolDown;
                yield return null;
            }

            UpdateValue(false, _minValue);
        }

        private void CastAbility(Player player)
        {
            player.PlayerStats.PlayerHealth.TakeDamage(_damage);
            _castAbilityParticle.Play();
            UpdateValue(true, _maxValue);
            _enemy.EnemyHealthBar.CoolDownImage.sprite = _enemy.EnemyHealthBar.CancelSprite;
        }

        private void UpdateValue(bool state, int delay)
        {
            _isUseAbility = state;
            _enemy.EnemyHealthBar.CoolDownImage.fillAmount = delay;
        }
    }
}