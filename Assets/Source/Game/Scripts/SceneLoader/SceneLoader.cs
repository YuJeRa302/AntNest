using IJunior.TypedScenes;
using UnityEngine;

public class SceneLoader : MonoBehaviour, ISceneLoadHandler<LoadConfig>
{
    [Header("[Audio Source]")]
    [SerializeField] private LevelSounds _levelSounds;
    [SerializeField] private ButtonFX _buttonFX;
    [Header("[LevelParameters]")]
    [SerializeField] private LevelParameters _levelParameters;

    public void OnSceneLoaded(LoadConfig loadConfig)
    {
        _levelSounds.SetValueVolume(loadConfig.AmbientVolume);
        _buttonFX.SetValueVolume(loadConfig.InterfaceVolume);
        _levelParameters.Initialized(loadConfig);
    }
}
