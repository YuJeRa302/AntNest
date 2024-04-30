using UnityEngine;
using Agava.YandexGames;
using Lean.Localization;
using UnityEngine.UI;
using System;

public class RewardPanel : GamePanels
{
    [SerializeField] private int _coinMultiplier = 2;
    [Header("[Level Entities]")]
    [SerializeField] private LevelObserver _levelObserver;
    [Header("[Rewards With Ads]")]
    [SerializeField] private Text _coinsRewardWithAds;
    [SerializeField] private Text _expRewardWithAds;
    [SerializeField] private Text _countKillEnemiesWithAds;
    [Header("[Rewards Without Ads]")]
    [SerializeField] private Text _coinsRewardWithoutAds;
    [SerializeField] private Text _expRewardWithoutAds;
    [SerializeField] private Text _countKillEnemiesWithoutAds;
    [Header("[End Screen]")]
    [SerializeField] private Image _endImage;
    [SerializeField] private Sprite _winSprite;
    [SerializeField] private Sprite _loseSprite;
    [SerializeField] private LeanLocalizedText _endText;
    [SerializeField] private string _winText;
    [SerializeField] private string _loseText;
    [Header("[Buttons]")]
    [SerializeField] private Button _closePanel;
    [SerializeField] private Button _openAd;

    public event Action RewardPanelClosed;

    private void Awake()
    {
        gameObject.SetActive(false);
        _levelObserver.GameEnded += Open;
        _levelObserver.LevelCompleted += GetReawrdValue;
        _closePanel.onClick.AddListener(Close);
        _openAd.onClick.AddListener(OpenRewardAd);
    }

    private void OnDestroy()
    {
        _levelObserver.GameEnded -= Open;
        _levelObserver.LevelCompleted -= GetReawrdValue;
        _closePanel.onClick.RemoveListener(Close);
        _openAd.onClick.RemoveListener(OpenRewardAd);
    }

    protected override void Open()
    {
        base.Open();
        _levelObserver.PlayerInterfaceView.gameObject.SetActive(false);
    }

    protected override void Close()
    {
        gameObject.SetActive(false);

#if UNITY_WEBGL && !UNITY_EDITOR
        InterstitialAd.Show(OnOpenAdCallback, OnCloseInterstitialAdCallback, OnErrorCallback);
#endif
        PanelClosed?.Invoke();
        RewardPanelClosed.Invoke();
    }

    private void OpenRewardAd() => VideoAd.Show(OnOpenAdCallback, OnRewardCallback, OnCloseAdCallback);

    private void OnOpenAdCallback()
    {
        OpenAd?.Invoke();
    }

    private void OnCloseAdCallback() { }

    private void OnCloseInterstitialAdCallback(bool state)
    {
        CloseAd?.Invoke();
    }

    private void OnErrorCallback(string state)
    {
        CloseAd?.Invoke();
    }

    private void OnRewardCallback()
    {
        Player.Wallet.TakeCoins(_levelObserver.CountMoneyEarned);
    }

    private void GetReawrdValue(bool state)
    {
        SetEndingImage(state);
        SetReawrdValue(_coinsRewardWithAds, _expRewardWithAds, _countKillEnemiesWithAds,
            _levelObserver.CountMoneyEarned * _coinMultiplier, _levelObserver.CountExpEarned, _levelObserver.CountKillEnemy);
        SetReawrdValue(_coinsRewardWithoutAds, _expRewardWithoutAds, _countKillEnemiesWithoutAds,
            _levelObserver.CountMoneyEarned, _levelObserver.CountExpEarned, _levelObserver.CountKillEnemy);
    }

    private void SetEndingImage(bool state)
    {
        _endImage.sprite = state == true ? _winSprite : _loseSprite;
        _endText.TranslationName = state == true ? _winText : _loseText;
    }

    private void SetReawrdValue(Text coinsReward, Text expReward, Text countKillEnemies, int coinsRewardValue, int expRewardValue, int countKillEnemiesValue)
    {
        coinsReward.text = coinsRewardValue.ToString();
        expReward.text = expRewardValue.ToString();
        countKillEnemies.text = countKillEnemiesValue.ToString();
    }
}
