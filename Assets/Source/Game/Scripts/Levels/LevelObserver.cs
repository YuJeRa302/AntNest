using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;
using UnityEngine.UI;
using Lean.Localization;

public class LevelObserver : MonoBehaviour
{
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
    [Header("[Buttons]")]
    [SerializeField] private Button _soundButton;
    [SerializeField] private Button _closeGameButton;
    [Header("[LeanLocalization]")]
    [SerializeField] private LeanLocalization _leanLocalization;

    private readonly int _levelCompleteBonus = 150;
    private readonly string _menuScene = "Menu";
    private readonly float _resumeTimeValue = 1f;
    private readonly float _pauseTimeValue = 0f;
    private readonly int _minExpCount = 0;
    private readonly float _maxLoadProgressValue = 0.9f;

    private bool _isMuteSound = false;
    private int _defaultCoins;
    private int _defaultExp;
    private int _defaultLevel;
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

    public int PlayerCoins => _playerCoins;
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
        _player.PlayerStats.PlayerHealth.PlayerDie += OnPlayerDied;
        _soundButton.onClick.AddListener(MuteSound);
        _closeGameButton.onClick.AddListener(CloseGame);
    }

    private void OnDisable()
    {
        RemovePanelListener();
        _enemySpawner.EnemyDied -= OnEnemyDied;
        _enemySpawner.LastEnemyDied -= GiveWinPlayer;
        _player.PlayerStats.PlayerHealth.PlayerDie -= OnPlayerDied;
        _soundButton.onClick.RemoveListener(MuteSound);
        _closeGameButton.onClick.RemoveListener(CloseGame);
    }

    public void Initialize(LoadConfig loadConfig)
    {
        _loadConfig = loadConfig;
        _defaultCoins = _loadConfig.PlayerCoins;
        _defaultExp = _loadConfig.PlayerExperience;
        _enemySpawner.Initialize(loadConfig, _player);
        _runeSpawner.Initialize();
        _player.PlayerStats.Initialize(_loadConfig.PlayerLevel, _loadConfig.PlayerExperience, _loadConfig.PlayerScore);
        _player.Wallet.Initialize(_loadConfig.PlayerCoins);
        LoadGamePanels();
    }

    public void TakeReward(int value)
    {
        _playerCoins += value;
        _countMoneyEarned += value;
    }

    private void LoadGamePanels()
    {
        foreach (var panel in _panels)
        {
            panel.Initialize(_player, this);
        }
    }

    private void CloseAllGamePanels()
    {
        foreach (var panel in _panels)
        {
            panel.gameObject.SetActive(false);
        }
    }

    private void AddPanelListener()
    {
        foreach (var panel in _panels)
        {
            if (panel is RewardPanel)
                (panel as RewardPanel).RewardPanelClosed += OnCloseRewardPanel;

            if (panel is PausePanel)
                (panel as PausePanel).LanguageChanged += OnLanguageChanged;

            panel.PanelOpened += PauseGame;
            panel.PanelClosed += ResumeGame;
            panel.OpenAd += OnOpenAd;
            panel.CloseAd += OnCloseAd;
        }
    }

    private void RemovePanelListener()
    {
        foreach (var panel in _panels)
        {
            if (panel is RewardPanel)
                (panel as RewardPanel).RewardPanelClosed -= OnCloseRewardPanel;

            if (panel is PausePanel)
                (panel as PausePanel).LanguageChanged -= OnLanguageChanged;

            panel.PanelOpened -= PauseGame;
            panel.PanelClosed -= ResumeGame;
            panel.OpenAd -= OnOpenAd;
            panel.CloseAd -= OnCloseAd;
        }
    }

    private void OnLanguageChanged(string value)
    {
        _leanLocalization.SetCurrentLanguage(value);
        _loadConfig.SetCurrentLanguage(value);
    }

    private void OnOpenAd()
    {
        SetTimeScale(_pauseTimeValue);
        AudioListener.pause = true;
    }

    private void OnCloseAd()
    {
        GameClosed?.Invoke();
        SetTimeScale(_resumeTimeValue);
        AudioListener.pause = false;
        LoadLevel();
    }

    private void OnEnemyDied(Enemy enemy)
    {
        _player.PlayerStats.EnemyDied(enemy);
        _countKillEnemy++;
        KillCountUpdated?.Invoke(_countKillEnemy);
    }

    private void OnPlayerDied(int playerCoins, int playerLevel, int playerExpirience, int playerScore)
    {
        _playerCoins = playerCoins;
        _playerExpirience = playerExpirience;
        _playerLevel = playerLevel;
        _playerScore = playerScore;
        SetTimeScale(_pauseTimeValue);
        GiveWinEnemy();
    }

    private void GiveWinPlayer()
    {
        CloseAllGamePanels();
        _loadConfig.LevelDataState.IsComplete = true;
        _countMoneyEarned = (_playerCoins - _defaultCoins) + _levelCompleteBonus;
        CalculateExpEarned();
        LevelCompleted?.Invoke(true);
        GameEnded?.Invoke();
    }

    private void GiveWinEnemy()
    {
        CloseAllGamePanels();
        _countMoneyEarned = (_playerCoins - _defaultCoins);
        CalculateExpEarned();
        LevelCompleted?.Invoke(false);
        GameEnded?.Invoke();
    }

    private void CalculateExpEarned()
    {
        int expEarned = 0;

        if (_defaultLevel < _playerLevel)
            expEarned = _playerExpirience;

        if (_defaultLevel == _playerLevel)
            expEarned = _playerExpirience >= _defaultExp ? _playerExpirience - _defaultExp : _minExpCount;

        _countExpEarned = expEarned;
    }

    private void PauseGame()
    {
        SetTimeScale(_pauseTimeValue);
        GamePaused?.Invoke();
    }

    private void ResumeGame()
    {
        SetTimeScale(_resumeTimeValue);
        GameResumed?.Invoke();
    }

    private void MuteSound()
    {
        _isMuteSound = _isMuteSound != true;
        SoundMuted?.Invoke(_isMuteSound);
    }

    private void CloseGame()
    {
        GameClosed?.Invoke();
        LoadLevel();
    }

    private void OnCloseRewardPanel()
    {
        SavePlayerStats();
        CloseGame();
    }

    private void SavePlayerStats()
    {
        var level = _loadConfig.LevelDataState.IsComplete ? _playerLevel : _loadConfig.PlayerLevel;
        _saveProgress.Save(_playerCoins, level, _playerExpirience, _playerScore, _loadConfig);
    }

    private void SetTimeScale(float value)
    {
        Time.timeScale = value;
    }

    private void LoadLevel()
    {
        StartCoroutine(LoadScreenLevel(SceneManager.LoadSceneAsync(_menuScene)));
    }

    private IEnumerator LoadScreenLevel(AsyncOperation asyncOperation)
    {
        if (_load != null)
            yield break;

        _load = asyncOperation;
        _load.allowSceneActivation = false;
        _canvasLoader.gameObject.SetActive(true);

        while (_load.progress < _maxLoadProgressValue)
        {
            yield return null;
        }

        _load.allowSceneActivation = true;
        _load = null;
    }
}
