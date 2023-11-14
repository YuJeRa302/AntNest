using UnityEngine;
using UnityEngine.UI;

public class RewardView : MonoBehaviour
{
    [Header("[Rewards With Ads]")]
    [SerializeField] private Text _coinsRewardWithAds;
    [SerializeField] private Text _expRewardWithAds;
    [SerializeField] private Text _countKillEnemiesWithAds;
    [Header("[Rewards Without Ads]")]
    [SerializeField] private Text _coinsRewardWithoutAds;
    [SerializeField] private Text _expRewardWithoutAds;
    [SerializeField] private Text _countKillEnemiesWithoutAds;
    [Header("[Win Objects]")]
    [SerializeField] private GameObject _winGameObject;
    [Header("[Lose Objects]")]
    [SerializeField] private GameObject _loseGameObject;
    [Header("[Menu Panel]")]
    [SerializeField] private GameObject _menuPanel;
    [Header("[Rewards]")]
    [SerializeField] private Rewards _rewards;

    public void ShowRewardPanel(bool state, int coinsReward, int expReward, int countKillEnemies)
    {
        gameObject.SetActive(true);
        GetReawrdValue(state, coinsReward, expReward, countKillEnemies);
        _menuPanel.SetActive(false);
    }

    public void CloseRewardPanel()
    {
        gameObject.SetActive(false);
    }

    private void GetReawrdValue(bool state, int coinsReward, int expReward, int countKillEnemies)
    {
        SetEnding(state);
        SetReawrdValue(_coinsRewardWithAds, _expRewardWithAds, _countKillEnemiesWithAds,
            coinsReward * _rewards.CoinMultiplier, expReward, countKillEnemies);
        SetReawrdValue(_coinsRewardWithoutAds, _expRewardWithoutAds, _countKillEnemiesWithoutAds,
            coinsReward, expReward, countKillEnemies);
    }

    private void SetEnding(bool state)
    {
        switch (state)
        {
            case true:
                SetEndingWin();
                break;
            case false:
                SetEndingLose();
                break;
        }
    }

    private void SetReawrdValue(Text coinsReward, Text expReward, Text countKillEnemies, int coinsRewardValue, int expRewardValue, int countKillEnemiesValue)
    {
        coinsReward.text = coinsRewardValue.ToString();
        expReward.text = expRewardValue.ToString();
        countKillEnemies.text = countKillEnemiesValue.ToString();
    }

    private void SetEndingWin()
    {
        _winGameObject.SetActive(true);
        _rewards.AudioSource.PlayOneShot(_rewards.AudioClipWin);
    }

    private void SetEndingLose()
    {
        _loseGameObject.SetActive(true);
        _rewards.AudioSource.PlayOneShot(_rewards.AudioClipLose);
    }
}