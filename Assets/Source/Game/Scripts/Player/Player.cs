using UnityEngine;

[RequireComponent(typeof(PlayerStats))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerEffects))]
[RequireComponent(typeof(PlayerSound))]
[RequireComponent(typeof(PlayerConsumables))]

public class Player : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;
    [SerializeField] private PlayerStats _playerStats;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerEffects _playerEffects;
    [SerializeField] private PlayerSound _playerSound;
    [SerializeField] private PlayerView _playerView;
    [SerializeField] private PlayerConsumables _playerConsumables;

    public PlayerStats PlayerStats => _playerStats;
    public PlayerSound PlayerSounds => _playerSound;
    public PlayerView PlayerView => _playerView;
    public PlayerConsumables PlayerConsumables => _playerConsumables;
    public Wallet Wallet => _wallet;
}