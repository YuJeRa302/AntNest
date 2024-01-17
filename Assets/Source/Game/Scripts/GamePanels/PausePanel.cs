using UnityEngine;
using UnityEngine.UI;

public class PausePanel : GamePanels
{
    [Header("[Level Entities]")]
    [SerializeField] private LevelObserver _levelObserver;
    [Header("[UI]")]
    [SerializeField] private Button _soundButton;
    [SerializeField] private Image _imageButton;
    [SerializeField] private Sprite _muteButton;
    [SerializeField] private Sprite _unmuteButton;

    private void OnEnable()
    {
        _levelObserver.GamePaused += OpenPanel;
        _levelObserver.GameResumed += ClosePanel;
        _levelObserver.SoundMuted += SetButtonImage;
    }

    private void OnDisable()
    {
        _levelObserver.GamePaused -= OpenPanel;
        _levelObserver.GameResumed -= ClosePanel;
        _levelObserver.SoundMuted -= SetButtonImage;
    }

    private void SetButtonImage(bool state)
    {
        _imageButton.sprite = state == true ? _muteButton : _unmuteButton;
    }
}