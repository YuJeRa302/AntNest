using System.Collections.Generic;
using UnityEngine;
using Agava.YandexGames;
using UnityEngine.UI;

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
    [Header("[Buttons]")]
    [SerializeField] private Button _openButton;
    [SerializeField] private Button _openAuthorize;

    private readonly List<LeaderboardPlayer> _leaderboardPlayers = new();

    private void Awake()
    {
        _openButton.onClick.AddListener(Open);
        _openAuthorize.onClick.AddListener(Authorize);
    }

    private void OnDestroy()
    {
        _openButton.onClick.RemoveListener(Open);
        _openAuthorize.onClick.RemoveListener(Authorize);
    }

    private void SetPlayer(int score)
    {
        if (PlayerAccount.IsAuthorized == false)
            return;

        Leaderboard.GetPlayerEntry(LeaderboardName, _ =>
        {
            Leaderboard.SetScore(LeaderboardName, score, OnPlayerScoreSet);
        });
    }

    private void Fill()
    {
        _leaderboardPlayers.Clear();

        if (PlayerAccount.IsAuthorized == false)
            return;

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

    private void Open()
    {
        if (PlayerAccount.IsAuthorized == true)
            PlayerAccount.RequestPersonalProfileDataPermission();
        else
            _dialogPanel.gameObject.SetActive(true);

        OnSuccessLoad();
    }

    private void Authorize()
    {
        PlayerAccount.Authorize();

        if (PlayerAccount.IsAuthorized == true)
            PlayerAccount.RequestPersonalProfileDataPermission();
        else
            return;

        OnSuccessLoad();
    }

    private void OnPlayerScoreSet()
    {
        Fill();
        _leaderboardView.Open();
    }

    private void OnSuccessLoad()
    {
        SetPlayer(_config.PlayerScore);
    }
}
