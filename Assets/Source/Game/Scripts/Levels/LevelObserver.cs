using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;
using UnityEngine.UI;
using Lean.Localization;
using Source.Game.Scripts;

public class LevelObserver : MonoBehaviour
{
    private readonly int _levelCompleteBonus = 150;
    private readonly string _menuScene = "Menu";
    private readonly float _maxLoadProgressValue = 0.9f;
    private readonly float _pauseValue = 0;

    [Header("[Level Entities]")]
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private LevelView _levelView;
    [SerializeField] private RuneSpawn _runeSpawner;
    [SerializeField] private SaveProgress _saveProgress;
    [SerializeField] private Player _player;
    [SerializeField] private PlayerInterfaceView _playerInterfaceView;
    [SerializeField] private SoundController _soundController;
    [SerializeField] private CanvasLoader _canvasLoader;
    [Header("[Panels]")]
    [SerializeField] private GamePanels[] _panels;
    [Header("[Joystick]")]
    [SerializeField] private Joystick _joystick;
    [Header("[Buttons]")]
    [SerializeField] private Button _soundButton;
    [SerializeField] private Button _closeGameButton;
    [Header("[LeanLocalization]")]
    [SerializeField] private LeanLocalization _leanLocalization;
    [Space(50)]
    [SerializeField] private PauseHandler _pauseHandler;

    private bool _isMuteSound = false;
    private int _countKillEnemy = 0;
    private int _countMoneyEarned = 0;
    private int _countExpEarned = 0;
    private AsyncOperation _load;
    private LoadConfig _loadConfig;
    private int _playerCoins;
    private int _playerExpirience;
    private int _playerLevel;
    private int _playerScore;

    public event Action GamePaused;
    public event Action GameResumed;
    public event Action GameEnded;
    public event Action GameClosed;
    public event Action<bool> SoundMuted;
    public event Action<int> KillCountUpdated;
    public event Action<bool> LevelCompleted;

    public int CountMoneyEarned => _countMoneyEarned;
    public int CountExpEarned => _countExpEarned;
    public int CountKillEnemy => _countKillEnemy;
    public LevelView LevelView => _levelView;
    public PlayerInterfaceView PlayerInterfaceView => _playerInterfaceView;
    public LoadConfig LoadConfig => _loadConfig;
    public SoundController SoundController => _soundController;
    public Player Player => _player;

    private void OnEnable()
    {
        AddPanelListener();
        _enemySpawner.EnemyDied += OnEnemyDied;
        _enemySpawner.LastEnemyDied += GiveWinPlayer;
        _player.PlayerStats.PlayerHealth.PlayerDied += OnPlayerDied;
        _player.Wallet.GoldenRuneTaked += OnGoldenRuneTaked;
        _soundButton.onClick.AddListener(MuteSound);
        _closeGameButton.onClick.AddListener(ExitGame);
    }

    private void OnDisable()
    {
        RemovePanelListener();
        _enemySpawner.EnemyDied -= OnEnemyDied;
        _enemySpawner.LastEnemyDied -= GiveWinPlayer;
        _player.PlayerStats.PlayerHealth.PlayerDied -= OnPlayerDied;
        _player.Wallet.GoldenRuneTaked -= OnGoldenRuneTaked;
        _soundButton.onClick.RemoveListener(MuteSound);
        _closeGameButton.onClick.RemoveListener(ExitGame);
    }

    public void Initialize(LoadConfig loadConfig)
    {
        _loadConfig = loadConfig;

        if (_loadConfig.TypeDevice == TypeDevice.Desktop)
            _playerInterfaceView.SetDesktopInterface();
        else
            _playerInterfaceView.SetMobileInterface();

        _loadConfig.SetPauseGameState(false);
        _enemySpawner.Initialize(loadConfig, _player);
        _runeSpawner.Initialize();
        _player.PlayerSounds.SetSoundValue(loadConfig.AmbientVolume);
        _player.PlayerStats.Initialize(_loadConfig.PlayerLevel, _loadConfig.PlayerExperience, _loadConfig.PlayerScore);
        _player.Wallet.Initialize(_loadConfig.PlayerCoins);
        LoadGamePanels();
    }

    private void LoadGamePanels()
    {
        foreach (var panel in _panels)
            panel.Initialize(_player, this);
    }

    private void CloseAllGamePanels()
    {
        foreach (var panel in _panels)
            panel.gameObject.SetActive(false);
    }

    private void AddPanelListener()
    {
        foreach (var panel in _panels)
        {
            if (panel is RewardPanel)
            {
                (panel as RewardPanel).RewardPanelClosed += OnCloseRewardPanel;
                (panel as RewardPanel).RewardScreenOpened += OnRewardTaked;
            }

            if (panel is PausePanel)
                (panel as PausePanel).LanguageChanged += OnLanguageChanged;

            panel.PanelOpened += PauseGame;
            panel.PanelClosed += ResumeGame;
            panel.AdOpened += OnOpenAd;
            panel.AdClosed += OnCloseAd;
        }
    }

    private void RemovePanelListener()
    {
        foreach (var panel in _panels)
        {
            if (panel is RewardPanel)
            {
                (panel as RewardPanel).RewardPanelClosed -= OnCloseRewardPanel;
                (panel as RewardPanel).RewardScreenOpened -= OnRewardTaked;
            }

            if (panel is PausePanel)
                (panel as PausePanel).LanguageChanged -= OnLanguageChanged;

            panel.PanelOpened -= PauseGame;
            panel.PanelClosed -= ResumeGame;
            panel.AdOpened -= OnOpenAd;
            panel.AdClosed -= OnCloseAd;
        }
    }

    private void OnLanguageChanged(string value)
    {
        _leanLocalization.SetCurrentLanguage(value);
        _loadConfig.SetCurrentLanguage(value);
    }

    private void OnGoldenRuneTaked(int value)
    {
        _countMoneyEarned += value;
    }

    private void OnRewardTaked(int value)
    {
        _playerCoins += value;
        _countMoneyEarned += value;
    }

    private void OnOpenAd()
    {
        SoundMuted?.Invoke(false);
    }

    private void OnCloseAd()
    {
        GameClosed?.Invoke();
        LoadLevel();
    }

    private void OnEnemyDied(Enemy enemy)
    {
        _countMoneyEarned += enemy.GoldReward;
        _countExpEarned += enemy.ExperienceReward;
        _countKillEnemy++;
        _player.PlayerStats.EnemyDied(enemy);
        KillCountUpdated?.Invoke(_countKillEnemy);
    }

    private void OnPlayerDied(int playerCoins, int playerLevel, int playerExpirience, int playerScore)
    {
        _playerCoins = playerCoins;
        _playerExpirience = playerExpirience;
        _playerLevel = playerLevel;
        _playerScore = playerScore;
        GiveWinEnemy();
    }

    private void GiveWinPlayer()
    {
        Time.timeScale = _pauseValue;
        GetPlayerResources();
        CloseAllGamePanels();
        _countMoneyEarned += _levelCompleteBonus;
        _playerCoins += _levelCompleteBonus;
        _loadConfig.LevelDataState.IsComplete = true;
        LevelCompleted?.Invoke(true);
        GameEnded?.Invoke();
    }

    private void GiveWinEnemy()
    {
        Time.timeScale = _pauseValue;
        CloseAllGamePanels();
        LevelCompleted?.Invoke(false);
        GameEnded?.Invoke();
    }

    private void GetPlayerResources()
    {
        _playerCoins = _player.Wallet.Coins;
        _playerExpirience = _player.PlayerStats.Experience;
        _playerLevel = _player.PlayerStats.Level;
        _playerScore = _player.PlayerStats.Score;
    }

    private void PauseGame()
    {
        _loadConfig.SetPauseGameState(true);
        _pauseHandler.PauseGame();
        GamePaused?.Invoke();
    }

    private void ResumeGame()
    {
        _loadConfig.SetPauseGameState(false);
        _pauseHandler.ResumeGame();
        _player.PlayerSounds.SetSoundValue(_loadConfig.AmbientVolume);
        GameResumed?.Invoke();
    }

    private void MuteSound()
    {
        _isMuteSound = _loadConfig.IsSoundOn != true;
        SoundMuted?.Invoke(_isMuteSound);
        _loadConfig.SetSoundState(_isMuteSound);
    }

    private void ExitGame()
    {
        StartCoroutine(CloseGame());
    }

    private IEnumerator CloseGame()
    {
        yield return _saveProgress.SaveApplicationParameters(_loadConfig);
        GameClosed?.Invoke();
        LoadLevel();
    }

    private void OnCloseRewardPanel()
    {
        var level = _loadConfig.LevelDataState.IsComplete ? _playerLevel : _loadConfig.PlayerLevel;
        _saveProgress.Save(_playerCoins, level, _playerExpirience, _playerScore, _loadConfig);
        GameClosed?.Invoke();
        LoadLevel();
    }

    private void LoadLevel()
    {
        StartCoroutine(LoadScreenLevel(SceneManager.LoadSceneAsync(_menuScene)));
    }

    private IEnumerator LoadScreenLevel(AsyncOperation asyncOperation)
    {
        if (_load != null)
            yield break;

        _canvasLoader.gameObject.SetActive(true);
        _load = asyncOperation;
        _load.allowSceneActivation = false;

        while (_load.progress < _maxLoadProgressValue)
        {
            yield return null;
        }

        _load.allowSceneActivation = true;
        _pauseHandler.ResumeGame();
        _load = null;
    }
}
