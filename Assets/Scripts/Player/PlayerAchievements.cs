using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAchievements : MonoBehaviour
{
    [Header("[Transform]")]
    [SerializeField] private Transform _achievementsTransform;
    [Header("[Achievements]")]
    [SerializeField] private List<Achievements> _achievements;

    private readonly UnityEvent<Achievements, int> _achievementsUpdate = new();

    public event UnityAction<Achievements, int> ChangedAchievements
    {
        add => _achievementsUpdate.AddListener(value);
        remove => _achievementsUpdate.RemoveListener(value);
    }

    public void UpdateAchievements(Enemy enemy, int enemyValue)
    {
        foreach (var achievement in _achievements)
        {
            if (achievement.Id == enemy.Id)
            {
                achievement.UpdateCountEnemy(enemyValue);
                _achievementsUpdate.Invoke(achievement, achievement.CurrentCount);
            }
        }
    }

    public List<Achievements> GetListAchievements()
    {
        return _achievements;
    }

    private void Start()
    {
        AddAchievementsToList();
    }

    private void AddAchievementsToList()
    {
        for (int index = 0; index < _achievementsTransform.childCount; index++)
        {
            _achievements.Add(_achievementsTransform.GetChild(index).GetComponent<EnemyAchiev>());
        }
    }
}