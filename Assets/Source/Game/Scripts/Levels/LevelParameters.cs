using UnityEngine;

public class LevelParameters : MonoBehaviour
{
    [Header("[Level Spawn]")]
    [SerializeField] private LevelSpawn _levelSpawn;
    [Header("[Runes Spawn]")]
    [SerializeField] private RuneSpawn _runeSpawn;
    [Header("[LevelUI]")]
    [SerializeField] private LevelUI _levelUI;
    [Header("[WavePanelView]")]
    [SerializeField] private WavePanelView _wavePanelView;
    [Header("[PauseMenu]")]
    [SerializeField] private PauseMenu _pauseMenu;

    private readonly int _indexEndlessWave = 0;
    private readonly int _levelCompleteBonus = 150;

    private LoadConfig _loadConfig;
    private Player _player;
    private Levels _levels;
    private bool _isPlayerAlive = true;
    private int _countKillEnemy = 0;
    private int _countMoneyEarned;
    private int _countExp;
    private int _totalCountEnemy;
    private int _currentCountEnemy = 0;
    private int _currentWave = 0;

    public int CountMoneyEarned => _countMoneyEarned;
    public int CountEnemyDie => _countKillEnemy;
    public LoadConfig LoadConfig => _loadConfig;
    public LevelSpawn LevelSpawn => _levelSpawn;
    public WavePanelView WavePanelView => _wavePanelView;
    public Levels Levels => _levels;
    public int IndexEndlessWave => _indexEndlessWave;
    public Player Player => _player;

    private void Start()
    {
        _player.PlayerEquipment.ShowHideEquipment();
    }

    public void OnEnemyDie(Enemy enemy)
    {
        _countKillEnemy++;
        _countMoneyEarned += enemy.GoldReward;
        _countExp += enemy.ExperienceReward;
        _player.PlayerStats.OnEnemyDie(enemy);
        enemy.Dying -= OnEnemyDie;
        _currentCountEnemy++;
        TrySpawnNextWave();
        _levelUI.UpdateEnemyKillCount(_countKillEnemy);
    }

    public void EnabledDisabledPlayer(bool state)
    {
        _player.enabled = state;
    }

    public void Initialized(LoadConfig loadConfig)
    {
        _player = FindObjectOfType<Player>();
        _loadConfig = loadConfig;
        _levels = _loadConfig.Levels;
        CalculateTotalNumberOfEnemies();
        LevelSpawnEntity();
        LoadPlayerStats();
        LoadSceneUi();
    }

    private void TrySpawnNextWave()
    {
        if (_isPlayerAlive == true)
        {
            if (_levels.IsStandart == true) SetStandartSpawn();
            else SetEndlessSpawn();
        }
        else return;
    }

    private void CalculateTotalNumberOfEnemies()
    {
        for (int index = 0; index < _levels.Wave.Length; index++)
        {
            _totalCountEnemy += _levels.Wave[index].CountEnemy;
        }
    }

    private void SetStandartSpawn()
    {
        if (_currentWave < _levels.Wave.Length - 1)
        {
            if (_currentCountEnemy == _levels.Wave[_currentWave].CountEnemy)
            {
                _currentWave++;
                _levelSpawn.SpawnNextWave(_levels.Wave, _currentWave);
                _currentCountEnemy = 0;
            }
        }
        else if (_totalCountEnemy == _countKillEnemy) WinPlayer();
    }

    private void SetEndlessSpawn()
    {
        if (_currentCountEnemy == _levels.Wave[_indexEndlessWave].CountEnemy)
        {
            _currentWave++;
            _levelSpawn.SpawnNextWave(_levels.Wave, _currentWave);
            _currentCountEnemy = 0;
        }
    }

    private void OnPlayerDie()
    {
        _isPlayerAlive = false;
        WinEnemy();
    }

    private void WinEnemy()
    {
        CompleteLevel(false);
    }

    private void WinPlayer()
    {
        _levels.SetComplete();
        _countMoneyEarned += _levelCompleteBonus;
        CompleteLevel(true);
    }

    private void CompleteLevel(bool state)
    {
        _pauseMenu.PauseGame();
        _pauseMenu.MuteAbientSound();
        _pauseMenu.RewardView.ShowRewardPanel(state, _countMoneyEarned, _countExp, _countKillEnemy);
        _player.PlayerDie -= OnPlayerDie;
    }

    private void LoadSceneUi()
    {
        _levelUI.LoadLevelUi(_levels.NameScene, _levels.NameEnemy,
            _levels.Sprite, _levels.Wave[0].EnemyPrefab.Sprite, _countKillEnemy);
    }

    private void LoadPlayerStats()
    {
        _player.PlayerSounds.Initialized(_loadConfig.AmbientVolume);
        _player.PlayerEquipment.Initialized();
        _isPlayerAlive = true;
        _player.PlayerDie += OnPlayerDie;
        _player.Wallet.Initialized(_loadConfig.PlayerCoins);
        _player.PlayerStats.Initialized(_loadConfig.PlayerLevel, _loadConfig.PlayerExperience);
    }

    private void LevelSpawnEntity()
    {
        _levelSpawn.Initialized(_levels, _currentWave);
        _runeSpawn.Initialized();
    }
}