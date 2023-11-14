using UnityEngine;
using Agava.WebUtility;

public class FocusObserver : MonoBehaviour
{
    private void OnEnable()
    {
        Application.focusChanged += OnInBackgroundChange;
        WebApplication.InBackgroundChangeEvent += OnInBackgroundChange;
    }

    private void OnDisable()
    {
        Application.focusChanged -= OnInBackgroundChange;
        WebApplication.InBackgroundChangeEvent -= OnInBackgroundChange;
    }

    private void OnInBackgroundChange(bool inBackground)
    {
        MuteSound(inBackground);
        PauseGame(inBackground);
    }

    private void MuteSound(bool value)
    {
        AudioListener.pause = value;
        AudioListener.volume = value ? 0f : 1f;
    }

    private void PauseGame(bool value)
    {
        Time.timeScale = value ? 0 : 1;
    }
}
