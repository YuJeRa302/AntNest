using System.Collections.Generic;
using UnityEngine;

public class QuestPanel : MonoBehaviour
{
    [Header("[Quest Panel]")]
    [SerializeField] private GameObject _mainButtons;
    [Header("[Level Buttons]")]
    [SerializeField] private List<Buttons> _buttonsGameLocation;
    [Header("[Containers]")]
    [SerializeField] private GameObject _buttonsContainer;
    [Header("[Config]")]
    [SerializeField] private LoadConfig _loadConfig;

    public void OpenQuestPanel()
    {
        _mainButtons.SetActive(false);
        LoadButtons(_buttonsGameLocation);
        gameObject.SetActive(true);
    }

    public void CloseQuestPanel()
    {
        _mainButtons.SetActive(true);
        gameObject.SetActive(false);
    }

    private void Start()
    {
        foreach (var button in _buttonsGameLocation)
        {
            Instantiate(button, _buttonsContainer.transform);
        }
    }

    private void LoadButtons(List<Buttons> buttons)
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].SetImage();
            buttons[i].GetConfig(_loadConfig);

            if (buttons[i].IsLevelComplete == true)
            {
                if (buttons.Count > 1) buttons[i + 1].UnlockButton();
                buttons[i].UnlockButton();
            }
        }
    }
}