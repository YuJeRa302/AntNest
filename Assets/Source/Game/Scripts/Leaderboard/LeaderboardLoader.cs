using System.Collections.Generic;
using UnityEngine;
using Agava.YandexGames;

public class LeaderboardLoader : MonoBehaviour
{
    private const string AnonynousName = "Anonymous";
    private const string LeaderboardName = "Leaderboard";

    [Header("[LeaderboardView]")]
    [SerializeField] private LeaderboardView _leaderboardView;
    [Header("[Dialog Panel]")]
    [SerializeField] private DialogPanel _dialogPanel;
    [Header("[Config]")]
    [SerializeField] private LoadConfig _config;

    private readonly List<LeaderboardPlayer> _leaderboardPlayers = new();

    public void SetPlayer(int score)
    {
        if (PlayerAccount.IsAuthorized == false) return;

        Leaderboard.GetPlayerEntry(LeaderboardName, _ =>
        {
            Leaderboard.SetScore(LeaderboardName, score);
        });
    }

    public void Fill()
    {
        _leaderboardPlayers.Clear();

        if (PlayerAccount.IsAuthorized == false) return;

        Leaderboard.GetEntries(LeaderboardName, result =>
        {
            for (int i = 0; i < result.entries.Length; i++)
            {
                var rank = result.entries[i].rank;
                var score = result.entries[i].score;
                var name = result.entries[i].player.publicName;

                if (string.IsNullOrEmpty(name)) name = AnonynousName;

                _leaderboardPlayers.Add(new LeaderboardPlayer(rank, name, score));
            }

            _leaderboardView.FillingLeaderboard(_leaderboardPlayers);
        });
    }

    public void Open()
    {
        if (PlayerAccount.IsAuthorized == true)
        {
            PlayerAccount.RequestPersonalProfileDataPermission();
            OpenLeaderBoard();
        }
        else _dialogPanel.ShowPanel();
    }

    public void Authorize()
    {
        PlayerAccount.Authorize();

        if (PlayerAccount.IsAuthorized == true)
        {
            OpenLeaderBoard();
        }
        else return;
    }

    private void OpenLeaderBoard()
    {
        SetLeaderboardValue(LeaderboardName, _config.PlayerScore);
        Fill();
        _leaderboardView.Open();
    }

    private void SetLeaderboardValue(string leaderboard, int newScore)
    {
        int oldScore = 0;
        string name = null;

        Leaderboard.GetEntries(leaderboard, (result) =>
        {
            foreach (var entry in result.entries)
            {
                name = entry.player.publicName;

                if (string.IsNullOrEmpty(name)) name = "Anonymous";

                oldScore = entry.score;
            }
        });

        if (oldScore > newScore)
        {
            Leaderboard.SetScore(leaderboard, oldScore);
        }
        else Leaderboard.SetScore(leaderboard, newScore);
    }
}
