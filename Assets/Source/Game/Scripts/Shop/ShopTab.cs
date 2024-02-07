using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class ShopTab : MonoBehaviour
{
    public TypeItem ItemType;

    [SerializeField] private Transform _container;
    [SerializeField] private Button _button;
    [SerializeField] private Shop _shop;
    [SerializeField] private ItemView _itemView;

    public event Action PanelOpened;

    public Transform Container => _container;
    public ItemView ItemView => _itemView;

    private void OnEnable()
    {
        _button.onClick.AddListener(OpenPanel);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OpenPanel);
    }

    private void OpenPanel()
    {
        gameObject.SetActive(true);
        PanelOpened?.Invoke();
    }
}