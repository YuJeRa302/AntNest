using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private readonly float _resumeTimeValue = 1f;//
    private readonly float _pauseTimeValue = 0f;//

    [Header("[PauseMenuView]")]
    [SerializeField] private PauseMenuView _pauseMenuView;
    [Header("[PauseMenuSound]")]
    [SerializeField] private PauseMenuSound _pauseMenuSound;
    [Header("[RewardView]")]
    [SerializeField] private RewardView _rewardView;
    [Header("[LevelParameters]")]
    [SerializeField] private LevelParameters _levelParameters;
    [Header("[Rewards]")]
    [SerializeField] private Rewards _rewards;
    [Header("[PlayerInterfaceView]")]
    [SerializeField] private PlayerInterfaceView _playerInterfaceView;

    public PauseMenuView PauseMenuView => _pauseMenuView;
    public RewardView RewardView => _rewardView;

    //public void ResumeGame()
    //{
    //    _pauseMenuView.Close();//
    //    _levelParameters.LevelSpawn.ResumeSpawn();//
    //    SetTimeScale(_resumeTimeValue);//
    //    SetActiveStateEntity(true);
    //    ResumeCooldown();
    //}

    //public void PauseGame()
    //{
    //    _pauseMenuView.Open();
    //    _levelParameters.LevelSpawn.StopSpawn();
    //    SetTimeScale(_pauseTimeValue);
    //    SetActiveStateEntity(false);
    //}

    //public void CloseRewardPanel()
    //{
    //    _rewardView.CloseRewardPanel();
    //    _levelParameters.LevelObserver.UpdatePlayerData();
    //    _rewards.OpenAd();
    //}

    //public void ExiteGame()
    //{
    //    _rewards.OpenAd();
    //}

    //public void SetSoundValue()
    //{
    //    var state = _pauseMenuSound.AudioSourceAmbient.mute != true;
    //    SetStateMuteButton(state);
    //}

    //public void MuteAbientSound()
    //{
    //    SetStateMuteButton(true);
    //}

    //private void Start()
    //{
    //    ResumeGame();
    //}

    //private void SetStateMuteButton(bool state)
    //{
    //    _pauseMenuSound.SetStateMute(state);
    //    _pauseMenuView.SetMuteButtonImage(state);
    //}

    //private void SetActiveStateEntity(bool state)
    //{
    //    _playerInterfaceView.gameObject.SetActive(state);
    //    _levelParameters.LevelSpawn.ChangeEnemiesState(state);
    //    _levelParameters.ChangePlayerState(state);
    //}

    //private void SetTimeScale(float value)
    //{
    //    Time.timeScale = value;
    //}

    //private void ResumeCooldown()
    //{
    //    _levelParameters.Player.PlayerStats.PlayerAbility.Ability[0].ResumeCooldown(_levelParameters.Player.PlayerStats.PlayerAbility.Ability[0].AnimationTime);
    //    _levelParameters.WavePanelView.PlayAnimation();
    //}
}