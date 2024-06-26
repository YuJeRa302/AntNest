using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable Item", menuName = "Create Consumable Item", order = 51)]
public class ConsumableItemData : ItemData
{
    [Header("[Item Parameters]")]
    [SerializeField] private int _count;
    [SerializeField] private int _value;
    [SerializeField] private float _delayButton;
    [SerializeField] private Sprite _spriteShopItem;
    [SerializeField] private TypeConsumable _typeConsumable;
    [Header("[Item GameObject]")]
    [SerializeField] private ItemGameObject _itemGameObject;
    [SerializeField] private ParticleSystem _effect;
    [SerializeField] private AudioClip _useConsumable;
    [SerializeField] private KeyCode _keyCode;

    public KeyCode KeyCode => _keyCode;
    public int Count => _count;
    public int Value => _value;
    public float DelayButton => _delayButton;
    public Sprite SpriteShopItem => _spriteShopItem;
    public ItemGameObject ItemGameObject => _itemGameObject;
    public TypeConsumable TypeConsumable => _typeConsumable;
    public ParticleSystem Effect => _effect;
    public AudioClip UseConsumable => _useConsumable;
}

[Serializable]
public class ConsumableItemState
{
    public ConsumableItemData ConsumableItemData;
}