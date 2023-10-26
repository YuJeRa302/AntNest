using UnityEngine;
using YG;

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
        YandexGame.RewVideoShow(reward);
    }

    public void OpenFullScreenAd()
    {
        YandexGame.FullscreenShow();
    }

    private void Start()
    {
        YandexGame.CloseFullAdEvent += ClosedFullScreenAd;
    }

    private void OnEnable() => YandexGame.RewardVideoEvent += Rewarded;
    private void OnDisable() => YandexGame.RewardVideoEvent -= Rewarded;

    private void Rewarded(int reward)
    {
        MultiplyPlayerReward(reward);
    }

    private void MultiplyPlayerReward(int reward)
    {
        if (reward == 1) _player.Wallet.TakeCoin(_levelParameters.CountMoneyEarned);
    }

    public void ClosedFullScreenAd()
    {
        _isClosedFullScreenAd = true;
    }
}