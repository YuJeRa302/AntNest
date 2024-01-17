using UnityEngine;
using UnityEngine.UI;

public class LeaderboardElement : MonoBehaviour
{
    [SerializeField] private Text _playerName;
    [SerializeField] private Text _playerRank;
    [SerializeField] private Text _playerScore;

    public void Initialize(string name, int rank, int score)
    {
        _playerName.text = name;
        _playerRank.text = rank.ToString();
        _playerScore.text = score.ToString();
    }
}