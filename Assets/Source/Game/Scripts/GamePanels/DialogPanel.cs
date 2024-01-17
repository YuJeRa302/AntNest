using System;
using UnityEngine;
using UnityEngine.UI;

public class DialogPanel : GamePanels
{
    [SerializeField] private Button _button;

    public Action Opened;

    private void OnEnable()
    {
        Opened += OpenPanel;
        _button.onClick.AddListener(ClosePanel);
    }

    private void OnDisable()
    {
        Opened -= OpenPanel;
        _button.onClick.RemoveListener(ClosePanel);
    }
}