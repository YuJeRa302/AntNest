using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ShopTab : MonoBehaviour
{
    public TypeItem ItemType;

    [SerializeField] private Transform _container;
    [SerializeField] private Button _openButton;
    [SerializeField] private Shop _shop;
    [SerializeField] private ItemView _itemView;
    [SerializeField] private List<ItemData> _items;

    public event Action TabOpened;

    public Transform Container => _container;
    public ItemView ItemView => _itemView;
    public List<ItemData> Items => _items;

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