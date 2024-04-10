using UnityEngine;

public class PlayerConsumables : MonoBehaviour
{
    [SerializeField] private ConsumableItem _consumableItem;
    [SerializeField] private Player _player;

    private int _currentHealthPotion;

    public int CountHealthPotion => _currentHealthPotion;

    public void Initialize()
    {
        _currentHealthPotion = _player.PlayerInventory.DefaultCountHealthPotion;
        _player.PlayerView.ChangeCountPotion(_currentHealthPotion);
    }

    public void BuyItem()
    {
        _currentHealthPotion++;
        _player.PlayerView.ChangeCountPotion(_currentHealthPotion);
    }

    public int GetPotion()
    {
        _currentHealthPotion--;
        _player.PlayerView.ChangeCountPotion(_currentHealthPotion);
        return _consumableItem.Value;
    }
}