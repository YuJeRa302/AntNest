using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Source.Game.Scripts
{
    public class LanguageButtonView : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _image;

        private string _languageTag;
        private AudioClip _hover;
        private AudioClip _click;
        private AudioSource _audioSource;

        public event Action<string> LanguageSelected;

        private void Awake()
        {
            _button.onClick.AddListener(OnSelectLanguage);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnSelectLanguage);
        }

        public void Initialize(LanguageButtonState languageButtonState, AudioSource audioSource, AudioClip click, AudioClip hover)
        {
            _image.sprite = languageButtonState.LanguageButtonData.IconLanguage;
            _languageTag = languageButtonState.LanguageButtonData.NameLanguage;
            _audioSource = audioSource;
            _hover = hover;
            _click = click;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _audioSource.PlayOneShot(_hover);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _audioSource.PlayOneShot(_click);
        }

        private void OnSelectLanguage()
        {
            if (LanguageSelected != null)
                LanguageSelected.Invoke(_languageTag);
        }
    }
}