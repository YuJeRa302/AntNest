using UnityEngine;
using Lean.Localization;
using UnityEngine.UI;
using System;

public class LevelDataView : MonoBehaviour
{
    private readonly int _zeroWave = 0;
    private readonly int _levelIndexShift = 1;
    private readonly int _firstLevelIndex = 0;
    private readonly Color _defaultColor = Color.blue;
    private readonly Color _acceptColor = Color.yellow;

    [Header("[Config]")]
    [SerializeField] private LoadConfig _loadConfig;
    [SerializeField] private LeanLocalizedText _nameLevel;
    [SerializeField] private LeanLocalizedText _levelAvailable;
    [SerializeField] private LeanLocalToken _levelToken;
    [SerializeField] private Text _levelAvailableText;
    [Header("[Buttons Animators]")]
    [SerializeField] private Animator[] _animators;
    [Header("[Image]")]
    [SerializeField] private Image _levelImage;
    [SerializeField] private Image _levelCompleteImage;
    [SerializeField] private Image _hardDifficultImage;
    [Header("[Buttons]")]
    [SerializeField] private Button _buttonAccept;
    [SerializeField] private Button _standartModeButton;
    [SerializeField] private Button _endlessModeButton;
    [SerializeField] private GameObject _levelAvailableGameObject;

    private LevelDataState _levelDataState;
    private string _standartLevelDescription;
    private string _endlessLevelDescription;
    private string _hintsText;

    public event Action<string, int> LevelModChanged;
    public event Action<string, int> HintsShowed;
    public event Action<string, LoadConfig> LevelLoading;

    enum TransitionParametr
    {
        Play
    }

    private void OnDestroy()
    {
        _standartModeButton.onClick.RemoveListener(SelectStandartLevel);
        _endlessModeButton.onClick.RemoveListener(SelectEndlessLevel);
        _buttonAccept.onClick.RemoveListener(LoadLevel);
    }

    public void Initialize(LevelDataState levelDataState, LoadConfig config)
    {
        AddListener();
        Fill(levelDataState, config);
        SetModeParameters(false, false, string.Empty, 0);
        LoadCompletePlayerLevels(levelDataState);
        CheckLevelState(levelDataState);
    }

    private void AddListener()
    {
        _standartModeButton.onClick.AddListener(SelectStandartLevel);
        _endlessModeButton.onClick.AddListener(SelectEndlessLevel);
        _buttonAccept.onClick.AddListener(LoadLevel);
    }

    private void Fill(LevelDataState levelDataState, LoadConfig config)
    {
        if (levelDataState.LevelData.HardDifficult == true)
            _hardDifficultImage.gameObject.SetActive(true);

        _loadConfig = config;
        _levelDataState = levelDataState;
        _levelImage.sprite = levelDataState.LevelData.LevelIcon;
        _nameLevel.TranslationName = levelDataState.LevelData.NameScene;
        _levelToken.SetValue(levelDataState.LevelData.LevelId.ToString());
        _levelAvailable.TranslationName = levelDataState.LevelData.LevelAvailable;
        _standartLevelDescription = levelDataState.LevelData.StandartLevelDescription;
        _endlessLevelDescription = levelDataState.LevelData.EndlessLevelDescription;
        _hintsText = levelDataState.LevelData.HintsText;
    }

    private void LoadCompletePlayerLevels(LevelDataState levelDataState)
    {
        bool levelState = _loadConfig.LevelsComplete.Length > _firstLevelIndex ? _loadConfig.LevelsComplete[levelDataState.LevelData.LevelId] : false;
        levelDataState.IsComplete = levelState;
        _levelCompleteImage.gameObject.SetActive(levelState);
        SetLevelState(levelState);

        if (levelDataState.LevelData.LevelId == _firstLevelIndex)
            SetLevelState(true);
    }

    private void CheckLevelState(LevelDataState levelDataState)
    {
        if (levelDataState.LevelData.LevelId == _firstLevelIndex)
            return;

        if (_loadConfig.LevelsComplete.Length > _firstLevelIndex)
            SetLevelState(_loadConfig.LevelsComplete[levelDataState.LevelData.LevelId - _levelIndexShift]);
    }

    private void SetLevelState(bool isLevelComplete)
    {
        _buttonAccept.gameObject.SetActive(isLevelComplete);
        _standartModeButton.gameObject.SetActive(isLevelComplete);
        _endlessModeButton.gameObject.SetActive(isLevelComplete);
        _levelAvailableGameObject.SetActive(!isLevelComplete);
    }

    private void SetButtonsColor(Color colorStandartButton, Color colorEndlessButton)
    {
        _standartModeButton.image.color = colorStandartButton;
        _endlessModeButton.image.color = colorEndlessButton;
    }

    private void LoadLevel()
    {
        if (_levelDataState.IsStandart != false || _levelDataState.IsEndless != false)
        {
            _loadConfig.LoadLevelData(_levelDataState);
            LevelLoading.Invoke(_levelDataState.LevelData.NameScene, _loadConfig);
        }
        else
            ShowHints();
    }

    private void SelectStandartLevel()
    {
        SetModeParameters(true, false, _standartLevelDescription, _levelDataState.LevelData.WaveData.Count);
        LevelModChanged.Invoke(_standartLevelDescription, _levelDataState.LevelData.WaveData.Count);
    }

    private void SelectEndlessLevel()
    {
        SetModeParameters(false, true, _endlessLevelDescription, _zeroWave);
        LevelModChanged.Invoke(_endlessLevelDescription, _zeroWave);
    }

    private void ShowHints()
    {
        HintsShowed.Invoke(_hintsText, _zeroWave);

        foreach (var animator in _animators)
        {
            animator.SetTrigger(TransitionParametr.Play.ToString());
        }
    }

    private void SetModeParameters(bool isStandart, bool isEndless, string description, int countWave)
    {
        _levelDataState.IsStandart = isStandart;
        _levelDataState.IsEndless = isEndless;
        ChangeColor(isStandart, isEndless);
    }

    private void ChangeColor(bool isStandart, bool isEndless)
    {
        if (isStandart == true)
            SetButtonsColor(_acceptColor, _defaultColor);
        else if (isEndless == true)
            SetButtonsColor(_defaultColor, _acceptColor);
        else
            SetButtonsColor(_defaultColor, _defaultColor);
    }
}
