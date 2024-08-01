using UnityEngine;

namespace Assets.Source.Game.Scripts
{
    public class PlayerInterfaceView : MonoBehaviour
    {
        [SerializeField] private GameObject _mobileInterface;
        [SerializeField] private GameObject[] _keyCodes;

        public void SetMobileInterface()
        {
            _mobileInterface.SetActive(true);
        }

        public void SetDesktopInterface()
        {
            foreach (var keyCode in _keyCodes)
                keyCode.SetActive(true);
        }
    }
}
