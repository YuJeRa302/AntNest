using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ArmorView : MonoBehaviour
{
    [SerializeField] private Text _nameItem;
    [SerializeField] private Text _priceItem;
    [SerializeField] private Image _iconItem;
    [SerializeField] private Image _coinIcon;
    [SerializeField] private Button _sellButton;
    [SerializeField] private Image _isBayed;

    private Armor _armor;

    public event UnityAction<Armor, ArmorView> SellButtonClick;

    private void OnEnable()
    {
        _sellButton.onClick.AddListener(OnButtonClick);
        _sellButton.onClick.AddListener(TryLockItem);
    }

    private void OnDisable()
    {
        _sellButton.onClick.RemoveListener(OnButtonClick);
        _sellButton.onClick.RemoveListener(TryLockItem);
    }

    public void Render(Armor armor)
    {
        _armor = armor;
        _nameItem.text = armor.Name;
        _priceItem.text = armor.Price.ToString();
        _iconItem.sprite = armor.ItemIcon;
    }

    public void TryLockItem()
    {
        if (_armor.IsBayed)
        {
            _sellButton.gameObject.SetActive(false);
            _isBayed.gameObject.SetActive(true);
        }
    }

    public void OnButtonClick()
    {
        SellButtonClick?.Invoke(_armor, this);
    }
}
