using UnityEngine;
using UnityEngine.UI;
using Lean.Localization;

public class QuestPanelView : MonoBehaviour
{
    [Header("[Gold Player]")]
    [SerializeField] private Text _playerGold;
    [Header("[Level Player]")]
    [SerializeField] private Text _playerLevel;
    [Header("[Description]")]
    [SerializeField] private LeanLocalizedText _description;
    [Header("[Wave]")]
    [SerializeField] private Text _waveCount;
    [Header("[Endless Image]")]
    [SerializeField] private Image _endlessImage;

    public void Initialize(int coins, int playerLevel)
    {
        _playerGold.text = coins.ToString();
        _playerLevel.text = playerLevel.ToString();
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