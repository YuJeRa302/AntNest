using Lean.Localization;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsView : MonoBehaviour
{
    private readonly Color _defaultColor = Color.blue;
    private readonly Color _acceptColor = Color.yellow;

    [Header("[Name Level]")]
    [SerializeField] private LeanLocalizedText _nameLevel;
    [Header("[Level Image]")]
    [SerializeField] private Image _levelImage;
    [Header("[UnlockLevel Image]")]
    [SerializeField] private Image _unlockImage;
    [Header("[Sprite]")]
    [SerializeField] private Sprite _acceptSprite;
    [SerializeField] private Sprite _cancelSprite;
    [Header("[Button Accept]")]
    [SerializeField] private Button _buttonAccept;
    [Header("[Button Select Mode]")]
    [SerializeField] private Button _standartModeButton;
    [SerializeField] private Button _endlessModeButton;

    public void Initialize(Sprite levelSprite, string levelName)
    {
        _levelImage.sprite = levelSprite;
        _nameLevel.TranslationName = levelName;
    }

    public void SetImage(bool isCompleteLevel)
    {
        _unlockImage.sprite = isCompleteLevel == true ? _acceptSprite : _cancelSprite;
    }

    public void SetButtonState(bool state)
    {
        _buttonAccept.interactable = state;
        _standartModeButton.interactable = state;
        _endlessModeButton.interactable = state;
    }

    public void ChangeColor(bool isStandart, bool isEndless)
    {
        if (isStandart == true) SetColor(_acceptColor, _defaultColor);
        else if (isEndless == true) SetColor(_defaultColor, _acceptColor);
    }

    private void SetColor(Color colorStandartButton, Color colorEndlessButton)
    {
        _standartModeButton.image.color = colorStandartButton;
        _endlessModeButton.image.color = colorEndlessButton;
    }
}
