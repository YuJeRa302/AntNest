using System;
using UnityEngine;

public class PlayerConsumables : MonoBehaviour
{
    [SerializeField] private GrenadeButtonGameObject _grenadeButton;
    [SerializeField] private PotionButtonGameObject _potionButton;
    [SerializeField] private MineButtonGameObject _mineButtonGameObject;
    [SerializeField] private Player _player;
    [SerializeField] private Transform _throwPoint;

    public event Action<TypeConsumable> ConsumableBuyed;

    public void Initialize()
    {
        foreach (var item in _player.PlayerInventory.ListConsumables)
        {
            if (item.ConsumableItemData.TypeConsumable == TypeConsumable.HealthPotion)
                _potionButton.Initialize(item, _player.transform, _player);
            else if (item.ConsumableItemData.TypeConsumable == TypeConsumable.LandMine)
                _mineButtonGameObject.Initialize(item, _player.transform, _player);
            else
                _grenadeButton.Initialize(item, _throwPoint, _player);
        }
    }

    public void BuyItem(ConsumableItemState consumableItemState)
    {
        ConsumableBuyed?.Invoke(consumableItemState.ConsumableItemData.TypeConsumable);
    }
}