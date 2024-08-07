using System.Collections;
using Agava.YandexGames;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Source.Game.Scripts
{
    public class SdkInitializer : MonoBehaviour
    {
        private readonly string _firstScene = "Menu";

        private void Awake()
        {
#if UNITY_EDITOR
            OnInitialize();
#else
        YandexGamesSdk.CallbackLogging = true;
#endif
        }

        private IEnumerator Start()
        {
#if UNITY_EDITOR
            yield return null;
#else
        yield return YandexGamesSdk.Initialize(OnInitialize);
#endif
        }

        private void OnInitialize()
        {
            SceneManager.LoadScene(_firstScene);
        }
    }
}