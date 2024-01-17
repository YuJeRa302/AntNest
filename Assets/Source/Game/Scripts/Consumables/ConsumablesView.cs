using Lean.Localization;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ConsumablesView : MonoBehaviour
{
    [Header("[Weapon View]")]
    [SerializeField] private Text _priceItem;
    [SerializeField] private Image _iconItem;
    [SerializeField] private Image _coinIcon;
    [SerializeField] private Button _buyButton;
    [Header("[Name]")]
    [SerializeField] private LeanLocalizedText _name;

    private Consumables _consumables;

    public event Action<Consumables> BuyButtonClick;

    public void Render(Consumables consumables)
    {
        _consumables = consumables;
        _name.TranslationName = _consumables.Name;
        _priceItem.text = _consumables.Price.ToString();
        _iconItem.sprite = _consumables.ItemIcon;
    }

    public void OnButtonClick()
    {
        BuyButtonClick?.Invoke(_consumables);
    }

    private void OnEnable()
    {
        _buyButton.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _buyButton.onClick.RemoveListener(OnButtonClick);
    }
}
