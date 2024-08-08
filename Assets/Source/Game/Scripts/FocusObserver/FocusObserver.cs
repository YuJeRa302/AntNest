using Agava.WebUtility;
using UnityEngine;

namespace Assets.Source.Game.Scripts
{
    [RequireComponent(typeof(PauseHandler))]
    public class FocusObserver : MonoBehaviour
    {
        [SerializeField] private PauseHandler _pauseHandler;

        private void Awake()
        {
            if (Application.isFocused == false)
                ChangeFocus(Application.isFocused);
        }

        private void OnEnable()
        {
            Application.focusChanged += OnInBackgroundChangeApp;
            WebApplication.InBackgroundChangeEvent += OnInBackgroundChangeWeb;
        }

        private void OnDisable()
        {
            Application.focusChanged -= OnInBackgroundChangeApp;
            WebApplication.InBackgroundChangeEvent -= OnInBackgroundChangeWeb;
        }

        private void OnInBackgroundChangeApp(bool inApp)
        {
            ChangeFocus(inApp);
        }

        private void OnInBackgroundChangeWeb(bool inBackground)
        {
            ChangeFocus(!inBackground);
        }

        private void ChangeFocus(bool state)
        {
            if (state)
                _pauseHandler.ResumeGame();
            else
                _pauseHandler.PauseGame();
        }

        [ContextMenu("Loose Focus")]
        public void LooseFocus()
        {
            OnInBackgroundChangeApp(false);
            OnInBackgroundChangeWeb(true);
        }

        [ContextMenu("Get Focus")]
        public void GetFocus()
        {
            OnInBackgroundChangeApp(true);
            OnInBackgroundChangeWeb(false);
        }
    }
}