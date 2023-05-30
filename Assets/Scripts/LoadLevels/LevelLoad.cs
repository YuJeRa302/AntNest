using System.Collections;
using UnityEngine;

public class LevelLoad : MonoBehaviour
{
    [Header("[PlayerSpawn]")]
    [SerializeField] private Transform _spawnLevelFirst;
    [SerializeField] private Transform _spawnLevelSecond;
    //[Header("[Sound]")]
    //[SerializeField] private LevelSounds _levelSounds;
    [Header("[Controller]")]
    [SerializeField] private LevelParameters _levelParameters;
    [Header("[Location]")]
    [SerializeField] private GameObject _menuLocation;
    [SerializeField] private GameObject _gameLocation;
    [Header("[Level Panel]")]
    [SerializeField] private GameObject _loadScreen;
    [SerializeField] private GameObject _startPanel;
    [Header("[Level Spawn]")]
    [SerializeField] private LevelSpawn _levelSpawn;
    [Header("[Laod Screen]")]
    [SerializeField] private Canvas _loadCanvas;
    [Header("[Delay]")]
    [SerializeField] private float _delayLoadScreen = 10f;
    [SerializeField] private float _delayEndScreen = 4f;

    private IEnumerator _loadLevel;
    private Player _player;

    public void LoadFirstLoaction(GameObject screen)
    {
        SetLoadScreen(screen, true, false);
        SetActiveLevel(true, false);
    }

    public void LoadGameLoaction(Levels loadLevel)
    {
        //_levelParameters.TakeParametrsLevel(loadLevel, _spawnLevelSecond);
        SetLoadScreen(null, false, true);
        SetActiveLevel(false, true);
    }

    private void SetLoadScreen(GameObject screen, bool soundFirstLevel, bool soundSecondLevel)
    {
        _loadLevel = LoadingLevel(screen, soundFirstLevel, soundSecondLevel);
        StartCoroutine(_loadLevel);
    }

    private void SetActiveLevel(bool SetActiveFirstLevel, bool SetActiveSecondLevel)
    {
        _menuLocation.gameObject.SetActive(SetActiveFirstLevel);
        _gameLocation.gameObject.SetActive(SetActiveSecondLevel);
    }

    private IEnumerator LoadingLevel(GameObject endedScreen, bool soundFirstLevel, bool soundSecondLevel)
    {
        //_levelSpawn.FindEnemy();
        LoadCanvas();

        if (endedScreen != null)
        {
            endedScreen.SetActive(true);
            yield return new WaitForSeconds(_delayEndScreen);
            endedScreen.SetActive(false);
        }

        yield return new WaitForSeconds(_delayLoadScreen);
        ChangeLevel(endedScreen, soundFirstLevel, soundSecondLevel);
    }

    private void LoadCanvas()
    {
        _loadCanvas.gameObject.SetActive(true);
        _loadScreen.SetActive(true);
    }

    private void EndedLevel()
    {
        _startPanel.SetActive(false);
        _loadCanvas.gameObject.SetActive(false);
        _player.Recover();
    }

    private void StartLevel()
    {
        _loadScreen.SetActive(false);
        _startPanel.SetActive(true);
    }

    private void ChangeLevel(GameObject endedScreen, bool soundFirstLevel, bool soundSecondLevel)
    {
        //_levelSounds.ChangeSound(soundFirstLevel, soundSecondLevel);

        if (endedScreen != null)
        {
            EndedLevel();
        }
        else
        {
            StartLevel();
        }
    }
}