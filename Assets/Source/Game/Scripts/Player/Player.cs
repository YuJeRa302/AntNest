using UnityEngine;

[RequireComponent(typeof(Wallet))]
[RequireComponent(typeof(PlayerStats))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerEffects))]
[RequireComponent(typeof(PlayerSound))]
[RequireComponent(typeof(PlayerConsumables))]
[RequireComponent(typeof(PlayerArmorEquipment))]
[RequireComponent(typeof(PlayerWeaponEquipment))]
[RequireComponent(typeof(PlayerInventory))]
public class Player : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;
    [SerializeField] private PlayerStats _playerStats;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerEffects _playerEffects;
    [SerializeField] private PlayerSound _playerSound;
    [SerializeField] private PlayerView _playerView;
    [SerializeField] private PlayerConsumables _playerConsumables;
    [SerializeField] private PlayerArmorEquipment _playerArmorEquipment;
    [SerializeField] private PlayerWeaponEquipment _playerWeaponEquipment;
    [SerializeField] private PlayerInventory _playerInventory;

    public PlayerStats PlayerStats => _playerStats;
    public PlayerSound PlayerSounds => _playerSound;
    public PlayerView PlayerView => _playerView;
    public PlayerConsumables PlayerConsumables => _playerConsumables;
    public Wallet Wallet => _wallet;
    public PlayerArmorEquipment PlayerArmorEquipment => _playerArmorEquipment;
    public PlayerWeaponEquipment PlayerWeaponEquipment => _playerWeaponEquipment;
    public PlayerInventory PlayerInventory => _playerInventory;
}