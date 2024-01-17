using UnityEngine.UI;
using UnityEngine;

public class PauseMenuView : MonoBehaviour
{
    [Header("[Buttons]")]
    [SerializeField] private Button _resumeButton;
    [Header("[Menu Panel]")]
    [SerializeField] private GameObject _menuPanel;
    [Header("[Mute Button Image]")]
    [SerializeField] private Image _imageButton;
    [Header("[Sprite Mute Button]")]
    [SerializeField] private Sprite _muteButton;
    [SerializeField] private Sprite _unmuteButton;
    [Header("[Reward View]")]
    [SerializeField] private RewardView _rewardPanel;

    public Image MuteImageButton => _imageButton;

    public void SetMuteButtonImage(bool state)
    {
        MuteImageButton.sprite = state == true ? _muteButton : _unmuteButton;
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }
}