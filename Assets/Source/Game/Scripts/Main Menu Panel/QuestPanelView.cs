using UnityEngine;
using UnityEngine.UI;
using Lean.Localization;

namespace Assets.Source.Game.Scripts
{
    public class QuestPanelView : MonoBehaviour
    {
        [SerializeField] private Text _playerGold;
        [SerializeField] private Text _playerLevel;
        [SerializeField] private LeanLocalizedText _description;
        [SerializeField] private Text _waveCount;
        [SerializeField] private GameObject _dialogPanel;
        [SerializeField] private Image _endlessImage;
        [SerializeField] private ScrollRect _scroll;
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
        }

        public void OpenDialogPanel()
        {
            _dialogPanel.SetActive(true);
            _scroll.gameObject.SetActive(false);
        }

        public void CloseDialogPanel()
        {
            _dialogPanel.SetActive(false);
            _scroll.gameObject.SetActive(true);
        }

        private void SetActiveState(bool imageState, bool textState)
        {
            _endlessImage.gameObject.SetActive(imageState);
            _waveCount.gameObject.SetActive(textState);
        }
    }
}