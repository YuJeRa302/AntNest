using IJunior.TypedScenes;
using UnityEngine;

namespace Assets.Source.Game.Scripts
{
    public class SceneLoader : MonoBehaviour, ISceneLoadHandler<LoadConfig>
    {
        [SerializeField] private LevelInizialisator _levelInizialisator;

        public void OnSceneLoaded(LoadConfig loadConfig)
        {
            _levelInizialisator.Initialize(loadConfig);
        }
    }
}