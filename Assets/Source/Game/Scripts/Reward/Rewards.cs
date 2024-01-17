using UnityEngine;
using Agava.YandexGames;
using System.Collections;

public class Rewards : MonoBehaviour
{
    private readonly float _pauseValue = 0;
    private readonly float _resumeValue = 1f;

    [Header("[Rewards Coin Multiplier]")]
    [SerializeField] private int _coinMultiplier = 2;
    [Header("[Level Parameters]")]
    [SerializeField] private LevelParameters _levelParameters;
    [Header("[Rewards Sound]")]
    [SerializeField] private RewardsSound _rewardsSound;

    private Player _player;
    private bool _isCloseFullScreenAd;

    public int CoinMultiplier => _coinMultiplier;
    public AudioClip AudioClipWin => _rewardsSound.AudioClipWin;
    public AudioClip AudioClipLose => _rewardsSound.AudioClipLose;
    public AudioSource AudioSource => _rewardsSound.AudioSource;

    public void OpenRewardAd() => VideoAd.Show(OnOpenCallback, OnRewardCallback, OnCloseCallback);

    public void OpenFullScreenAd() => InterstitialAd.Show(OnOpenCallback, OnCloseAddCallback, OnErrorCallback);

    public void OpenAd() => WaitingAdClose();

    private void OnOpenCallback()
    {
        Time.timeScale = _pauseValue;
        AudioListener.pause = true;
        _levelParameters.LevelSound.volume = _pauseValue;
    }

    private void OnCloseCallback()
    {
        Time.timeScale = _resumeValue;
        AudioListener.pause = false;
    }

    private void OnCloseAddCallback(bool state)
    {
        _isCloseFullScreenAd = true;
        OnCloseCallback();
    }

    private void OnErrorCallback(string state)
    {
        _isCloseFullScreenAd = true;
    }

    private void OnRewardCallback()
    {
        _player = FindObjectOfType<Player>();
        _player.Wallet.TakeCoins(_levelParameters.LevelObserver.CountMoneyEarned);
    }

    private IEnumerator WaitingAdClose()
    {
        OpenFullScreenAd();

        while (_isCloseFullScreenAd != true)
        {
            yield return null;
        }
    }
}