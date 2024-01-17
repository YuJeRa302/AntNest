using UnityEngine;

[RequireComponent(typeof(PlayerStats))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerEffects))]
[RequireComponent(typeof(PlayerSound))]
[RequireComponent(typeof(PlayerConsumables))]

public class Player : MonoBehaviour
{
    [Header("[Wallet]")]
    [SerializeField] private Wallet _wallet;
    [Header("[Player Stats]")]
    [SerializeField] private PlayerStats _playerStats;
    [Header("[Player Movement]")]
    [SerializeField] private PlayerMovement _playerMovement;
    [Header("[Player Effects]")]
    [SerializeField] private PlayerEffects _playerEffects;
    [Header("[Player Sound]")]
    [SerializeField] private PlayerSound _playerSound;
    [Header("[Player View]")]
    [SerializeField] private PlayerView _playerView;
    [Header("[Player Consumables]")]
    [SerializeField] private PlayerConsumables _playerConsumables;

    public PlayerStats PlayerStats => _playerStats;
    public PlayerSound PlayerSounds => _playerSound;
    public PlayerView PlayerView => _playerView;
    public PlayerConsumables PlayerConsumables => _playerConsumables;
    public Wallet Wallet => _wallet;
}