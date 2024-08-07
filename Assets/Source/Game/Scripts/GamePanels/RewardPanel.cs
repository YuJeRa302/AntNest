using System;
using Agava.YandexGames;
using Lean.Localization;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Game.Scripts
{
    public class RewardPanel : GamePanels
    {
        [SerializeField] private LevelObserver _levelObserver;
        [Header("[Rewards Text]")]
        [SerializeField] private Text _coinsRewardWithAds;
        [SerializeField] private Text _expRewardWithAds;
        [SerializeField] private Text _countKillEnemiesWithAds;
        [SerializeField] private Text _countCoinPerReward;
        [Header("[End Screen]")]
        [SerializeField] private Image _endImage;
        [SerializeField] private Sprite _winSprite;
        [SerializeField] private Sprite _loseSprite;
        [SerializeField] private LeanLocalizedText _endText;
        [SerializeField] private string _winText;
        [SerializeField] private string _loseText;
        [Header("[Buttons]")]
        [SerializeField] private Button _closePanelButton;
        [SerializeField] private Button _openAdButton;
        [SerializeField] private Button _closeRewardScreenButton;
        [Space(50)]
        [SerializeField] private GameObject _rewardScreen;

        public event Action RewardPanelClosed;
        public event Action<bool> RewardPanelOpened;
        public event Action<int> RewardScreenOpened;

        private void Awake()
        {
            _levelObserver.GameEnded += Open;
            _levelObserver.LevelCompleted += GetReawrdValue;
            _closePanelButton.onClick.AddListener(Close);
            _openAdButton.onClick.AddListener(OpenRewardAd);
            _closeRewardScreenButton.onClick.AddListener(CloseRewardScreen);
            _rewardScreen.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            _levelObserver.GameEnded -= Open;
            _levelObserver.LevelCompleted -= GetReawrdValue;
            _closePanelButton.onClick.RemoveListener(Close);
            _openAdButton.onClick.RemoveListener(OpenRewardAd);
            _closeRewardScreenButton.onClick.RemoveListener(CloseRewardScreen);
        }

        protected override void Open()
        {
            gameObject.SetActive(true);
            LevelObserver.PlayerInterfaceView.gameObject.SetActive(false);
        }

        protected override void Close()
        {
#if UNITY_EDITOR
            AdClosed?.Invoke();
            RewardPanelClosed?.Invoke();
#else
        InterstitialAd.Show(OnOpenAdCallback, OnCloseInterstitialAdCallback, OnErrorCallback);
#endif
        }

        private void OpenRewardScreen()
        {
            RewardScreenOpened?.Invoke(_levelObserver.CountMoneyEarned);
            _rewardScreen.gameObject.SetActive(true);
            _countCoinPerReward.text = "+ " + _levelObserver.CountMoneyEarned.ToString();
            _openAdButton.gameObject.SetActive(false);
            _closePanelButton.gameObject.SetActive(false);
        }

        private void CloseRewardScreen()
        {
            _closePanelButton.gameObject.SetActive(true);
            _rewardScreen.gameObject.SetActive(false);

            SetReawrdValue(
                _coinsRewardWithAds,
                _expRewardWithAds,
                _countKillEnemiesWithAds,
                _levelObserver.CountMoneyEarned,
                _levelObserver.CountExpEarned,
                _levelObserver.CountKillEnemy);
        }

        private void OpenRewardAd() => VideoAd.Show(OnOpenAdCallback, OnRewardCallback, OnCloseAdCallback);

        private void OnOpenAdCallback()
        {
            AdOpened?.Invoke();
        }

        private void OnCloseAdCallback() 
        {
        }

        private void OnCloseInterstitialAdCallback(bool state)
        {
            AdClosed?.Invoke();
            RewardPanelClosed?.Invoke();
        }

        private void OnErrorCallback(string state)
        {
            RewardPanelClosed?.Invoke();
        }

        private void OnRewardCallback()
        {
            OpenRewardScreen();
        }

        private void GetReawrdValue(bool state)
        {
            RewardPanelOpened?.Invoke(state);
            SetEndingImage(state);

            SetReawrdValue(
                _coinsRewardWithAds,
                _expRewardWithAds,
                _countKillEnemiesWithAds,
                _levelObserver.CountMoneyEarned,
                _levelObserver.CountExpEarned,
                _levelObserver.CountKillEnemy);
        }

        private void SetEndingImage(bool state)
        {
            _endImage.sprite = state == true ? _winSprite : _loseSprite;
            _endText.TranslationName = state == true ? _winText : _loseText;
        }

        private void SetReawrdValue(Text coinsReward, Text expReward, Text countKillEnemies, int coins, int exp, int killCountEnemies)
        {
            coinsReward.text = coins.ToString();
            expReward.text = exp.ToString();
            countKillEnemies.text = killCountEnemies.ToString();
        }
    }
}