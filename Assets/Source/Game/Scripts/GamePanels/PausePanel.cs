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

    private void Awake()
    {
        gameObject.SetActive(false);
        _levelObserver.GamePaused += Open;
        _levelObserver.GameResumed += Close;
        _levelObserver.SoundMuted += SetButtonImage;
    }

    private void OnDestroy()
    {
        _levelObserver.GamePaused -= Open;
        _levelObserver.GameResumed -= Close;
        _levelObserver.SoundMuted -= SetButtonImage;
    }

    protected override void Open()
    {
        base.Open();
        _levelObserver.PlayerInterfaceView.gameObject.SetActive(false);
    }

    protected override void Close()
    {
        base.Close();
        _levelObserver.PlayerInterfaceView.gameObject.SetActive(true);
    }

    private void SetButtonImage(bool state)
    {
        _imageButton.sprite = state == true ? _muteButton : _unmuteButton;
    }
}