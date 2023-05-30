using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AbilityView : MonoBehaviour
{
    [SerializeField] private Text _nameItem;
    [SerializeField] private Text _priceItem;
    [SerializeField] private Image _iconAbility;
    [SerializeField] private Button _sellButton;
    [SerializeField] private Image _isBayed;

    private Ability _ability;

    public event UnityAction<Ability, AbilityView> SellButtonClick;

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

    public void Render(Ability ability)
    {
        _ability = ability;
        _nameItem.text = ability.Name;
        _priceItem.text = ability.Price.ToString();
        _iconAbility.sprite = ability.AbilityIcon;
    }

    public void TryLockItem()
    {
        if (_ability.IsBayed)
        {
            _sellButton.gameObject.SetActive(false);
            _isBayed.gameObject.SetActive(true);
        }
    }

    public void OnButtonClick()
    {
        SellButtonClick?.Invoke(_ability, this);
    }
}