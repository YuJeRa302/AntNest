using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Game.Scripts
{
    public class DialogPanel : GamePanels
    {
        [SerializeField] private Button _buttonClose;

        private void Awake()
        {
            gameObject.SetActive(false);
            _buttonClose.onClick.AddListener(Close);
        }

        private void OnDestroy()
        {
            _buttonClose.onClick.RemoveListener(Close);
        }

        public void OpenPanel()
        {
            gameObject.SetActive(true);
        }

        protected override void Close()
        {
            gameObject.SetActive(false);
        }
    }
}