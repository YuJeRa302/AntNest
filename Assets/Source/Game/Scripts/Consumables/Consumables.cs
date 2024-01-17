using UnityEngine;

public abstract class Consumables : MonoBehaviour
{
    [Header("[Consumables View]")]
    [SerializeField] private Sprite _sprite;
    [SerializeField] private int _price;
    [Header("[Name]")]
    [SerializeField] private string _name;

    public Sprite ItemIcon => _sprite;
    public string Name => _name;
    public int Price => _price;
}