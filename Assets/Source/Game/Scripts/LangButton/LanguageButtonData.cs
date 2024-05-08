using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New LangButton", menuName = "Create LangButton", order = 51)]
public class LanguageButtonData : ScriptableObject
{
    [SerializeField] private string _nameLanguage;
    [SerializeField] private Sprite _iconLanguage;

    public string NameLanguage => _nameLanguage;
    public Sprite IconLanguage => _iconLanguage;
}

[Serializable]
public class LanguageButtonState
{
    public LanguageButtonData LanguageButtonData;
}