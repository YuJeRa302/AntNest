using UnityEngine;

public class Armor : MonoBehaviour
{
    [SerializeField] private ArmorItem _armorItem;

    public ArmorItem Item => _armorItem;
    public int ArmorValue => _armorItem.ItemArmor;
    public Sprite ItemIcon => _armorItem.ItemIcon;
    public string Name => _armorItem.Name;
    public int Price => _armorItem.Price;
    public bool IsBayed => _armorItem.IsBayed;
    public int Level => _armorItem.ArmorLevel;

    private void OnEnable()
    {
        _armorItem.OnChangeState += SetState;
    }

    private void OnDisable()
    {
        _armorItem.OnChangeState -= SetState;
    }

    private void SetState(bool state)
    {
        gameObject.SetActive(state);
    }
}