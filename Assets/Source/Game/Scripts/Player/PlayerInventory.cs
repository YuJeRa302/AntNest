using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Source.Game.Scripts
{
    public class PlayerInventory : MonoBehaviour
    {
        private const int DefaultItemLevel = 1;

        [SerializeField] private Player _player;
        [SerializeField] private PlayerEquipment _playerEquipment;
        [SerializeField] private PlayerEquipmentState _defaultPlayerEquipmentState;
        [SerializeField] private PlayerAbilityState _defaultAbilityState;
        [SerializeField] private PlayerConsumableState _defaultConsumableState;

        private PlayerEquipmentState _playerEquipmentState;
        private PlayerAbilityState _playerAbilityState;
        private PlayerConsumableState _playerConsumableState;

        public List<EquipmentItemState> ListWeapon => _playerEquipmentState.Items.Where(item => item.ItemData.ItemType == TypeItem.Weapon).ToList();
        public List<EquipmentItemState> ListArmor => _playerEquipmentState.Items.Where(item => item.ItemData.ItemType == TypeItem.Armor).ToList();
        public List<AbilityState> ListAbilities => _playerAbilityState.Items.ToList();
        public List<ConsumableItemState> ListConsumables => _playerConsumableState.Items.ToList();
        public EquipmentItemState CurrentWeapon => _playerEquipmentState.EquippedWeapon;
        public EquipmentItemState CurrentArmor => _playerEquipmentState.EquippedArmor;

        private void Awake()
        {
            LoadDefaultState();
            _playerEquipment.Initialize(_playerEquipmentState);
            _player.PlayerConsumablesUser.Initialize();
            AddDefaultEquipment(ListWeapon);
            AddDefaultEquipment(ListArmor);
            _player.PlayerView.UpdatePlayerStats();
        }

        public void RemoveUnnecessaryAbility(AbilityState abilityState)
        {
            _playerAbilityState.Items.Remove(abilityState);
        }

        private void LoadDefaultState()
        {
            _playerEquipmentState = _defaultPlayerEquipmentState;
            _playerAbilityState = _defaultAbilityState;
            _playerConsumableState = _defaultConsumableState;
        }

        private void AddDefaultEquipment(List<EquipmentItemState> equipmentItemStates)
        {
            foreach (var item in equipmentItemStates)
            {
                if (item.ItemData.Level == DefaultItemLevel)
                    AddEquipment(item);
            }
        }

        private void AddEquipment(EquipmentItemState equipmentItemState)
        {
            _playerEquipment.BuyItem(equipmentItemState);
            _playerEquipment.AddItem(equipmentItemState);
        }
    }
}