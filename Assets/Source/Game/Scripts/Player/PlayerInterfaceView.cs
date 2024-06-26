using UnityEngine;

public class PlayerInterfaceView : MonoBehaviour
{
    [Header("[Mobile Interface GameObject]")]
    [SerializeField] private GameObject _mobileInterface;
    [Header("[Desktop Interface GameObject]")]
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
