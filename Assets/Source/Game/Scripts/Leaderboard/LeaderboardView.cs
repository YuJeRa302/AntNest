using System.Collections.Generic;
using UnityEngine;

public class LeaderboardView : MenuTab
{
    [Header("[Container]")]
    [SerializeField] private GameObject _leaderboardContainer;
    [Header("[ElementPrefab]")]
    [SerializeField] private LeaderboardElement _leaderboardElementPrefab;
    [Header("[Menu]")]
    [SerializeField] private MenuPanel _menuPanel;

    private List<LeaderboardElement> _leaderboardElements = new();

    public void FillingLeaderboard(List<LeaderboardPlayer> leaderboardPlayers)
    {
        ClearLeaderboard();

        foreach (LeaderboardPlayer player in leaderboardPlayers)
        {
            LeaderboardElement leaderboardElementInstance = Instantiate(_leaderboardElementPrefab, _leaderboardContainer.transform);
            leaderboardElementInstance.Initialize(player.Name, player.Rank, player.Score);

            _leaderboardElements.Add(leaderboardElementInstance);
        }
    }

    public void Open()
    {
        _menuPanel.gameObject.SetActive(false);
        gameObject.SetActive(true);
    }

    private void ClearLeaderboard()
    {
        foreach (var element in _leaderboardElements)
        {
            Destroy(element);
        }

        ClearContainer();
        _leaderboardElements = new List<LeaderboardElement>();
    }

    private void ClearContainer()
    {
        for (int i = 0; i < _leaderboardContainer.transform.childCount; i++)
        {
            Destroy(_leaderboardContainer.transform.GetChild(i).gameObject);
        }
    }
}