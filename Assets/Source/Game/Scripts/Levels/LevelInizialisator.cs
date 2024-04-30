using UnityEngine;

public class LevelInizialisator : MonoBehaviour
{
    [Header("[Level Entities]")]
    [SerializeField] private LevelObserver _levelObserver;
    [SerializeField] private SoundController _soundController;
    [SerializeField] private LevelView _levelView;

    public void Initialize(LoadConfig loadConfig)
    {
        _soundController.Initialize(loadConfig);
        _levelObserver.Initialize(loadConfig);
        _levelView.Initialize(
            loadConfig.LevelDataState.LevelData.NameScene,
            loadConfig.LevelDataState.LevelData.NameEnemy,
            loadConfig.LevelDataState.LevelData.LevelIcon,
            loadConfig.LevelDataState.LevelData.WaveData[0].EnemyData.EnemyIcon
            );
    }
}
