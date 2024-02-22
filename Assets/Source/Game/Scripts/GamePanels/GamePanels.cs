using System;
using UnityEngine;

public abstract class GamePanels : MonoBehaviour
{
    protected Player Player;
    protected LevelObserver LevelObserver;

    public Action PanelOpened;
    public Action PanelClosed;
    public Action OpenAd;
    public Action CloseAd;

    public void Initialize(Player player, LevelObserver levelObserver)
    {
        Player = player;
        LevelObserver = levelObserver;
    }

    protected virtual void Open()
    {
        gameObject.SetActive(true);
        PanelOpened?.Invoke();
    }

    protected virtual void Close()
    {
        gameObject.SetActive(false);
        PanelClosed?.Invoke();
    }
}
