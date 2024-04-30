using UnityEngine;
using UnityEngine.UI;
using Lean.Localization;

public class LevelView : MonoBehaviour
{
    [Header("[Level Entities]")]
    [SerializeField] private LevelObserver _levelObserver;
    [Header("[UI Entities]")]
    [SerializeField] private LeanLocalizedText _levelName;
    [SerializeField] private LeanLocalizedText _enemiesName;
    [SerializeField] private Image _imageLevel;
    [SerializeField] private Image _imageEnemy;
    [SerializeField] private Text _enemiesCount;
    [Header("[Canvas Loader]")]
    [SerializeField] private CanvasLoader _canvasLoader;
    [Header("[Wave Entities]")]
    [SerializeField] private Text _waveNumber;
    [SerializeField] private LeanLocalizedText _waveName;

    private readonly int _defaultEnemiesCount = 0;
    private readonly string _waveLeanLocalizedName = "Wave";

    private void Awake()
    {
        _levelObserver.KillCountUpdated += UpdateEnemyKillCount;
        _levelObserver.GameClosed += OnOpenCanvasLoader;
    }

    private void OnDestroy()
    {
        _levelObserver.KillCountUpdated -= UpdateEnemyKillCount;
        _levelObserver.GameClosed -= OnOpenCanvasLoader;
    }

    public void Initialize(string levelName, string enemiesName, Sprite levelSprite, Sprite enemiesSprite)
    {
        _levelName.TranslationName = levelName;
        _enemiesName.TranslationName = enemiesName;
        _waveName.TranslationName = _waveLeanLocalizedName;
        _enemiesCount.text = _defaultEnemiesCount.ToString();
        _imageLevel.sprite = levelSprite;
        _imageEnemy.sprite = enemiesSprite;
    }

    public void ChangeWaveNumber(int waveNumber)
    {
        ++waveNumber;
        _waveNumber.text = waveNumber.ToString();
    }

    private void UpdateEnemyKillCount(int value)
    {
        _enemiesCount.text = value.ToString();
    }

    private void OnOpenCanvasLoader()
    {
        _canvasLoader.gameObject.SetActive(true);
    }
}