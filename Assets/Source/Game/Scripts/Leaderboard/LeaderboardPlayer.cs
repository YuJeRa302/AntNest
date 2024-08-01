using UnityEngine;

namespace Assets.Source.Game.Scripts
{
    public class LeaderboardPlayer : MonoBehaviour
    {
        public LeaderboardPlayer(int rank, string name, int score)
        {
            this.Rank = rank;
            this.Name = name;
            this.Score = score;
        }

        public int Rank { get; private set; }
        public string Name { get; private set; }
        public int Score { get; private set; }
    }
}