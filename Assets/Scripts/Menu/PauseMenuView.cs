using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class PauseMenuView : MonoBehaviour
{
    [Header("[ShopPanel]")]
    [SerializeField] private ShopPanel _shopPanel;
    [Header("[Buttons]")]
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _restartButton;
    [Header("[Menu Panel]")]
    [SerializeField] private GameObject _menuPanel;
    [Header("[Reward Panel]")]
    [SerializeField] private GameObject _rewardPanel;
    [Header("[Level Finish]")]
    [SerializeField] private TMP_Text _levelState;
    [Header("[Rewards]")]
    [SerializeField] private TMP_Text _coinsReward;
    [SerializeField] private TMP_Text _expReward;
    [SerializeField] private TMP_Text _countKillEnemies;

    public void ResumeGame()
    {
        gameObject.SetActive(false);
        DefaultSetButtons();
    }

    public void PauseGame()
    {
        gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        ResumeGame();
    }

    public void OpenShopPanel()
    {
        _shopPanel.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public void ChagenButtons()
    {
        _resumeButton.gameObject.SetActive(false);
        _restartButton.gameObject.SetActive(true);
    }

    public void ShowRewardPanel(string state, int coinsReward, int expReward, int countKillEnemies)
    {
        _levelState.text = state;
        _coinsReward.text = coinsReward.ToString();
        _expReward.text = expReward.ToString();
        _countKillEnemies.text = countKillEnemies.ToString();
        _menuPanel.SetActive(false);
        _rewardPanel.SetActive(true);
    }

    public void CloseRewardPanel()
    {
        _menuPanel.SetActive(true);
        _rewardPanel.SetActive(false);
    }

    private void DefaultSetButtons()
    {
        _resumeButton.gameObject.SetActive(true);
        _restartButton.gameObject.SetActive(false);
    }
}