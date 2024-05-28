using System.Collections;
using UnityEngine;
using Agava.YandexGames;
using UnityEngine.SceneManagement;

public class SdkInitializer : MonoBehaviour
{
    private readonly string _firstScene = "Menu";

    private void Awake()
    {
        YandexGamesSdk.CallbackLogging = true;
    }

    private IEnumerator Start()
    {
        yield return YandexGamesSdk.Initialize(OnInitialize);
    }

    private void OnInitialize()
    {
        SceneManager.LoadScene(_firstScene);
    }
}
