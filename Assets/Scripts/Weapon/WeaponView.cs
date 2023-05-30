using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class WeaponView : MonoBehaviour
{
    [SerializeField] private TMP_Text _nameItem;
    [SerializeField] private TMP_Text _priceItem;
    [SerializeField] private Image _iconItem;
    [SerializeField] private Image _coinIcon;
    [SerializeField] private Button _buyButton;
    [SerializeField] private Image _isBayed;

    private Weapon _weapon;

    public event UnityAction<Weapon, WeaponView> BuyButtonClick;

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

    public void Render(Weapon weapon)
    {
        _weapon = weapon;
        _nameItem.text = weapon.Name;
        _priceItem.text = weapon.Price.ToString();
        _iconItem.sprite = weapon.ItemIcon;
    }

    public void TryLockItem()
    {
        if (_weapon.IsBayed)
        {
            _buyButton.gameObject.SetActive(false);
            _isBayed.gameObject.SetActive(true);
        }
    }

    public void OnButtonClick()
    {
        BuyButtonClick?.Invoke(_weapon, this);
    }
}
