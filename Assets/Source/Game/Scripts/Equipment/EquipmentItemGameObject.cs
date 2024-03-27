using System;

public abstract class EquipmentItemGameObject : ItemGameObject
{
    public event Action<bool> ActiveStateChanged;

    public void SetActive(bool value)
    {
        gameObject.SetActive(value);
        ActiveStateChanged?.Invoke(value);
    }
}