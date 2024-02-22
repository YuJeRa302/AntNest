using System.Collections.Generic;
using UnityEngine;

public class PlayerConsumables : MonoBehaviour
{
    [SerializeField] private Transform _consumablesTransform;
    [SerializeField] private List<Consumables> _consumables;
    [SerializeField] private Player _player;

    private int _currentHealthPotion = 2;

    public int CountHealthPotion => _currentHealthPotion;

    private void OnDestroy()
    {
        foreach (var consumable in _consumables)
        {
            consumable.Item.ItemBuyed -= TakePotion;
        }
    }

    public int GetPotion()
    {
        _currentHealthPotion--;
        _player.PlayerView.ChangeCountPotion(_currentHealthPotion);
        return _consumables[0].Item.Value;
    }

    public void Initialize()
    {
        AddItemToList();
        AddListener();
        _player.PlayerView.ChangeCountPotion(_currentHealthPotion);
    }

    public List<Consumables> GetListConsumables()
    {
        return _consumables;
    }

    private void TakePotion()
    {
        _currentHealthPotion++;
        _player.PlayerView.ChangeCountPotion(_currentHealthPotion);
    }

    private void AddItemToList()
    {
        for (int i = 0; i < _consumablesTransform.childCount; i++)
        {
            _consumables.Add(_consumablesTransform.GetChild(i).GetComponent<Consumables>());
        }
    }

    private void AddListener()
    {
        foreach (var consumable in _consumables)
        {
            consumable.Item.ItemBuyed += TakePotion;
        }
    }
}