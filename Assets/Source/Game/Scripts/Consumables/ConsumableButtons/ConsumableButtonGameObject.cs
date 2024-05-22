using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class ConsumableButtonGameObject : MonoBehaviour
{
    [SerializeField] private Button _useConsumableButton;
    [SerializeField] private Text _countConsumableItem;
    [SerializeField] private Image _reloadingImage;

    protected readonly int MinValue = 0;

    protected Player Player;
    protected bool IsUseConsumable = true;
    protected float ButtonDelay;
    protected int Value;
    protected TypeConsumable TypeConsumable;
    protected ConsumableItemData ConsumableItemData;
    protected int CountConsumableItem;
    protected ItemGameObject ItemGameObject;
    protected Transform PlacementPoint;

    private float _animationTime;
    private Coroutine _delay;

    [Obsolete]
    private void Awake()
    {
        _useConsumableButton.onClick.AddListener(Use);
    }

    private void OnEnable()
    {
        _delay = StartCoroutine(Delay(ButtonDelay));
        UpdateCountConsumable();
    }

    [Obsolete]
    private void OnDestroy()
    {
        _useConsumableButton.onClick.RemoveListener(Use);
        Player.PlayerConsumables.ConsumableBuyed -= OnBuyConsumable;

        if (_delay != null)
            StopCoroutine(_delay);
    }

    public void Initialize(ConsumableItemState consumableItemState, Transform placementPoint, Player player)
    {
        Player = player;
        ButtonDelay = consumableItemState.ConsumableItemData.DelayButton;
        TypeConsumable = consumableItemState.ConsumableItemData.TypeConsumable;
        ConsumableItemData = consumableItemState.ConsumableItemData;
        ItemGameObject = consumableItemState.ConsumableItemData.ItemGameObject;
        CountConsumableItem = consumableItemState.ConsumableItemData.Count;
        PlacementPoint = placementPoint;
        _countConsumableItem.text = CountConsumableItem.ToString();
        Player.PlayerConsumables.ConsumableBuyed += OnBuyConsumable;
    }

    [Obsolete]
    protected virtual void Use() { }

    protected void ApplyConsumable(float delay)
    {
        UpdateCountConsumable();
        UpdateButton(true, delay);
        ResumeCooldown(delay);
    }

    private void ResumeCooldown(float delay)
    {
        if (gameObject.activeSelf == true)
        {
            if (_delay != null)
                StopCoroutine(_delay);

            _delay = StartCoroutine(Delay(delay));
        }
        else
            return;
    }

    private IEnumerator Delay(float delay)
    {
        _animationTime = delay;

        while (_animationTime > MinValue)
        {
            _animationTime -= Time.deltaTime;
            _reloadingImage.fillAmount = _animationTime / delay;
            yield return null;
        }

        UpdateButton(false, MinValue);
    }

    private void UpdateCountConsumable()
    {
        _countConsumableItem.text = CountConsumableItem.ToString();
    }

    private void UpdateButton(bool state, float delay)
    {
        IsUseConsumable = state;
        _reloadingImage.fillAmount = delay;
    }

    private void OnBuyConsumable(TypeConsumable typeConsumable)
    {
        if (typeConsumable == TypeConsumable)
            CountConsumableItem++;
        else
            return;
    }
}
