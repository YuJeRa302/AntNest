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
        if (PlayerAccount.IsAuthorized == true) PlayerAccount.RequestPersonalProfileDataPermission();

        if (PlayerAccount.IsAuthorized == false) return;

        OnSuccessLoad();
    }

    public void Authorize()
    {
        PlayerAccount.Authorize();

        if (PlayerAccount.IsAuthorized == true) PlayerAccount.RequestPersonalProfileDataPermission();

        if (PlayerAccount.IsAuthorized == false) return;

        OnSuccessLoad();
    }

    private void OnSuccessLoad()
    {
        SetPlayer(_config.PlayerScore);
        Fill();
        _leaderboardView.Open();
    }
}
