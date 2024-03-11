using UnityEngine;

public class PlayerConsumables : MonoBehaviour
{
    [SerializeField] private ConsumableItem _consumableItem;
    [SerializeField] private Player _player;

    private int _currentHealthPotion = 2;

    public int CountHealthPotion => _currentHealthPotion;

    public void Initialize()
    {
        _player.PlayerView.ChangeCountPotion(_currentHealthPotion);
    }

    public int GetPotion()
    {
        _currentHealthPotion--;
        _player.PlayerView.ChangeCountPotion(_currentHealthPotion);
        return _consumableItem.Value;
    }

    public void TakePotion()
    {
        _currentHealthPotion++;
        _player.PlayerView.ChangeCountPotion(_currentHealthPotion);
    }
}