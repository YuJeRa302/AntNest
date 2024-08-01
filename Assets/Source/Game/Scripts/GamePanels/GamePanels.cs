using System;
using UnityEngine;

namespace Assets.Source.Game.Scripts
{
    public abstract class GamePanels : MonoBehaviour
    {
        protected Player Player;
        protected LevelObserver LevelObserver;

        public Action PanelOpened;
        public Action PanelClosed;
        public Action AdOpened;
        public Action AdClosed;

        public virtual void Initialize(Player player, LevelObserver levelObserver)
        {
            Player = player;
            LevelObserver = levelObserver;
        }

        protected virtual void Open()
        {
            gameObject.SetActive(true);
            LevelObserver.PlayerInterfaceView.gameObject.SetActive(false);

            if (PanelOpened != null)
                PanelOpened?.Invoke();
        }

        protected virtual void Close()
        {
            gameObject.SetActive(false);
            LevelObserver.PlayerInterfaceView.gameObject.SetActive(true);

            if (PanelClosed != null)
                PanelClosed?.Invoke();
        }
    }
}