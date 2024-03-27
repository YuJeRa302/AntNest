using UnityEngine;

[RequireComponent(typeof(Wallet))]
[RequireComponent(typeof(PlayerStats))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerEffects))]
[RequireComponent(typeof(PlayerSound))]
[RequireComponent(typeof(PlayerConsumables))]
[RequireComponent(typeof(PlayerEquipment))]

public class Player : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;
    [SerializeField] private PlayerStats _playerStats;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerEffects _playerEffects;
    [SerializeField] private PlayerSound _playerSound;
    [SerializeField] private PlayerView _playerView;
    [SerializeField] private PlayerConsumables _playerConsumables;
    [SerializeField] private PlayerEquipment _playerEquipment;

    public PlayerStats PlayerStats => _playerStats;
    public PlayerSound PlayerSounds => _playerSound;
    public PlayerView PlayerView => _playerView;
    public PlayerConsumables PlayerConsumables => _playerConsumables;
    public Wallet Wallet => _wallet;
    public PlayerEquipment PlayerEquipment => _playerEquipment;
}