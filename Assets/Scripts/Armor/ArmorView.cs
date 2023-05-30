using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class ArmorView : MonoBehaviour
{
    [SerializeField] private TMP_Text _nameItem;
    [SerializeField] private TMP_Text _priceItem;
    [SerializeField] private Image _iconItem;
    [SerializeField] private Image _coinIcon;
    [SerializeField] private Button _buyButton;
    [SerializeField] private Image _isBayed;

    private Armor _armor;

    public event UnityAction<Armor, ArmorView> BuyButtonClick;

    private void OnEnable()
    {
        _buyButton.onClick.AddListener(OnButtonClick);
        _buyButton.onClick.AddListener(TryLockItem);
    }

    private void OnDisable()
    {
        _buyButton.onClick.RemoveListener(OnButtonClick);
        _buyButton.onClick.RemoveListener(TryLockItem);
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
            _buyButton.gameObject.SetActive(false);
            _isBayed.gameObject.SetActive(true);
        }
    }

    public void OnButtonClick()
    {
        BuyButtonClick?.Invoke(_armor, this);
    }
}
