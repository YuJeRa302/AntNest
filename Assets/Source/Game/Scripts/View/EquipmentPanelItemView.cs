using System;
using Lean.Localization;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Game.Scripts
{
    public class EquipmentPanelItemView : MonoBehaviour
    {
        [SerializeField] private Text _itemPrice;
        [SerializeField] private Image _itemIcon;
        [SerializeField] private Button _buyButton;
        [SerializeField] private LeanLocalizedText _itemName;
        [SerializeField] private LeanLocalizedText _levelAvailableLeanLocalized;
        [SerializeField] private LeanLocalToken _levelToken;
        [SerializeField] private Text _levelAvailableText;
        [SerializeField] private Button[] _changeButtons;
        [SerializeField] private Image _isBayed;
        [SerializeField] private Image _shopIcon;
        [SerializeField] private Image _isCurrentWeapon;
        [SerializeField] private Text _itemValue;
        [SerializeField] private Text _levelItem;
        [SerializeField] private GameObject _levelAvailableGameObject;

        private EquipmentItemState _equipmentItemState;

        public event Action<EquipmentPanelItemView> BuyButtonClicked;
        public event Action<EquipmentPanelItemView> CurrentEquipmentChanged;

        public EquipmentItemState EquipmentItemState => _equipmentItemState;

        private void OnDestroy()
        {
            _buyButton.onClick.RemoveListener(OnButtonClick);
            _buyButton.onClick.RemoveListener(TryLockItem);

            foreach (var button in _changeButtons)
                button.onClick.RemoveListener(OnChangeCurrentEquipment);
        }

        public void Initialize(EquipmentItemState itemState, Player player)
        {
            _equipmentItemState = itemState;
            AddListener();
            Fill(_equipmentItemState);
            TryUnlockBuyButton(player, _equipmentItemState);
            TryLockItem();
            TrySetCurrentEquipment(_equipmentItemState);
            CheckEquipState();
        }

        private void Fill(EquipmentItemState equipmentItemState)
        {
            _itemName.TranslationName = equipmentItemState.ItemData.Name;
            _levelToken.SetValue(equipmentItemState.ItemData.Level.ToString());
            _levelAvailableLeanLocalized.TranslationName = equipmentItemState.ItemData.LevelAvailableText;
            _itemPrice.text = equipmentItemState.ItemData.Price.ToString();
            _itemIcon.sprite = equipmentItemState.ItemData.ItemIcon;
            _itemValue.text = equipmentItemState.ItemData.Value.ToString();
            _levelItem.text = equipmentItemState.ItemData.Level.ToString();
            _shopIcon.sprite = equipmentItemState.ItemData.ShopIcon;
            _buyButton.gameObject.SetActive(false);
            _levelAvailableGameObject.SetActive(false);
        }

        private void AddListener()
        {
            _buyButton.onClick.AddListener(OnButtonClick);
            _buyButton.onClick.AddListener(TryLockItem);

            foreach (var button in _changeButtons)
                button.onClick.AddListener(OnChangeCurrentEquipment);
        }

        private void TryLockItem()
        {
            if (_equipmentItemState.IsBuyed == false)
                return;

            _buyButton.gameObject.SetActive(false);
            _isBayed.gameObject.SetActive(true);

            foreach (var button in _changeButtons)
                button.gameObject.SetActive(true);
        }

        private void TrySetCurrentEquipment(EquipmentItemState equipmentItemState)
        {
            _isCurrentWeapon.gameObject.SetActive(equipmentItemState.IsEquipped);
        }

        private void TryUnlockBuyButton(Player player, EquipmentItemState equipmentItemState)
        {
            if (player.PlayerStats.Level >= equipmentItemState.ItemData.Level)
                _buyButton.gameObject.SetActive(true);
            else
                _levelAvailableGameObject.SetActive(true);
        }

        private void OnChangeCurrentEquipment()
        {
            CurrentEquipmentChanged?.Invoke(this);
        }

        private void OnButtonClick()
        {
            BuyButtonClicked?.Invoke(this);
        }

        private void CheckEquipState()
        {
            _isCurrentWeapon.gameObject.SetActive(_equipmentItemState.IsEquipped);
        }
    }
}