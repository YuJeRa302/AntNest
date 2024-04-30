using UnityEngine;

public class LevelParameters : MonoBehaviour
{
    [Header("[Level View]")]
    [SerializeField] private LevelView _levelView;
    [Header("[WavePanel View]")]
    [SerializeField] private WavePanelView _wavePanelView;
    [Header("[Level Spawn]")]
    [SerializeField] private LevelSpawn _levelSpawn;
    [Header("[Runes Spawn]")]
    [SerializeField] private RuneSpawn _runeSpawn;
    [Header("[Level Observer]")]
    [SerializeField] private LevelObserver _levelObserver;
    [Header("[Level Sound]")]
    [SerializeField] private LevelSounds _levelSounds;
    [Header("[CanvasLoader]")]
    [SerializeField] private CanvasLoader _canvasLoader;

    private LoadConfig _loadConfig;
    private Player _player;
    private Levels _levels;

    public int IndexEndlessWave => 0;
    public Player Player => _player;
    public CanvasLoader CanvasLoader => _canvasLoader;
    public LoadConfig LoadConfig => _loadConfig;
    public LevelSpawn LevelSpawn => _levelSpawn;
    public LevelObserver LevelObserver => _levelObserver;
    public WavePanelView WavePanelView => _wavePanelView;
    public LevelView LevelView => _levelView;
    public Levels Levels => _levels;
    public AudioSource LevelSound => _levelSounds.AudioSource;

    //public void Initialize(LoadConfig loadConfig)
    //{
    //    _player = FindObjectOfType<Player>();
    //    _loadConfig = loadConfig;
    //    _levels = _loadConfig.Levels;
    //    CalculateTotalNumberOfEnemies();
    //    LevelSpawnEntity();
    //    LoadPlayerStats();
    //    LoadSceneUI();
    //}

    //public void ChangePlayerState(bool state)
    //{
    //    _player.enabled = state;
    //}

    //public void SpawnNextWave(bool isPlayerAlive, int currentCountEnemy)
    //{
    //    if (isPlayerAlive == true)
    //    {
    //        if (_levels.IsStandart == true) SetStandartSpawn(currentCountEnemy);
    //        else SetEndlessSpawn(currentCountEnemy);
    //    }
    //    else return;
    //}

    //private void CalculateTotalNumberOfEnemies()
    //{
    //    for (int index = 0; index < _levels.Wave.Length; index++)
    //    {
    //        _totalCountEnemy += _levels.Wave[index].CountEnemy;
    //    }
    //}

    //private void SetStandartSpawn(int currentCountEnemy)
    //{
    //    if (_currentWave < _levels.Wave.Length - 1)
    //    {
    //        if (currentCountEnemy == _levels.Wave[_currentWave].CountEnemy)
    //        {
    //            SetWaveParameters(_levels.Wave);
    //        }
    //    }
    //    else if (_totalCountEnemy == _levelObserver.CountKillEnemy) _levelObserver.GiveWinPlayer();
    //}

    //private void SetEndlessSpawn(int currentCountEnemy)
    //{
    //    if (currentCountEnemy == _levels.Wave[_indexEndlessWave].CountEnemy + _currentWave)
    //    {
    //        SetWaveParameters(_levels.Wave);
    //    }
    //}

    //private void SetWaveParameters(Wave[] waves)
    //{
    //    _currentWave++;
    //    _levelSpawn.SpawnNextWave(waves, _currentWave);
    //    _levelObserver.SetDefaultCountEnemy();
    //}

    //private void LoadSceneUI()
    //{
    //    _levelView.Initialize(_levels.NameScene,
    //        _levels.NameEnemy,
    //        _levels.Sprite,
    //        _levels.Wave[0].EnemyPrefab.Sprite,
    //        _levelObserver.CountKillEnemy);
    //}

    //private void LoadPlayerStats()
    //{
    //    _player.PlayerSounds.Initialize(_loadConfig.AmbientVolume);
    //    _player.Wallet.Initialize(_loadConfig.PlayerCoins);
    //    _player.PlayerStats.Initialize(_loadConfig.PlayerLevel, _loadConfig.PlayerExperience, _loadConfig.PlayerScore);
    //}

    //private void LevelSpawnEntity()
    //{
    //    _levelSpawn.Initialize(_levels, _currentWave);
    //    _runeSpawn.Initialize();
    //}
}