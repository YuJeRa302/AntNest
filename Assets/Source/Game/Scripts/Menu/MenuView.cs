using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Game.Scripts
{
    public class MenuView : MonoBehaviour
    {
        [SerializeField] private MenuPanel _menuPanel;
        [SerializeField] private Button _soundButton;
        [SerializeField] private Image _imageButton;
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

            if (SoundStateChanged != null)
                SoundStateChanged?.Invoke(state);
        }
    }
}