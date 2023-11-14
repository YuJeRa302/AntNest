using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("[PauseMenuView]")]
    [SerializeField] private PauseMenuView _pauseMenuView;
    [Header("[PauseMenuSound]")]
    [SerializeField] private PauseMenuSound _pauseMenuSound;
    [Header("[RewardView]")]
    [SerializeField] private RewardView _rewardView;
    [Header("[LevelParameters]")]
    [SerializeField] private LevelParameters _levelParameters;
    [Header("[CanvasLoader]")]
    [SerializeField] private CanvasLoader _canvasLoader;
    [Header("[SaveProgress]")]
    [SerializeField] private SaveProgress _saveProgress;
    [Header("[Rewards]")]
    [SerializeField] private Rewards _rewards;
    [Header("[PlayerInterfaceView]")]
    [SerializeField] private PlayerInterfaceView _playerInterfaceView;
    [Header("[Name Leaderboard]")]
    [SerializeField] private string _leaderboard;

    private readonly float _resumeTimeValue = 1f;
    private readonly float _pauseTimeValue = 0f;
    private readonly string _menuScene = "Menu";

    private AsyncOperation _load;

    public PauseMenuView PauseMenuView => _pauseMenuView;
    public RewardView RewardView => _rewardView;

    private void Start()
    {
        ResumeGame();
    }

    public void ResumeGame()
    {
        _playerInterfaceView.gameObject.SetActive(true);
        _pauseMenuView.ResumeGame();
        SetTimeScale(_resumeTimeValue);
        SetActiveStateEntity(true);
    }

    public void PauseGame()
    {
        _playerInterfaceView.gameObject.SetActive(false);
        _pauseMenuView.PauseGame();
        SetTimeScale(_pauseTimeValue);
        SetActiveStateEntity(false);
    }

    public void CloseRewardPanel()
    {
        UpdatePlayerStats();
        StartCoroutine(LoadScreenLevel(SceneManager.LoadSceneAsync(_menuScene)));
    }

    public void ExiteGame()
    {
        StartCoroutine(LoadScreenLevel(SceneManager.LoadSceneAsync(_menuScene)));
    }

    public void SetSoundValue()
    {
        var state = _pauseMenuSound.AudioSourceAmbient.mute != true;
        SetStateMuteButton(state);
    }

    public void MuteAbientSound()
    {
        SetStateMuteButton(true);
    }

    private void SetStateMuteButton(bool state)
    {
        _pauseMenuSound.SetStateMute(state);
        _pauseMenuView.SetMuteButtonImage(state);
    }

    private void SetActiveStateEntity(bool state)
    {
        _levelParameters.LevelSpawn.EnabledDisabledEnemy(state);
        _levelParameters.EnabledDisabledPlayer(state);
    }

    private void SetTimeScale(float value)
    {
        Time.timeScale = value;
    }

    private IEnumerator LoadScreenLevel(AsyncOperation asyncOperation)
    {
        if (_load != null) yield break;

        _load = asyncOperation;
        _load.allowSceneActivation = false;
        _canvasLoader.gameObject.SetActive(true);
        _rewards.OpenFullScreenAd();

        while (_load.progress < 0.9f)
        {
            yield return null;
        }

        _load.allowSceneActivation = true;
        _load = null;
    }

    private void UpdatePlayerStats()
    {
        var level = _levelParameters.LoadConfig.Levels.IsComplete ? _levelParameters.Player.PlayerLevel : 0;
        _saveProgress.Save(_levelParameters.LoadConfig.Language,
            _levelParameters.Player.Wallet.GiveCoin(), level,
            _levelParameters.Player.PlayerStats.Experience,
            _levelParameters.Player.PlayerStats.Score,
            _levelParameters.LoadConfig.IsFirstSession,
            _levelParameters.Levels.LevelId,
            _levelParameters.Levels.IsComplete);
        _rewardView.CloseRewardPanel();
    }
}