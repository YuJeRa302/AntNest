using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    [Header("[Level]")]
    [SerializeField] private TMP_Text _levelName;
    [SerializeField] private Image _imageLevel;
    [Header("[Enemy]")]
    [SerializeField] private TMP_Text _enemiesName;
    [SerializeField] private TMP_Text _enemiesCount;
    [SerializeField] private Image _imageEnemy;
    [Header("[AchievementsPanel]")]
    [SerializeField] private AchievementsPanel _achievementsPanel;
    public AchievementsPanel AchievementsPanel => _achievementsPanel;

    public void LoadLevelUi(string levelName, string enemiesName, Sprite levelSprite, Sprite enemiesSprite, int enemiesCount)
    {
        _levelName.text = levelName.ToString();
        _enemiesName.text = enemiesName;
        _enemiesCount.text = enemiesCount.ToString();
        _imageLevel.sprite = levelSprite;
        _imageEnemy.sprite = enemiesSprite;
    }
}
