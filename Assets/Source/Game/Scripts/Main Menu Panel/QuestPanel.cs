using System.Collections.Generic;
using System.Collections;
using IJunior.TypedScenes;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Assets.Source.Game.Scripts
{
    public class QuestPanel : MenuTab
    {
        private readonly string _guidScene = "Guid";

        [SerializeField] private QuestPanelView _questPanelView;
        [SerializeField] private ObjectDisabler _objectDisabler;
        [SerializeField] private LoadConfig _loadConfig;
        [SerializeField] private GameObject _buttonsContainer;
        [SerializeField] private DefaultLevelState _defaultLevelState;
        [SerializeField] private LevelDataView _levelDataView;
        [Header("[Dialog Panel Buttons]")]
        [SerializeField] private Button _openGuidButton;
        [SerializeField] private Button _closeDialogPanelButton;

        private AsyncOperation _load;
        private List<LevelDataView> _levelDataViews = new ();
        private DefaultLevelState _levelState;

        protected override void Awake()
        {
            base.Awake();
            _openGuidButton.onClick.AddListener(LoadGuidLevel);
            _closeDialogPanelButton.onClick.AddListener(CloseDialogPanel);
            _questPanelView.CloseDialogPanel();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _openGuidButton.onClick.RemoveListener(LoadGuidLevel);
            _closeDialogPanelButton.onClick.RemoveListener(CloseDialogPanel);
        }

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
            if (_loadConfig.IsFirstSession == true)
                _questPanelView.OpenDialogPanel();

            _levelState = _defaultLevelState;
            _loadConfig.SetCountLevels(_levelState.LevelDataState.Count);
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
            _objectDisabler.gameObject.SetActive(true);

            while (_load.progress < 0.9f)
            {
                yield return null;
            }

            _load.allowSceneActivation = true;
            _load = null;
        }

        private void CloseDialogPanel()
        {
            _questPanelView.CloseDialogPanel();
            _loadConfig.SetSessionState(false);
        }

        private void LoadGuidLevel()
        {
            StartCoroutine(LoadScreenLevel(SceneManager.LoadSceneAsync(_guidScene)));
        }
    }
}