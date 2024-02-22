using UnityEngine;
using UnityEngine.UI;

public class DialogPanel : GamePanels
{
    [SerializeField] private Button _button;

    private void Awake()
    {
        gameObject.SetActive(false);
        _button.onClick.AddListener(Close);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(Close);
    }

    public void OpenPanel()
    {
        gameObject.SetActive(true);
    }
}