using UnityEngine;
using UnityEngine.UI;
using Lean.Localization;

public class LevelUI : MonoBehaviour
{
    [Header("[Level]")]
    [SerializeField] private LeanLocalizedText _levelName;
    [SerializeField] private Image _imageLevel;
    [Header("[Enemy]")]
    [SerializeField] private LeanLocalizedText _enemiesName;
    [SerializeField] private Text _enemiesCount;
    [SerializeField] private Image _imageEnemy;

    public void LoadLevelUi(string levelName, string enemiesName, Sprite levelSprite, Sprite enemiesSprite, int enemiesCount)
    {
        _levelName.TranslationName = levelName;
        _enemiesName.TranslationName = enemiesName;
        _enemiesCount.text = enemiesCount.ToString();
        _imageLevel.sprite = levelSprite;
        _imageEnemy.sprite = enemiesSprite;
    }

    public void UpdateEnemyKillCount(int value)
    {
        _enemiesCount.text = value.ToString();
    }
}
