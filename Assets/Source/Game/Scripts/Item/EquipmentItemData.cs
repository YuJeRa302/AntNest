using System;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Create Equipment", order = 51)]
public class EquipmentItemData : ItemData
{
    [Header("[Equipment Stats]")]
    [SerializeField] private int _value;
    [SerializeField] private int _level;
    [SerializeField] private Sprite _shopIcon;
    [FormerlySerializedAs("_itemObject")]
    [FormerlySerializedAs("_template")]
    [Header("[Template]")]
    [SerializeField] private ItemGameObject _itemGameObject;

    public int Value => _value;
    public int Level => _level;
    public Sprite ShopIcon => _shopIcon;
    public ItemGameObject ItemGameObject => _itemGameObject;
}

[Serializable]
public class ItemState
{
    public EquipmentItemData ItemData;
}

[Serializable]
public class EquipmentItemState : ItemState
{
    public bool IsEquipped;
    public bool IsBuyed;
    public int Level;
}
