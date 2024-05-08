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
    [SerializeField] private Text _coinCount;
    [Header("[Canvas Loader]")]
    [SerializeField] private CanvasLoader _canvasLoader;
    [Header("[Wave Entities]")]
    [SerializeField] private Text _waveNumber;
    [SerializeField] private LeanLocalizedText _waveName;
    [SerializeField] private Image _extraWaveIcon;

    private readonly int _defaultEnemiesCount = 0;
    private readonly string _waveLeanLocalizedName = "Wave";

    private void Awake()
    {
        _levelObserver.KillCountUpdated += OnUpdateEnemyKillCount;
        _levelObserver.GameClosed += OnOpenCanvasLoader;
        _levelObserver.Player.Wallet.CoinCountChanged += OnUpdateCoinCount;
    }

    private void OnDestroy()
    {
        _levelObserver.KillCountUpdated -= OnUpdateEnemyKillCount;
        _levelObserver.GameClosed -= OnOpenCanvasLoader;
        _levelObserver.Player.Wallet.CoinCountChanged -= OnUpdateCoinCount;
    }

    public void Initialize(string levelName, string enemiesName, Sprite levelSprite, Sprite enemiesSprite, int coins)
    {
        _levelName.TranslationName = levelName;
        _enemiesName.TranslationName = enemiesName;
        _waveName.TranslationName = _waveLeanLocalizedName;
        _enemiesCount.text = _defaultEnemiesCount.ToString();
        _imageLevel.sprite = levelSprite;
        _imageEnemy.sprite = enemiesSprite;
        _coinCount.text = coins.ToString();
    }

    public void ChangeWaveNumber(int waveNumber)
    {
        ++waveNumber;
        _waveNumber.text = waveNumber.ToString();
    }

    public void ShowExtraWaveIcon(bool state)
    {
        _extraWaveIcon.gameObject.SetActive(state);
    }

    private void OnUpdateCoinCount(int value)
    {
        _coinCount.text = value.ToString();
    }

    private void OnUpdateEnemyKillCount(int value)
    {
        _enemiesCount.text = value.ToString();
    }

    private void OnOpenCanvasLoader()
    {
        _canvasLoader.gameObject.SetActive(true);
    }
}