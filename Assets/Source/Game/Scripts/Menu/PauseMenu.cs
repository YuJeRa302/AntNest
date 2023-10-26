using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class PauseMenu : MonoBehaviour
{
    [Header("[PauseMenuView]")]
    [SerializeField] private PauseMenuView _pauseMenuView;
    [Header("[RewardView]")]
    [SerializeField] private RewardView _rewardView;
    [Header("[LevelParameters]")]
    [SerializeField] private LevelParameters _levelParameters;
    [Header("[Sound]")]
    [SerializeField] private AudioSource _ambientSounds;
    [SerializeField] private AudioSource _buttonFX;
    [Header("[CanvasLoader]")]
    [SerializeField] private CanvasLoader _canvasLoader;
    [Header("[SaveProgress]")]
    [SerializeField] private SaveProgress _saveProgress;
    [Header("[MainUI]")]
    [SerializeField] private MainUiView _mainUiView;
    [Header("[Rewards]")]
    [SerializeField] private Rewards _rewards;
    [Header("[Name Leaderboard]")]
    [SerializeField] private string _leaderboard;

    private readonly float _resumeTimeValue = 1f;
    private readonly float _pauseTimeValue = 0f;
    private readonly string _menuScene = "Menu";

    public PauseMenuView PauseMenuView => _pauseMenuView;
    public RewardView RewardView => _rewardView;

    private void Start()
    {
        ResumeGame();
    }

    public void ResumeGame()
    {
        _mainUiView.gameObject.SetActive(true);
        _pauseMenuView.ResumeGame();
        SetTimeScale(_resumeTimeValue);
        SetActiveStateEntity(true);
    }

    public void PauseGame()
    {
        _mainUiView.gameObject.SetActive(false);
        _pauseMenuView.PauseGame();
        SetTimeScale(_pauseTimeValue);
        SetActiveStateEntity(false);
    }

    public void CloseRewardPanel()
    {
        UpdatePlayerStats();
        LoadScreenLevel(SceneManager.LoadSceneAsync(_menuScene));
    }

    public void ExiteGame()
    {
        LoadScreenLevel(SceneManager.LoadSceneAsync(_menuScene));
    }

    public void SetSoundValue()
    {
        var state = _ambientSounds.mute != true;
        SetStateMuteButton(state);
    }

    public void MuteAbientSound()
    {
        SetStateMuteButton(true);
    }

    private void SetStateMuteButton(bool state)
    {
        _ambientSounds.mute = state;
        _buttonFX.mute = state;
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

    private async void LoadScreenLevel(AsyncOperation asyncOperation)
    {
        _canvasLoader.gameObject.SetActive(true);
        asyncOperation.allowSceneActivation = false;
        _rewards.OpenFullScreenAd();

        if (_rewards.IsClosedFullScreenAd == true)
        {
            while (asyncOperation.progress < 0.9f)
            {
                await Task.Yield();
            }
        }

        await Task.Delay(2000);
        _canvasLoader.gameObject.SetActive(false);
        asyncOperation.allowSceneActivation = true;
    }

    private void UpdatePlayerStats() 
    {
        var level = _levelParameters.LoadConfig.Levels.IsComplete ? _levelParameters.Player.PlayerLevel : 0;
        _saveProgress.Save(_levelParameters.LoadConfig.Language, _levelParameters.Player.Wallet.GiveCoin(), level,
            _levelParameters.Player.PlayerStats.Experience, _levelParameters.Levels.LevelId, _levelParameters.Levels.IsComplete);
        YandexGame.NewLeaderboardScores(_leaderboard, _levelParameters.CountMoneyEarned);
        _rewardView.CloseRewardPanel();
    }
}