using System;
using UnityEngine;
using UnityEngine.UI;

public class LanguageButton : MonoBehaviour
{
    [Header("[Language]")]
    [SerializeField] private string _language;
    [Header("[Button]")]
    [SerializeField] private Button _button;

    public event Action<string> LanguageSelected; // переделать по типу Level Data , сделать LanguageButtonData и LanguageButtonView, заполнять их в Settings

    private void Awake()
    {
        _button.onClick.AddListener(OnSelectLanguage);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnSelectLanguage);
    }

    private void OnSelectLanguage()
    {
        LanguageSelected.Invoke(_language);
    }
}