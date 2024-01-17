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

    protected virtual void OpenPanel()
    {
        gameObject.SetActive(true);
        PanelOpened?.Invoke();
    }

    protected virtual void ClosePanel()
    {
        gameObject.SetActive(false);
        PanelClosed?.Invoke();
    }

    protected virtual void Subscribe() 
    {

    }

    protected virtual void UpdateInfo() { }
    protected virtual void Filling() { }
}
