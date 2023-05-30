using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("[PauseMenuView]")]
    [SerializeField] private PauseMenuView _pauseMenuView;
    [Header("[LevelParameters]")]
    [SerializeField] private LevelParameters _levelParameters;

    private readonly float _resumeTimeValue = 1f;
    private readonly float _pauseTimeValue = 0f;
    private readonly string _menuScene = "Menu";
    private Player _player;

    public PauseMenuView PauseMenuView => _pauseMenuView;

    private void Start()
    {
        ResumeGame();
    }

    public void ResumeGame()
    {
        _pauseMenuView.ResumeGame();
        SetTimeScale(_resumeTimeValue);
        _levelParameters.LevelSpawn.EnabledDisabledEnemy(true);
        _levelParameters.EnabledDisabledPlayer(true);
    }

    public void PauseGame()
    {
        _pauseMenuView.PauseGame();
        SetTimeScale(_pauseTimeValue);
        _levelParameters.LevelSpawn.EnabledDisabledEnemy(false);
        _levelParameters.EnabledDisabledPlayer(false);
    }

    public void RestartGame()
    {
        _levelParameters.RestartGame();
        ResumeGame();
    }

    public void ExiteGame()
    {
        _player = FindObjectOfType<Player>();

        if (_levelParameters.LoadConfig.Levels.IsComplete)
        {
            _levelParameters.LoadConfig.SetPlayerAchievements(_player.PlayerAchievements.GetListAchievements());
            _levelParameters.LoadConfig.SetPlayerParameters(_player.Coins, _player.PlayerLevel, _player.PlayerExperience);
            SceneManager.LoadScene(_menuScene);
        }
        else
        {
            SceneManager.LoadScene(_menuScene);
        }
    }

    private void SetTimeScale(float value) 
    {
        Time.timeScale = value;
    }
}