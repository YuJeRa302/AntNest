using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AchievementsView : MonoBehaviour
{
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _countEnemy;
    [SerializeField] private Image _iconEnemy;
    [SerializeField] private Slider _slider;

    private int _maxCount;

    public void Render(Achievements achievements)
    {
        _name.text = achievements.Name;
        _countEnemy.text = achievements.CurrentCount.ToString() + "/" + achievements.MaxCount.ToString();
        _iconEnemy.sprite = achievements.EnemyIcon;
        _slider.maxValue = achievements.MaxCount;
        _slider.value = achievements.CurrentCount;
        _maxCount = achievements.MaxCount;
    }

    public void UpdateCount(int countEnemy)
    {
        _countEnemy.text = countEnemy.ToString() + "/" + _maxCount.ToString();
        _slider.value = countEnemy;
    }
}