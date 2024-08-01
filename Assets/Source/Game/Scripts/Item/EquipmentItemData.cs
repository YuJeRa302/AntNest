using UnityEngine;
using UnityEngine.Serialization;

namespace Assets.Source.Game.Scripts
{
    [CreateAssetMenu(fileName = "New Equipment", menuName = "Create Equipment", order = 51)]
    public class EquipmentItemData : ItemData
    {
        [Header("[Equipment Stats]")]
        [SerializeField] private int _value;
        [SerializeField] private int _level;
        [SerializeField] private Sprite _shopIcon;
        [SerializeField] private string _levelAvailableText;
        [FormerlySerializedAs("_itemObject")]
        [FormerlySerializedAs("_template")]
        [Header("[Template]")]
        [SerializeField] private Item _item;

        public int Value => _value;
        public int Level => _level;
        public string LevelAvailableText => _levelAvailableText;
        public Sprite ShopIcon => _shopIcon;
        public Item Item => _item;
    }
}