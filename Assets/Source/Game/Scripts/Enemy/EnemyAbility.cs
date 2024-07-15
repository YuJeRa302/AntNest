using System;
using System.Collections;
using UnityEngine;

public class EnemyAbility : MonoBehaviour
{
    private readonly int _minValue = 0;
    private readonly int _maxValue = 1;

    [Header("[Stats]")]
    [SerializeField] private float _coolDown;
    [SerializeField] private int _damage;
    [Header("[Enemy]")]
    [SerializeField] private Enemy _enemy;

    private bool _isUseAbility = true;

    public event Action AbilityUsed;

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
        else
            return;
    }

    public void Initialize(EnemyData enemyData)
    {
        _coolDown = enemyData.AbilityCoolDown;
        _damage = enemyData.AbilityDamage;
    }

    private IEnumerator AbilityCoolDown(float coolDown)
    {
        float animationTime = coolDown;

        while (animationTime > _minValue)
        {
            animationTime -= Time.deltaTime;
            _enemy.EnemyView.CoolDownImage.fillAmount = animationTime / coolDown;
            yield return null;
        }

        UpdateValue(false, _minValue);
    }

    private void CastAbility(Player player)
    {
        AbilityUsed.Invoke();
        player.PlayerStats.PlayerHealth.TakeDamage(_damage);
        UpdateValue(true, _maxValue);
        _enemy.EnemyView.CoolDownImage.sprite = _enemy.EnemyView.CancelSprite;
    }

    private void UpdateValue(bool state, int delay)
    {
        _isUseAbility = state;
        _enemy.EnemyView.CoolDownImage.fillAmount = delay;
    }
}