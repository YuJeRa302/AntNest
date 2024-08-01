using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Game.Scripts
{
    public class ConsumableButton : MonoBehaviour
    {
        [SerializeField] private Button _useConsumableButton;
        [SerializeField] private Text _countConsumableItemText;
        [SerializeField] private Image _reloadingImage;

        private bool _isUseConsumable = true;
        private ConsumableItemData _consumableItemData;
        private int _countConsumableItem;
        private int _minValue = 0;
        private Player _player;
        private KeyCode _keyCode;
        private float _defaultDelayConsumable;
        private float _currentDelayConsumable;
        private Coroutine _coolDown;

        public event Action<ConsumableItemData> ConsumableUsed;

        private void Awake()
        {
            _useConsumableButton.onClick.AddListener(Use);
        }

        private void OnEnable()
        {
            _coolDown = StartCoroutine(Delay());
            UpdateCountConsumable();
        }

        private void OnDestroy()
        {
            _useConsumableButton.onClick.RemoveListener(Use);
            _player.PlayerConsumablesUser.ConsumableBuyed -= OnBuyConsumable;
            _player.PlayerConsumablesUser.ConsumableUsed -= ApplyConsumable;

            if (_coolDown != null)
                StopCoroutine(_coolDown);
        }

        private void Update()
        {
            if (Input.GetKey(_keyCode))
                Use();
        }

        public void Initialize(ConsumableItemState consumableItemState, Player player)
        {
            _player = player;
            Fill(consumableItemState);
            AddListener();
        }

        private void Use()
        {
            if (_countConsumableItem > _minValue && _isUseConsumable == false)
            {
                if (ConsumableUsed != null)
                    ConsumableUsed?.Invoke(_consumableItemData);
            }
        }

        private void AddListener()
        {
            _player.PlayerConsumablesUser.ConsumableBuyed += OnBuyConsumable;
            _player.PlayerConsumablesUser.ConsumableUsed += ApplyConsumable;
        }

        private void Fill(ConsumableItemState consumableItemState)
        {
            _keyCode = consumableItemState.ConsumableItemData.KeyCode;
            _defaultDelayConsumable = consumableItemState.ConsumableItemData.DelayButton;
            _consumableItemData = consumableItemState.ConsumableItemData;
            _countConsumableItem = consumableItemState.ConsumableItemData.Count;
            _countConsumableItemText.text = _countConsumableItem.ToString();
        }

        private void ApplyConsumable(TypeConsumable typeConsumable)
        {
            if (typeConsumable != _consumableItemData.TypeConsumable)
                return;

            _countConsumableItem--;
            _currentDelayConsumable = _defaultDelayConsumable;
            UpdateCountConsumable();
            UpdateButton(true, _currentDelayConsumable);
            ResumeCooldown();
        }

        private void ResumeCooldown()
        {
            if (gameObject.activeSelf == true)
            {
                if (_coolDown != null)
                    StopCoroutine(_coolDown);

                _coolDown = StartCoroutine(Delay());
            }
        }

        private IEnumerator Delay()
        {
            while (_currentDelayConsumable > _minValue)
            {
                _currentDelayConsumable -= Time.deltaTime;
                _reloadingImage.fillAmount = _currentDelayConsumable / _defaultDelayConsumable;
                yield return null;
            }

            UpdateButton(false, _minValue);
        }

        private void UpdateCountConsumable()
        {
            _countConsumableItemText.text = _countConsumableItem.ToString();
        }

        private void UpdateButton(bool state, float delay)
        {
            _isUseConsumable = state;
            _reloadingImage.fillAmount = delay;
        }

        private void OnBuyConsumable(TypeConsumable typeConsumable)
        {
            if (typeConsumable == _consumableItemData.TypeConsumable)
                _countConsumableItem++;
            else
                return;
        }
    }
}