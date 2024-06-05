using System;
using UnityEngine;
using UnityEngine.UI;
using Lean.Localization;

public abstract class ShopTab : MonoBehaviour
{
    public TypeItem ItemType;

    [SerializeField] protected GameObject ItemContainer;

    protected DialogPanel DialogPanel;

    [SerializeField] private Button _openButton;
    [SerializeField] private Shop _shop;
    [SerializeField] private LeanLocalizedText _leanText;
    [SerializeField] private string _translationText;

    public event Action TabOpened;
    public event Action PlayerResourceUpdated;

    protected void Awake()
    {
        _openButton.onClick.AddListener(OpenTab);
    }

    protected void OnDestroy()
    {
        _openButton.onClick.RemoveListener(OpenTab);
    }

    public void Initialize(DialogPanel dialogPanel)
    {
        DialogPanel = dialogPanel;
    }

    protected virtual void OpenTab()
    {
        _leanText.TranslationName = _translationText;
        TabOpened?.Invoke();
        gameObject.SetActive(true);
    }

    protected virtual void UpdatePlayerResourceValue()
    {
        PlayerResourceUpdated.Invoke();
    }
}