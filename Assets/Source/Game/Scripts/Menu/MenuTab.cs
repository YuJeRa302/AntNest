using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Game.Scripts
{
    public class MenuTab : MonoBehaviour
    {
        [SerializeField] protected MenuPanel MenuPanel;
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _openButton;

        protected virtual void Awake()
        {
            gameObject.SetActive(false);
            _openButton.onClick.AddListener(OpenTab);
            _closeButton.onClick.AddListener(CloseTab);
        }

        protected virtual void OnDestroy()
        {
            _openButton.onClick.RemoveListener(OpenTab);
            _closeButton.onClick.RemoveListener(CloseTab);
        }

        protected virtual void OpenTab()
        {
            gameObject.SetActive(true);
            MenuPanel.gameObject.SetActive(false);
        }

        protected virtual void CloseTab()
        {
            gameObject.SetActive(false);
            MenuPanel.gameObject.SetActive(true);
        }
    }
}