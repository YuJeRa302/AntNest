using UnityEngine;

public class LevelParameters : MonoBehaviour
{
    [Header("[Level Spawn]")]
    [SerializeField] private LevelSpawn _levelSpawn;
    [Header("[Config]")]
    [SerializeField] private LoadConfig _loadConfig;
    [Header("[LevelUI]")]
    [SerializeField] private LevelUI _levelUI;
    [Header("[PauseMenu]")]
    [SerializeField] private PauseMenu _pauseMenu;

    private readonly string _winPlayer = "Победа";
    private readonly string _winEnemy = "Поражение";
    private readonly int enemyValue = 1;

    private Player _player;
    private Levels _levels;
    private int _countKillEnemy = 0;
    private int _countMoneyEarned;
    private int _countExp;
    private bool _isPlayerAlive = true;

    public int CountEnemyDie => _countKillEnemy;
    public Enemy Enemy => _levels.EnemyPrefab;
    public LoadConfig LoadConfig => _loadConfig;
    public LevelSpawn LevelSpawn => _levelSpawn;

    public void OnEnemyDie(Enemy enemy)
    {
        _countKillEnemy++;
        _countMoneyEarned += enemy.GoldReward;
        _countExp += enemy.ExperienceReward;
        _player.PlayerStats.OnEnemyDie(enemy);
        enemy.Dying -= OnEnemyDie;
        UpdateAchievements(_levels.EnemyPrefab, enemyValue);

        if (_countKillEnemy == _levels.CountEnemy && _isPlayerAlive == true)
        {
            WinPlayer(_winPlayer);
        }
    }

    public void RestartGame()
    {
        _levelSpawn.SearchForEnemiesToDestroy();
        _player.Recover();
        LoadLevelParameters();
        LoadScene();
    }
    public void EnabledDisabledPlayer(bool state)
    {
        _player.enabled = state;
    }

    private void Start()
    {
        LoadLevelParameters();
        LoadScene();
    }

    private void OnPlayerDie()
    {
        _isPlayerAlive = false;
        WinEnemy(_winEnemy);
    }

    private void WinEnemy(string winEnemy)
    {
        _pauseMenu.PauseMenuView.ChagenButtons();
        CompleteLevel(winEnemy);
    }

    private void WinPlayer(string winPlayer)
    {
        _levels.SetComplete();
        CompleteLevel(winPlayer);
    }

    private void UpdateAchievements(Enemy enemy, int countEnemy)
    {
        _player.PlayerAchievements.UpdateAchievements(enemy, countEnemy);
    }

    private void LoadLevelParameters()
    {
        _player = FindObjectOfType<Player>();
        LoadPlayerStats();
        _levels = _loadConfig.Levels;
    }

    private void LoadScene()
    {
        _levelUI.LoadLevelUi(_levels.NameLocation, _levels.EnemyPrefab.Name, _levels.Sprite, _levels.EnemyPrefab.Sprite, _countKillEnemy);
        _levelSpawn.StartSpawn(_levels);
    }

    private void CompleteLevel(string state)
    {
        _pauseMenu.PauseGame();
        _pauseMenu.PauseMenuView.ShowRewardPanel(state, _countMoneyEarned, _countExp, _countKillEnemy);
        _player.PlayerDie -= OnPlayerDie;
    }

    private void LoadPlayerStats()
    {
        _isPlayerAlive = true;
        _player.PlayerDie += OnPlayerDie;
        _levelUI.AchievementsPanel.GetAchievements(_loadConfig.GetListAchievements());
        _player.Wallet.SetDefaltCoins(_loadConfig.PlayerCoins);
        _player.PlayerStats.SetDefaultLevel(_loadConfig.PlayerLevel, _loadConfig.PlayerExperience);
    }
}