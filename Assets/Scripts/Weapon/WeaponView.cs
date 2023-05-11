using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WeaponView : MonoBehaviour
{
    [SerializeField] private Text _nameItem;
    [SerializeField] private Text _priceItem;
    [SerializeField] private Image _iconItem;
    [SerializeField] private Image _coinIcon;
    [SerializeField] private Button _sellButton;
    [SerializeField] private Image _isBayed;

    private Weapon _weapon;

    public event UnityAction<Weapon, WeaponView> SellButtonClick;

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
            _sellButton.gameObject.SetActive(false);
            _isBayed.gameObject.SetActive(true);
        }
    }

    public void OnButtonClick()
    {
        SellButtonClick?.Invoke(_weapon, this);
    }
}
