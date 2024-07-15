using Lean.Localization;
using UnityEngine;
using UnityEngine.UI;

public class GuidView : MonoBehaviour
{
    private readonly string _beginTextButton = "Begin";
    private readonly string _endTextButton = "End";
    private readonly string _endDescription = "EndDescription";

    [Header("[Level Entities]")]
    [SerializeField] private GuidObserver _guidObserver;
    [Header("[LeanLocalized Tokens]")]
    [SerializeField] private string[] _title;
    [Header("[Guid Description Text]")]
    [SerializeField] private string[] _description;
    [Header("[Guid GameObjects]")]
    [SerializeField] private GameObject[] _descriptionGameObjects;
    [SerializeField] private GameObject _settingsGameObject;
    [SerializeField] private GameObject _guidGameObject;
    [SerializeField] private GameObject _mobileInterface;
    [Header("[Mute Button Image]")]
    [SerializeField] private Sprite _muteButtonSprite;
    [SerializeField] private Sprite _unmuteButtonSprite;
    [SerializeField] private Image _soundButtonImage;
    [Header("[Guid Panel]")]
    [SerializeField] private LeanLocalizedText _titleText;
    [SerializeField] private LeanLocalizedText _guidText;
    [SerializeField] private Image _imageGuidButton;
    [SerializeField] private LeanLocalizedText _textGuidButton;

    public int DescriptionLength => _description.Length;

    private void Awake()
    {
        _guidObserver.SoundMuted += OnSoundMuted;
        _guidObserver.GuidUpdated += OnGuidUpdated;
        _guidObserver.SettingsOpened += OnOpenSettings;
        _guidObserver.SettingsClosed += OnCloseSettings;
        _guidObserver.MobileInterfaceEnabled += OnSetMobileInterface;
    }

    private void OnDisable()
    {
        _guidObserver.SoundMuted -= OnSoundMuted;
        _guidObserver.GuidUpdated -= OnGuidUpdated;
        _guidObserver.SettingsOpened -= OnOpenSettings;
        _guidObserver.SettingsClosed -= OnCloseSettings;
        _guidObserver.MobileInterfaceEnabled -= OnSetMobileInterface;
    }

    public void Initialize(LoadConfig loadConfig, int index)
    {
        _textGuidButton.TranslationName = _beginTextButton;
        _soundButtonImage.sprite = loadConfig == true ? _unmuteButtonSprite : _muteButtonSprite;
        SetNextGuid(index);
    }

    private void OnSetMobileInterface()
    {
        _mobileInterface.SetActive(true);
    }

    private void OnSoundMuted(bool state)
    {
        _soundButtonImage.sprite = state == true ? _unmuteButtonSprite : _muteButtonSprite;
    }

    private void OnGuidUpdated(int index)
    {
        SetNextGuid(index);
        UpdateDialogPanel(index);
    }

    private void SetNextGuid(int index)
    {
        _titleText.TranslationName = _title[index];
        _guidText.TranslationName = _description[index];
        _descriptionGameObjects[index].SetActive(true);
    }

    private void UpdateDialogPanel(int index)
    {
        if (index == _descriptionGameObjects.Length - 1)
        {
            _imageGuidButton.gameObject.SetActive(false);
            _textGuidButton.gameObject.SetActive(true);
            _textGuidButton.TranslationName = _endTextButton;
            _guidText.TranslationName = _endDescription;
        }

        if (_imageGuidButton.gameObject.activeSelf == false && index != _descriptionGameObjects.Length - 1)
        {
            _imageGuidButton.gameObject.SetActive(true);
            _textGuidButton.gameObject.SetActive(false);
        }
    }

    private void OnOpenSettings()
    {
        _settingsGameObject.SetActive(true);
        _guidGameObject.SetActive(false);
    }

    private void OnCloseSettings()
    {
        _settingsGameObject.SetActive(false);
        _guidGameObject.SetActive(true);
    }
}
