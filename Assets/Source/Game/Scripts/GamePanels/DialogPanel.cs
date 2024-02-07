using UnityEngine;
using UnityEngine.UI;

public class DialogPanel : GamePanels
{
    [SerializeField] private Button _button;

    private void Awake()
    {
        gameObject.SetActive(false);
        _button.onClick.AddListener(ClosePanel);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(ClosePanel);
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }
}