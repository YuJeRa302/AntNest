using UnityEngine;

public class SoundController : MonoBehaviour
{
    [Header("[Audio Source]")]
    [SerializeField] private AudioSource _ambientAudioSource;
    [SerializeField] private AudioSource _interfaceAudioSource;
    [Header("[Level Entities]")]
    [SerializeField] private LevelObserver _levelObserver;

    private void OnEnable()
    {
        _levelObserver.SoundMuted += OnMuted;
    }

    private void OnDisable()
    {
        _levelObserver.SoundMuted -= OnMuted;
    }

    public void Initialize(LoadConfig loadConfig)
    {
        SetAmbientVolume(loadConfig.AmbientVolume);
        SetInterfaceVolume(loadConfig.InterfaceVolume);
    }

    private void OnMuted(bool state)
    {
        _ambientAudioSource.mute = state;
        _interfaceAudioSource.mute = state;
    }

    private void SetAmbientVolume(float value)
    {
        _ambientAudioSource.volume = value;
    }

    private void SetInterfaceVolume(float value)
    {
        _interfaceAudioSource.volume = value;
    }
}