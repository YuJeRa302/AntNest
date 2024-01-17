using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;
using UnityEngine.UI;

public class LevelObserver : MonoBehaviour
{
    private readonly int _levelCompleteBonus = 150;
    private readonly string _menuScene = "Menu";
    private readonly float _resumeTimeValue = 1f;
    private readonly float _pauseTimeValue = 0f;

    [Header("[Level Entities]")]
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private SaveProgress _saveProgress;
    [SerializeField] private Player _player;
    [Header("[Panels]")]
    [SerializeField] private GamePanels[] _panels;
    [Header("[Buttons]")]
    [SerializeField] private Button _soundButton;
    [SerializeField] private Button _pauseGameButton;
    [SerializeField] private Button _resumeGameButton;
    [SerializeField] private Button _closeGameButton;

    private bool _isPlayerAlive = true;
    private bool _isMuteSound = false;
    private int _countKillEnemy = 0;
    private int _countMoneyEarned;
    private int _countExp;
    private AsyncOperation _load;
    private LoadConfig _loadConfig;

    public bool IsPlayerAlive => _isPlayerAlive;
    public int CountMoneyEarned => _countMoneyEarned;
    public int CountExp => _countExp;
    public int CountKillEnemy => _countKillEnemy;
    public EnemySpawner EnemySpawner => _enemySpawner;

    public Action GamePaused;
    public Action GameResumed;
    public Action GameEnded;
    public Action GameClosed;
    public Action<bool> SoundMuted;
    public Action<int> KillCountUpdated;
    public Action<bool> LevelCompleted;

    private void OnEnable()
    {
        AddPanelListener();
        _enemySpawner.EnemyDied += OnEnemyDied;
        _enemySpawner.LastEnemyDied += GiveWinPlayer;
        _player.PlayerStats.PlayerHealth.PlayerDie += OnPlayerDied;
        _soundButton.onClick.AddListener(MuteSound);
        _pauseGameButton.onClick.AddListener(PauseGame);
        _resumeGameButton.onClick.AddListener(ResumeGame);
        _closeGameButton.onClick.AddListener(CloseGame);
    }

    private void OnDisable()
    {
        RemovePanelListener();
        _enemySpawner.EnemyDied -= OnEnemyDied;
        _enemySpawner.LastEnemyDied -= GiveWinPlayer;
        _player.PlayerStats.PlayerHealth.PlayerDie -= OnPlayerDied;
        _soundButton.onClick.RemoveListener(MuteSound);
        _pauseGameButton.onClick.RemoveListener(PauseGame);
        _resumeGameButton.onClick.RemoveListener(ResumeGame);
        _closeGameButton.onClick.RemoveListener(CloseGame);
    }

    public void Initialize(LoadConfig loadConfig)
    {
        _loadConfig = loadConfig;
        _isPlayerAlive = true;
        _enemySpawner.Initialize(loadConfig);
        LoadPanels();
    }

    private void LoadPanels()
    {
        foreach (var panel in _panels)
        {
            panel.Initialize(_player, this);
        }
    }

    private void AddPanelListener()
    {
        foreach (var panel in _panels)
        {
            panel.PanelOpened += OnOpenShopPanel;
            panel.PanelOpened += OnOpenMenuPanel;
            panel.PanelClosed += OnCloseShopPanel;
            panel.PanelClosed += OnCloseMenuPanel;
            panel.OpenAd += OnOpenAd;
            panel.CloseAd += OnCloseAd;
        }
    }

    private void RemovePanelListener()
    {
        foreach (var panel in _panels)
        {
            panel.PanelOpened -= OnOpenShopPanel;
            panel.PanelOpened -= OnOpenMenuPanel;
            panel.PanelClosed -= OnCloseShopPanel;
            panel.PanelClosed -= OnCloseMenuPanel;
            panel.OpenAd -= OnOpenAd;
            panel.CloseAd -= OnCloseAd;
        }
    }

    private void OnOpenShopPanel()
    {
        Debug.Log("OnOpenShopPanel");
    }

    private void OnOpenMenuPanel()
    {
        Debug.Log("OnOpenMenuPanel");
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

    private void OnCloseShopPanel()
    {

    }

    private void OnCloseMenuPanel()
    {

    }

    private void OnEnemyDied(Enemy enemy)
    {
        UpdatePlayerStats(enemy);
        _countKillEnemy++;
        KillCountUpdated?.Invoke(_countKillEnemy);
    }

    private void OnPlayerDied()
    {
        _isPlayerAlive = false;
        GiveWinEnemy();
    }

    private void GiveWinPlayer()
    {
        GameEnded?.Invoke();
        _loadConfig.Levels.SetComplete();
        _countMoneyEarned += _levelCompleteBonus;
        LevelCompleted?.Invoke(true);
    }

    private void GiveWinEnemy()
    {
        GameEnded?.Invoke();
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

    public void UpdatePlayerData()
    {
        var level = _loadConfig.Levels.IsComplete ? _player.PlayerStats.Level : 0;
        _saveProgress.Save(_loadConfig.Language,
            _player.Wallet.GetCoins(),
            level,
            _player.PlayerStats.Experience,
            _player.PlayerStats.Score,
           _loadConfig.IsFirstSession,
            _loadConfig.Levels.LevelId,
            _loadConfig.Levels.IsComplete);
    }

    private void SetTimeScale(float value)
    {
        Time.timeScale = value;
    }

    private void LoadLevel()
    {
        StartCoroutine(LoadScreenLevel(SceneManager.LoadSceneAsync(_menuScene)));
    }

    private void UpdatePlayerStats(Enemy enemy)
    {
        _countMoneyEarned += enemy.GoldReward;
        _countExp += enemy.ExperienceReward;
        _player.PlayerStats.OnEnemyDie(enemy);
    }

    private IEnumerator LoadScreenLevel(AsyncOperation asyncOperation)
    {
        if (_load != null) yield break;

        _load = asyncOperation;
        _load.allowSceneActivation = false;
        //_levelParameters.CanvasLoader.gameObject.SetActive(true);

        while (_load.progress < 0.9f)
        {
            yield return null;
        }

        _load.allowSceneActivation = true;
        _load = null;
    }
}
