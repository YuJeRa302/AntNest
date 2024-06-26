using System;
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

    public event Action<bool> SoundStateChanged;

    private void Awake()
    {
        _soundButton.onClick.AddListener(SetSoundState);
    }

    private void OnDestroy()
    {
        _soundButton.onClick.RemoveListener(SetSoundState);
    }

    public void Initialize()
    {
        _imageButton.sprite = _menuPanel.Config.IsSoundOn == true ? _unmuteButtonSprite : _muteButtonSprite;
    }

    private void SetSoundState()
    {
        bool state = _menuPanel.Config.IsSoundOn != true;
        _imageButton.sprite = state == true ? _unmuteButtonSprite : _muteButtonSprite;
        SoundStateChanged?.Invoke(state);
    }
}