using System.Collections.Generic;
using UnityEngine;

public class QuestPanel : Panels
{
    private readonly int _indexShift = 1;

    [Header("[Level Buttons]")]
    [SerializeField] private List<Buttons> _buttons;
    [Header("[Containers]")]
    [SerializeField] private GameObject _buttonsContainer;
    [Header("[QuestPanelView]")]
    [SerializeField] private QuestPanelView _questPanelView;
    [Header("[CanvasLoader]")]
    [SerializeField] private CanvasLoader _canvasLoader;

    public QuestPanelView QuestPanelView => _questPanelView;

    public void Initialize(LoadConfig loadConfig)
    {
        _questPanelView.Initialize(loadConfig.PlayerCoins, loadConfig.PlayerLevel);
        UpdateParameters(loadConfig, _buttons);
        UnlockNewLevel(_buttons);
        Load(_buttons);
    }

    private void UpdateParameters(LoadConfig loadConfig, List<Buttons> buttons)
    {
        for (int index = 0; index < buttons.Count; index++)
        {
            SetParameters(loadConfig, buttons[index]);

            if (loadConfig.PlayerLevels != null)
            {
                if (loadConfig.PlayerLevels.TryGetValue(index, out bool value))
                {
                    buttons[index].ButtonsView.SetButtonState(value);
                    buttons[index].Levels.SetComplete();
                }
            }
        }

        buttons[0].ButtonsView.SetButtonState(true);
    }

    private void UnlockNewLevel(List<Buttons> buttons)
    {
        for (int i = 0; i < buttons.Count - _indexShift; i++)
        {
            if (buttons[i].IsLevelComplete) buttons[i + _indexShift].ButtonsView.SetButtonState(true);
        }
    }

    private void Load(List<Buttons> buttons)
    {
        Clear();

        foreach (var button in buttons)
        {
            Instantiate(button, _buttonsContainer.transform);
        }
    }

    private void Clear()
    {
        for (int i = 0; i < _buttonsContainer.transform.childCount; i++)
        {
            Destroy(_buttonsContainer.transform.GetChild(i).gameObject);
        }
    }

    private void SetParameters(LoadConfig loadConfig, Buttons buttons)
    {
        buttons.GetParameters(loadConfig, _canvasLoader, this);
        buttons.ButtonsView.SetButtonState(false);
        buttons.ButtonsView.SetImage(buttons.IsLevelComplete);
    }
}