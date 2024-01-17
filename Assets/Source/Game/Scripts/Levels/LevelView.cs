using UnityEngine;
using UnityEngine.UI;
using Lean.Localization;

public class LevelView : MonoBehaviour
{
    [Header("[Level Entities]")]
    [SerializeField] private LevelObserver _levelObserver;
    [Header("[UI Entities]")]
    [SerializeField] private LeanLocalizedText _levelName;
    [SerializeField] private Image _imageLevel;
    [SerializeField] private LeanLocalizedText _enemiesName;
    [SerializeField] private Text _enemiesCount;
    [SerializeField] private Image _imageEnemy;

    private void OnEnable()
    {
        _levelObserver.KillCountUpdated += UpdateEnemyKillCount;
    }

    private void OnDisable()
    {
        _levelObserver.KillCountUpdated -= UpdateEnemyKillCount;
    }

    public void Initialize(string levelName, string enemiesName, Sprite levelSprite, Sprite enemiesSprite, int enemiesCount)
    {
        _levelName.TranslationName = levelName;
        _enemiesName.TranslationName = enemiesName;
        _enemiesCount.text = enemiesCount.ToString();
        _imageLevel.sprite = levelSprite;
        _imageEnemy.sprite = enemiesSprite;
    }

    private void UpdateEnemyKillCount(int value)
    {
        _enemiesCount.text = value.ToString();
    }
}
