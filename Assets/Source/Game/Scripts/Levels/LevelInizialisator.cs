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
            loadConfig.Levels.NameScene,
            loadConfig.Levels.NameEnemy,
            loadConfig.Levels.Sprite,
            loadConfig.Levels.Wave[0].EnemyPrefab.Sprite,
            loadConfig.Levels.Wave[0].CountEnemy
            );
    }
}
