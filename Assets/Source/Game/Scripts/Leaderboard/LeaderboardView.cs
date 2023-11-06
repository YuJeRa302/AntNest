using System.Collections.Generic;
using UnityEngine;

public class LeaderboardView : MonoBehaviour
{
    [Header("[Container]")]
    [SerializeField] private Transform _container;
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
            LeaderboardElement leaderboardElementInstance = Instantiate(_leaderboardElementPrefab, _container);
            leaderboardElementInstance.Initialized(player.Name, player.Rank, player.Score);

            _leaderboardElements.Add(leaderboardElementInstance);
        }
    }

    public void Open()
    {
        _menuPanel.gameObject.SetActive(false);
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
        _menuPanel.gameObject.SetActive(true);
    }

    private void ClearLeaderboard()
    {
        foreach (var element in _leaderboardElements)
        {
            Destroy(element);
        }

        _leaderboardElements = new List<LeaderboardElement>();
    }
}