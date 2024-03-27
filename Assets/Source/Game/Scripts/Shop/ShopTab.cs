using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class ShopTab : MonoBehaviour
{
    public TypeItem ItemType;

    [SerializeField] protected Transform _container;
    [SerializeField] private Button _openButton;

    public event Action TabOpened;

    protected void Awake()
    {
        _openButton.onClick.AddListener(OpenTab);
    }

    protected void OnDestroy()
    {
        _openButton.onClick.RemoveListener(OpenTab);
    }

    protected virtual void OpenTab()
    {
        TabOpened?.Invoke();
        gameObject.SetActive(true);
    }
}