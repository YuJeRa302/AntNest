using System.Collections.Generic;
using IJunior.TypedScenes;
using System.Collections;
using UnityEngine;
using System;

public class QuestPanel : MenuTab
{
    [SerializeField] private List<Buttons> _buttons;
    [SerializeField] private QuestPanelView _questPanelView;
    [SerializeField] private CanvasLoader _canvasLoader;
    [Header("[View]")]
    [SerializeField] private LoadConfig _loadConfig;
    [Header("[Containers]")]
    [SerializeField] private GameObject _buttonsContainer;
    [Header("[Default Level Data]")]
    [SerializeField] private DefaultLevelState _defaultLevelState;
    [Header("[View]")]
    [SerializeField] private LevelDataView _levelDataView;

    private AsyncOperation _load;
    private List<LevelDataView> _levelDataViews = new();
    private DefaultLevelState _levelState;

    public QuestPanelView QuestPanelView => _questPanelView;

    protected override void OpenTab()
    {
        base.OpenTab();
        Initialize();
    }

    protected override void CloseTab()
    {
        base.CloseTab();
        Clear();
    }

    private void Initialize()
    {
        _levelState = _defaultLevelState;
        _questPanelView.Initialize(_loadConfig.PlayerCoins, _loadConfig.PlayerLevel);
        Fill();
    }

    private void Fill()
    {
        foreach (LevelDataState levelDataState in _levelState.LevelDataState)
        {
            LevelDataView view = Instantiate(_levelDataView, _buttonsContainer.transform);
            _levelDataViews.Add(view);
            view.Initialize(levelDataState, _loadConfig);
            view.LevelModChanged += _questPanelView.SetTextValue;
            view.HintsShowed += _questPanelView.SetTextValue;
            view.LevelLoading += LoadScene;
        }
    }

    private void Clear()
    {
        foreach (LevelDataView view in _levelDataViews)
        {
            view.LevelModChanged -= _questPanelView.SetTextValue;
            view.HintsShowed -= _questPanelView.SetTextValue;
            view.LevelLoading -= LoadScene;
            Destroy(view.gameObject);
        }

        _levelDataViews.Clear();
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
        if (_load != null)
            yield break;

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

[Serializable]
public struct DefaultLevelState
{
    public List<LevelDataState> LevelDataState;
}