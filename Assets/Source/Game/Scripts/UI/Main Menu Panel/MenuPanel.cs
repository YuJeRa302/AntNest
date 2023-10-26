using UnityEngine;

public class MenuPanel : Panels
{
    [Header("[Level Buttons]")]
    [SerializeField] private QuestPanel _questPanel;
    [Header("[Menu]")]
    [SerializeField] private Menu _menu;

    public void Initialized() => _menu.Initialized(); 

    protected override void UpdateInfo()
    {
        _questPanel.Initialized(_menu.LoadConfig);
    }
}