using System.Collections.Generic;
using UnityEngine;

public class QuestPanel : Panels
{
    [Header("[Level Buttons]")]
    [SerializeField] private List<Buttons> _buttonsGameLocation;
    [Header("[Containers]")]
    [SerializeField] private GameObject _buttonsContainer;
    [Header("[QuestPanelView]")]
    [SerializeField] private QuestPanelView _questPanelView;
    [Header("[CanvasLoader]")]
    [SerializeField] private CanvasLoader _canvasLoader;

    private readonly int _minCountButtons = 0;
    private readonly int _indexShift = 1;

    public QuestPanelView QuestPanelView => _questPanelView;

    public void Initialized(LoadConfig loadConfig)
    {
        _questPanelView.Initialized(loadConfig.PlayerCoins, loadConfig.PlayerLevel);
        UpdateParameters(loadConfig, _buttonsGameLocation);
        Load();
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
                    buttons[index].SetButtonState(value);
                    buttons[index].Levels.SetComplete();
                }
            }
        }

        buttons[0].SetButtonState(true);
        UnlockNew(buttons);
    }

    private void Load()
    {
        if (_buttonsContainer.transform.childCount == _minCountButtons)
        {
            foreach (var button in _buttonsGameLocation)
            {
                Instantiate(button, _buttonsContainer.transform);
            }
        }
        else return;
    }

    private void SetParameters(LoadConfig loadConfig, Buttons buttons)
    {
        buttons.GetConfig(loadConfig);
        buttons.GetQuestPanel(gameObject.GetComponent<QuestPanel>());
        buttons.GetCanvasLoader(_canvasLoader);
        buttons.SetButtonState(false);
        buttons.SetImage();
    }

    private void UnlockNew(List<Buttons> buttons)
    {
        for (int index = 0; index < buttons.Count - _indexShift; index++)
        {
            if (buttons[index].IsLevelComplete == true) buttons[index + _indexShift].SetButtonState(true);
        }
    }
}