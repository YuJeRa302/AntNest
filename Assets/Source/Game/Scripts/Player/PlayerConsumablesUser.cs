using System;
using UnityEngine;

namespace Assets.Source.Game.Scripts
{
    public class PlayerConsumablesUser : MonoBehaviour
    {
        [SerializeField] private ConsumableButton _grenadeButton;
        [SerializeField] private ConsumableButton _potionButton;
        [SerializeField] private ConsumableButton _mineButton;
        [SerializeField] private Player _player;
        [SerializeField] private Transform _throwPoint;

        private int _minValue = 0;

        public event Action<TypeConsumable> ConsumableBuyed;
        public event Action<TypeConsumable> ConsumableUsed;

        private void OnDestroy()
        {
            _grenadeButton.ConsumableUsed -= UseGrenade;
            _potionButton.ConsumableUsed -= UseHealthPotion;
            _mineButton.ConsumableUsed -= UseMine;
        }

        public void Initialize()
        {
            foreach (var item in _player.PlayerInventory.ListConsumables)
            {
                if (item.ConsumableItemData.TypeConsumable == TypeConsumable.HealthPotion)
                    _potionButton.Initialize(item, _player);
                else if (item.ConsumableItemData.TypeConsumable == TypeConsumable.LandMine)
                    _mineButton.Initialize(item, _player);
                else
                    _grenadeButton.Initialize(item, _player);
            }

            AddListener();
        }

        public void BuyItem(ConsumableItemState consumableItemState)
        {
            ConsumableBuyed?.Invoke(consumableItemState.ConsumableItemData.TypeConsumable);
        }

        private void AddListener()
        {
            _grenadeButton.ConsumableUsed += UseGrenade;
            _potionButton.ConsumableUsed += UseHealthPotion;
            _mineButton.ConsumableUsed += UseMine;
        }

        private void UseHealthPotion(ConsumableItemData consumableItemData)
        {
            if (_player.PlayerStats.PlayerHealth.CurrentHealth != _player.PlayerStats.PlayerHealth.MaxHealth)
            {
                _player.PlayerStats.PlayerHealth.ChangeHealth(consumableItemData.Value);
                ConsumableUsed?.Invoke(consumableItemData.TypeConsumable);
            }
        }

        private void UseMine(ConsumableItemData consumableItemData)
        {
            Item item = Instantiate(
                consumableItemData.Item,
                new Vector3(_player.transform.localPosition.x, _minValue, _player.transform.localPosition.z),
                Quaternion.identity);

            (item as FieldMine).Initialize(consumableItemData);
            ConsumableUsed?.Invoke(consumableItemData.TypeConsumable);
        }

        private void UseGrenade(ConsumableItemData consumableItemData)
        {
            Item item = Instantiate(
                consumableItemData.Item,
                new Vector3(_throwPoint.position.x, _throwPoint.position.y, _throwPoint.position.z),
                Quaternion.identity);

            (item as Grenade).Initialize(consumableItemData);
            (item as Grenade).Rigidbody.AddForce(_throwPoint.forward * (item as Grenade).ThrowForce, ForceMode.VelocityChange);
            ConsumableUsed?.Invoke(consumableItemData.TypeConsumable);
        }
    }
}