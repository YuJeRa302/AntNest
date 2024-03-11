using UnityEngine;

public abstract class ItemObject : MonoBehaviour
{
    [SerializeField] protected bool IsItemBuyed;

    public bool IsBayed => IsItemBuyed;

    public virtual void Buy()
    {
        IsItemBuyed = true;
    }
}