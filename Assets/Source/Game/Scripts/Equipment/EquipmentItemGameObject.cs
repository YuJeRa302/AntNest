using System;

public abstract class EquipmentItemGameObject : ItemGameObject
{
    public event Action<bool> EquipmentChanged;

    public void SetActive(bool value)
    {
        gameObject.SetActive(value);
        EquipmentChanged?.Invoke(value);
    }
}