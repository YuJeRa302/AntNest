using UnityEngine;
using IJunior.TypedScenes;
using System.Collections;

[RequireComponent(typeof(ButtonsView))]

public class Buttons : MonoBehaviour
{
    private readonly int _zeroWave = 0;

    [Header("[Level Prefab]")]
    [SerializeField] private Levels _level;
    [Header("[Config]")]
    [SerializeField] private LoadConfig _loadConfig;
    [Header("[CanvasLoader]")]
    [SerializeField] private CanvasLoader _canvasLoader;
    [Header("[QuestPanel]")]
    [SerializeField] private QuestPanel _questPanel;
    [Header("[Level Mode Description]")]
    [SerializeField] private string _standartLevelDescription;
    [SerializeField] private string _endlessLevelDescription;
    [Header("[Hints Text]")]
    [SerializeField] private string _hintsText;
    [Header("[Buttons Animator]")]
    [SerializeField] private Animator[] _animators;
    [Header("[Buttons View]")]
    [SerializeField] private ButtonsView _buttonsView;

    private AsyncOperation _load;

    enum TransitionParametr
    {
        Play
    }

    public bool IsLevelComplete => _level.IsComplete;
    public Levels Levels => _level;
    public ButtonsView ButtonsView => _buttonsView;

    public void GetParameters(LoadConfig config, CanvasLoader canvas, QuestPanel questPanel)
    {
        _loadConfig = config;
        _canvasLoader = canvas;
        _questPanel = questPanel;
    }

    public void LoadLevel()
    {
        if (_level.IsStandart != false || _level.IsEndless != false)
        {
            _loadConfig.SetLevelParameters(_level);
            LoadScene(_level.NameScene, _loadConfig);
        }
        else OutputHints();
    }

    public void SelectStandartLevel()
    {
        SetModeParameters(true, false, _standartLevelDescription, _level.Wave.Length);
    }

    public void SelectEndlessLevel()
    {
        SetModeParameters(false, true, _endlessLevelDescription, _zeroWave);
    }

    private void OutputHints()
    {
        _questPanel.QuestPanelView.SetTextValue(_hintsText, _zeroWave);

        foreach (var animator in _animators)
        {
            animator.SetTrigger(TransitionParametr.Play.ToString());
        }
    }

    private void SetModeParameters(bool isStandart, bool isEndless, string description, int countWave)
    {
        _level.SetModeParameters(isStandart, isEndless);
        _questPanel.QuestPanelView.SetTextValue(description, countWave);
        _buttonsView.ChangeColor(isStandart, isEndless);
    }

    private void Start()
    {
        _buttonsView.Initialize(_level.Sprite, _level.NameScene);
        SetModeParameters(false, false, string.Empty, 0);
    }

    private void LoadScene(string sceneName, LoadConfig loadConfig)
    {
        switch (sceneName)
        {
            case Desert._sceneName:
                StartCoroutine(LoadScreenLevel(Desert.LoadAsync(loadConfig)));
                break;
            case Forest._sceneName:
                StartCoroutine(LoadScreenLevel(Forest.LoadAsync(loadConfig)));
                break;
            case Cave._sceneName:
                StartCoroutine(LoadScreenLevel(Cave.LoadAsync(loadConfig)));
                break;
        }
    }

    private IEnumerator LoadScreenLevel(AsyncOperation asyncOperation)
    {
        if (_load != null) yield break;

        _load = asyncOperation;
        _load.allowSceneActivation = false;
        _canvasLoader.gameObject.SetActive(true);

        while (_load.progress < 0.9f)
        {
            yield return null;
        }

        _load.allowSceneActivation = true;
        _load = null;
    }
}