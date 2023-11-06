using UnityEngine;
using Agava.YandexGames;

public class Rewards : MonoBehaviour
{
    [Header("[Rewards Coin Multiplier]")]
    [SerializeField] private int _coinMultiplier = 2;
    [Header("[Level Parameters]")]
    [SerializeField] private LevelParameters _levelParameters;
    [Header("[Audio Source]")]
    [SerializeField] private AudioSource _audioSource;
    [Header("[Audio Clip]")]
    [SerializeField] private AudioClip _audioClipWin;
    [SerializeField] private AudioClip _audioClipLose;

    private Player _player;
    private bool _isClosedFullScreenAd = false;

    public int CoinMultiplier => _coinMultiplier;
    public bool IsClosedFullScreenAd => _isClosedFullScreenAd;
    public AudioClip AudioClipWin => _audioClipWin;
    public AudioClip AudioClipLose => _audioClipLose;
    public AudioSource AudioSource => _audioSource;

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