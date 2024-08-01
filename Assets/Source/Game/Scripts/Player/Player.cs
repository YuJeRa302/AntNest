using UnityEngine;

namespace Assets.Source.Game.Scripts
{
    [RequireComponent(typeof(Wallet))]
    [RequireComponent(typeof(PlayerStats))]
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(PlayerSoundPlayer))]
    [RequireComponent(typeof(PlayerConsumablesUser))]
    [RequireComponent(typeof(PlayerInventory))]
    [RequireComponent(typeof(PlayerEquipment))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private Wallet _wallet;
        [SerializeField] private PlayerStats _playerStats;
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private PlayerSoundPlayer _playerSound;
        [SerializeField] private PlayerView _playerView;
        [SerializeField] private PlayerConsumablesUser _playerConsumablesUser;
        [SerializeField] private PlayerInventory _playerInventory;
        [SerializeField] private PlayerEquipment _playerEquipment;
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private PlayerCamera _playerUICamera;

        public PlayerStats PlayerStats => _playerStats;
        public PlayerSoundPlayer PlayerSounds => _playerSound;
        public PlayerView PlayerView => _playerView;
        public PlayerConsumablesUser PlayerConsumablesUser => _playerConsumablesUser;
        public Wallet Wallet => _wallet;
        public PlayerInventory PlayerInventory => _playerInventory;
        public PlayerInput PlayerInput => _playerInput;
        public PlayerCamera PlayerUICamera => _playerUICamera;
    }
}