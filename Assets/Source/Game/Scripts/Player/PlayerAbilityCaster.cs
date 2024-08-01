using System;
using System.Collections;
using UnityEngine;

namespace Assets.Source.Game.Scripts
{
    public class PlayerAbilityCaster : MonoBehaviour
    {
        [SerializeField] private Player _player;

        private IEnumerator _abilityDuration;

        public event Action<AbilityState> AbilityBuyed;
        public event Action<TypeAbility, int> AbilityUsed;
        public event Action<TypeAbility, int> AbilityEnded;

        public void BuyAbility(AbilityState abilityState)
        {
            if (abilityState == null)
                return;

            abilityState.IsBuyed = true;

            if (AbilityBuyed != null)
                AbilityBuyed?.Invoke(abilityState);
        }

        public void UpgradeAbility(AbilityState abilityState)
        {
            abilityState.CurrentLevel++;
            BuyAbility(abilityState);
        }

        public void UseAbility(TypeAbility typeAbility, float abilityDuration, int abilityValue)
        {
            _abilityDuration = AbilityDuration(typeAbility, abilityDuration, abilityValue);
            StartCoroutine(_abilityDuration);

            if (AbilityUsed != null)
                AbilityUsed?.Invoke(typeAbility, abilityValue);
        }

        private IEnumerator AbilityDuration(TypeAbility typeAbility, float abilityDuration, int abilityValue)
        {
            while (abilityDuration > 0)
            {
                abilityDuration -= Time.deltaTime;
                yield return null;
            }

            _player.PlayerView.AbilityEffect.Stop();
            StopCoroutine(_abilityDuration);

            if (AbilityEnded != null)
                AbilityEnded?.Invoke(typeAbility, abilityValue);
        }
    }
}