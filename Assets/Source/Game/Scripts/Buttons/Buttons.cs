using UnityEngine;
using UnityEngine.UI;
using IJunior.TypedScenes;
using System.Threading.Tasks;
using Lean.Localization;

public class Buttons : MonoBehaviour
{
    [Header("[Name Level]")]
    [SerializeField] private LeanLocalizedText _nameLevel;
    [Header("[Level Image]")]
    [SerializeField] private Image _levelImage;
    [Header("[UnlockLevel Image]")]
    [SerializeField] private Image _unlockImage;
    [Header("[Sprite]")]
    [SerializeField] private Sprite _acceptSprite;
    [SerializeField] private Sprite _cancelSprite;
    [Header("[Level Prefab]")]
    [SerializeField] private Levels _level;
    [Header("[Button]")]
    [SerializeField] private Button _buttonAccept;
    [Header("[Button Select Mode]")]
    [SerializeField] private Button _standartModeButton;
    [SerializeField] private Button _endlessModeButton;
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

    private readonly int _zeroWave = 0;

    enum TransitionParametr
    {
        Play
    }

    public bool IsLevelComplete => _level.IsComplete;
    public int Level => _level.LevelId;
    public Levels Levels => _level;

    public void SetImage()
    {
        _unlockImage.sprite = _level.IsComplete == true ? _acceptSprite : _cancelSprite;
    }

    public void SetButtonState(bool state)
    {
        _buttonAccept.interactable = state;
        _standartModeButton.interactable = state;
        _endlessModeButton.interactable = state;
    }

    public void GetConfig(LoadConfig config)
    {
        _loadConfig = config;
    }

    public void GetCanvasLoader(CanvasLoader canvas)
    {
        _canvasLoader = canvas;
    }

    public void GetQuestPanel(QuestPanel questPanel)
    {
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
        ChangeColor(isStandart, isEndless);
    }

    private void Start()
    {
        _levelImage.sprite = _level.Sprite;
        _nameLevel.TranslationName = _level.NameScene;
        SetModeParameters(false, false, string.Empty, 0);
    }

    private void LoadScene(string sceneName, LoadConfig loadConfig)
    {
        switch (sceneName)
        {
            case Desert._sceneName:
                LoadScreenLevel(Desert.LoadAsync(loadConfig));
                break;
            case Forest._sceneName:
                LoadScreenLevel(Forest.LoadAsync(loadConfig));
                break;
            case Cave._sceneName:
                LoadScreenLevel(Cave.LoadAsync(loadConfig));
                break;
        }
    }

    private async void LoadScreenLevel(AsyncOperation asyncOperation)
    {
        _canvasLoader.gameObject.SetActive(true);
        asyncOperation.allowSceneActivation = false;

        while (asyncOperation.progress < 0.9f)
        {
            await Task.Yield();
        }

        await Task.Delay(2000);
        _canvasLoader.gameObject.SetActive(false);
        asyncOperation.allowSceneActivation = true;
    }

    private void ChangeColor(bool isStandart, bool isEndless)
    {
        if (isStandart == true)
        {
            _standartModeButton.image.color = Color.yellow;
            _endlessModeButton.image.color = Color.blue;
        }
        else if (isEndless == true)
        {
            _standartModeButton.image.color = Color.blue;
            _endlessModeButton.image.color = Color.yellow;
        }
        else return;
    }
}