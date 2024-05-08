using UnityEngine;

public class LevelInizialisator : MonoBehaviour
{
    [Header("[Level Entities]")]
    [SerializeField] private LevelObserver _levelObserver;
    [SerializeField] private SoundController _soundController;
    [SerializeField] private LevelView _levelView;

    public void Initialize(LoadConfig loadConfig)
    {
        var nameEnemy = loadConfig.LevelDataState.IsStandart ? loadConfig.LevelDataState.LevelData.NameEnemy : loadConfig.LevelDataState.LevelData.EndlessText;
        var enemyIcon = loadConfig.LevelDataState.IsStandart ? loadConfig.LevelDataState.LevelData.WaveData[0].EnemyData.EnemyIcon
            : loadConfig.LevelDataState.LevelData.EndlessSprite;
        _soundController.Initialize(loadConfig);
        _levelObserver.Initialize(loadConfig);
        _levelView.Initialize(
            loadConfig.LevelDataState.LevelData.NameScene,
            nameEnemy,
            loadConfig.LevelDataState.LevelData.LevelIcon,
            enemyIcon,
            loadConfig.PlayerCoins
            );
    }
}
