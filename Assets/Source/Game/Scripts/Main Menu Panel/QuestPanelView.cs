using UnityEngine;
using UnityEngine.UI;
using Lean.Localization;

public class QuestPanelView : MonoBehaviour
{
    [Header("[Text]")]
    [SerializeField] private Text _playerGold;
    [SerializeField] private Text _playerLevel;
    [SerializeField] private LeanLocalizedText _description;
    [SerializeField] private Text _waveCount;
    [Header("[Endless Image]")]
    [SerializeField] private Image _endlessImage;
    [Header("[ScrollRect]")]
    [SerializeField] private ScrollRect _scroll;
    [Header("[DotView]")]
    [SerializeField] private DotView _dotView;

    public void Initialize(int coins, int playerLevel)
    {
        _playerGold.text = coins.ToString();
        _playerLevel.text = playerLevel.ToString();
        _dotView.SetScrollRect(_scroll);
    }

    public void SetTextValue(string value, int waveCount)
    {
        _description.TranslationName = value;

        if (waveCount > 0)
        {
            SetActiveState(false, true);
            _waveCount.text = waveCount.ToString();
        }
        else SetActiveState(true, false);
    }

    private void SetActiveState(bool imageState, bool textState)
    {
        _endlessImage.gameObject.SetActive(imageState);
        _waveCount.gameObject.SetActive(textState);
    }
}