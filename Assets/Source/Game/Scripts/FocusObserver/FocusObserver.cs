using UnityEngine;
using Agava.WebUtility;

public class FocusObserver : MonoBehaviour
{
    private readonly float _pauseValue = 0;
    private readonly float _resumeValue = 1f;

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
        if (state == true) ResumeGame();
        else PauseGame();
    }

    private void PauseGame()
    {
        AudioListener.pause = true;
        AudioListener.volume = _pauseValue;
        Time.timeScale = _pauseValue;
    }

    private void ResumeGame()
    {
        AudioListener.pause = false;
        AudioListener.volume = _resumeValue;
        Time.timeScale = _resumeValue;
    }
}
