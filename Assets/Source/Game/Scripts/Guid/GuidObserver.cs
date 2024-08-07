using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Source.Game.Scripts
{
    public class GuidObserver : MonoBehaviour
    {
        private readonly string _menuScene = "Menu";
        private readonly float _maxLoadProgressValue = 0.9f;

        [Header("[Level Entities]")]
        [SerializeField] private GuidSoundController _guidSoundController;
        [SerializeField] private GuidView _guidView;
        [SerializeField] private SaveProgress _saveProgress;
        [SerializeField] private LoadConfig _loadConfig;
        [SerializeField] private ObjectDisabler _objectDisabler;
        [Header("[Buttonsl]")]
        [SerializeField] private Button _guidButton;
        [SerializeField] private Button _openSettings;
        [SerializeField] private Button _closeSettings;
        [SerializeField] private Button _soundButton;

        private bool _isMuteSound = false;
        private int _guidIndex = 0;
        private AsyncOperation _load;

        public event Action<bool> SoundMuted;
        public event Action<int> GuidUpdated;
        public event Action SettingsOpened;
        public event Action SettingsClosed;
        public event Action MobileInterfaceEnabled;

        private void Awake()
        {
            if (_loadConfig.TypeDevice == TypeDevice.Mobile)
                MobileInterfaceEnabled?.Invoke();

            _guidSoundController.Initialize(_loadConfig);
            _guidView.Initialize(_loadConfig, _guidIndex);
            _soundButton.onClick.AddListener(MuteSound);
            _openSettings.onClick.AddListener(OpenSettings);
            _closeSettings.onClick.AddListener(CloseSettings);
            _guidButton.onClick.AddListener(GuidUpdate);
        }

        private void OnDestroy()
        {
            _soundButton.onClick.RemoveListener(MuteSound);
            _guidButton.onClick.RemoveListener(GuidUpdate);
            _openSettings.onClick.RemoveListener(OpenSettings);
            _closeSettings.onClick.RemoveListener(CloseSettings);
        }

        private void MuteSound()
        {
            _isMuteSound = _loadConfig.IsSoundOn != true;
            _loadConfig.SetSoundState(_isMuteSound);
            SoundMuted?.Invoke(_isMuteSound);
        }

        private void GuidUpdate()
        {
            if (_guidIndex < _guidView.DescriptionLength - 1)
                _guidIndex++;
            else
                StartCoroutine(EndGuid());

            GuidUpdated?.Invoke(_guidIndex);
        }

        private IEnumerator EndGuid()
        {
            _loadConfig.SetSessionState(false);
            yield return _saveProgress.SaveApplicationParameters(_loadConfig);
            StartCoroutine(LoadScreenLevel(SceneManager.LoadSceneAsync(_menuScene)));
        }

        private void OpenSettings()
        {
            SettingsOpened?.Invoke();
        }

        private void CloseSettings()
        {
            SettingsClosed?.Invoke();
        }

        private IEnumerator LoadScreenLevel(AsyncOperation asyncOperation)
        {
            if (_load != null)
                yield break;

            _objectDisabler.gameObject.SetActive(true);
            _load = asyncOperation;
            _load.allowSceneActivation = false;

            while (_load.progress < _maxLoadProgressValue)
            {
                yield return null;
            }

            _load.allowSceneActivation = true;
            _load = null;
        }
    }
}