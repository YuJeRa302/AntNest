using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class ShopTab : MonoBehaviour
{
    public TypeItem ItemType;

    [SerializeField] protected GameObject ItemContainer;

    protected DialogPanel DialogPanel;

    [SerializeField] private Button _openButton;
    [SerializeField] private Shop _shop;

    public event Action TabOpened;
    public event Action PlayerResourceUpdated;

    protected void Awake()
    {
        _openButton.onClick.AddListener(OpenTab);
    }

    protected void OnDestroy()
    {
        _openButton.onClick.RemoveListener(OpenTab);
    }

    public void Initialize(DialogPanel dialogPanel)
    {
        DialogPanel = dialogPanel;
    }

    protected virtual void OpenTab()
    {
        TabOpened?.Invoke();
        gameObject.SetActive(true);
    }

    protected virtual void UpdatePlayerResourceValue()
    {
        PlayerResourceUpdated.Invoke();
    }
}