using UnityEngine;
using Agava.YandexGames;

public class Rewards : MonoBehaviour
{
    [Header("[Rewards Coin Multiplier]")]
    [SerializeField] private int _coinMultiplier = 2;
    [Header("[Level Parameters]")]
    [SerializeField] private LevelParameters _levelParameters;
    [Header("[Rewards Sound]")]
    [SerializeField] private RewardsSound _rewardsSound;

    private Player _player;

    public int CoinMultiplier => _coinMultiplier;
    public AudioClip AudioClipWin => _rewardsSound.AudioClipWin;
    public AudioClip AudioClipLose => _rewardsSound.AudioClipLose;
    public AudioSource AudioSource => _rewardsSound.AudioSource;

    public void OpenRewardAd(int reward)
    {
        _player = FindObjectOfType<Player>();
#if UNITY_WEBGL && !UNITY_EDITOR
    VideoAd.Show();
#endif
        MultiplyPlayerReward(reward);
    }

    public void OpenFullScreenAd()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
    InterstitialAd.Show();
#endif
    }

    private void MultiplyPlayerReward(int reward)
    {
        if (reward == 1) _player.Wallet.TakeCoin(_levelParameters.CountMoneyEarned);
    }
}