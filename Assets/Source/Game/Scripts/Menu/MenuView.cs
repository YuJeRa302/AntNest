using UnityEngine;
using UnityEngine.UI;

public class MenuView : MonoBehaviour
{
    [Header("[Menu Panel]")]
    [SerializeField] private MenuPanel _menuPanel;
    [Header("[Mute Button]")]
    [SerializeField] private Button _soundButton;
    [Header("[Mute Button Image]")]
    [SerializeField] private Image _imageButton;
    [Header("[Sprite Mute Button]")]
    [SerializeField] private Sprite _muteButtonSprite;
    [SerializeField] private Sprite _unmuteButtonSprite;

    private void Awake()
    {
        gameObject.SetActive(false);
        _soundButton.onClick.AddListener(SetSoundState);
    }

    private void OnDestroy()
    {
        _soundButton.onClick.RemoveListener(SetSoundState);
    }

    private void SetSoundState()
    {
        var state = _menuPanel.MenuSound.AmbientSounds.mute != true;
        _imageButton.sprite = state == true ? _muteButtonSprite : _unmuteButtonSprite;
        _menuPanel.MenuSound.SetStateMuteSound(state);
    }
}