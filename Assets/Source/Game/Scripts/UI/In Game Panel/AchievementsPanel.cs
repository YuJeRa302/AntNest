using System.Collections.Generic;
using UnityEngine;

public class AchievementsPanel : ShopPanel
{
    [Header("[Views]")]
    [SerializeField] private AchievementsView _achievementsView;
    [Header("[Containers]")]
    [SerializeField] private GameObject _achievementsContainer;
    [Header("[Achievements]")]
    [SerializeField] private AchievementsView[] _achievementInPanel;

    private List<Achievements> _achievements;
    private Player _player;

    private void Start()
    {
        _player = FindObjectOfType<Player>();
        _achievements = _player.PlayerAchievements.GetListAchievements();
        _player.PlayerAchievements.ChangedAchievements += OnUpdateAchievements;
    }

    public void GetAchievements(List<Achievements> achievements)
    {
        if (achievements != null && achievements.Count > 0)
        {
            for (int index = 0; index < _achievements.Count; index++)
            {
                _achievements[index].UpdateCountEnemy(achievements[index].CurrentCount);
            }
        }

        Filling(_achievements);
    }

    public void OnUpdateAchievements(Achievements achievements, int countEnemy)
    {
        _achievementInPanel[achievements.Id - 1].UpdateCount(countEnemy);
    }

    private void Filling(List<Achievements> achievements)
    {
        for (int i = 0; i < achievements.Count; i++)
        {
            Add(achievements[i]);
        }

        var childrenAchievemetns = _achievementsContainer.GetComponentInChildren<Transform>();
        _achievementInPanel = new AchievementsView[childrenAchievemetns.childCount];

        for (int i = 0; i < childrenAchievemetns.childCount; i++)
        {
            _achievementInPanel[i] = childrenAchievemetns.GetChild(i).GetComponent<AchievementsView>();
        }
    }
    private void Add(Achievements achievements)
    {
        var view = Instantiate(_achievementsView, _achievementsContainer.transform);
        view.Render(achievements);
    }

    private void OnDisable()
    {
        _player.PlayerAchievements.ChangedAchievements += OnUpdateAchievements;
    }
}