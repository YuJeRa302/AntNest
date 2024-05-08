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

    private bool _isMuteSound = false;
    private int _defaultCoins;
    private int _defaultExp;
    private int _countKillEnemy = 0;
    private int _countMoneyEarned = 0;
    private int _countExpEarned = 0;
    private AsyncOperation _load;
    private LoadConfig _loadConfig;

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

    private void OnPlayerDied()
    {
        GiveWinEnemy();
    }

    private void GiveWinPlayer()
    {
        CloseAllGamePanels();
        GameEnded?.Invoke();
        _loadConfig.LevelDataState.IsComplete = true;
        _countMoneyEarned = (_player.Wallet.Coins - _defaultCoins) + _levelCompleteBonus;
        _countExpEarned = _player.PlayerStats.Experience - _defaultExp;
        LevelCompleted?.Invoke(true);
    }

    private void GiveWinEnemy()
    {
        CloseAllGamePanels();
        GameEnded?.Invoke();
        _countMoneyEarned = (_player.Wallet.Coins - _defaultCoins);
        _countExpEarned = _player.PlayerStats.Experience - _defaultExp;
        LevelCompleted?.Invoke(false);
    }

    private void PauseGame()
    {
        GamePaused?.Invoke();
        SetTimeScale(_pauseTimeValue);
    }

    private void ResumeGame()
    {
        GameResumed?.Invoke();
        SetTimeScale(_resumeTimeValue);
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
        var level = _loadConfig.LevelDataState.IsComplete ? _player.PlayerStats.Level : _loadConfig.PlayerLevel;
        _saveProgress.Save(_loadConfig.Language,
            _player.Wallet.Coins,
            level,
            _player.PlayerStats.Experience,
            _player.PlayerStats.Score,
           _loadConfig.IsFirstSession,
            _loadConfig.LevelDataState.LevelData.LevelId,
            _loadConfig.LevelDataState.IsComplete);
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
        if (_load != null) yield break;

        _load = asyncOperation;
        _load.allowSceneActivation = false;

        while (_load.progress < 0.9f)
        {
            yield return null;
        }

        _load.allowSceneActivation = true;
        _load = null;
    }
}
