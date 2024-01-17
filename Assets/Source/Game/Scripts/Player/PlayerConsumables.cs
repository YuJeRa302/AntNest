using System.Collections.Generic;
using UnityEngine;

public class PlayerConsumables : MonoBehaviour
{
    [Header("[Transform]")]
    [SerializeField] private Transform _consumablesTransform;
    [Header("[Consumables]")]
    [SerializeField] private List<Consumables> _consumables;
    [Header("[Player]")]
    [SerializeField] private Player _player;

    private int _currentHealthPotion = 2;

    public int CountHealthPotion => _currentHealthPotion;

    public void BuyConsumables(int value)
    {
        _player.Wallet.Buy(value);
        TakePotion();
    }

    public void GetPotion()
    {
        _currentHealthPotion--;
        _player.PlayerView.ChangeCountPotion(_currentHealthPotion);
    }

    public void Initialize()
    {
        AddItemToList();
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
}