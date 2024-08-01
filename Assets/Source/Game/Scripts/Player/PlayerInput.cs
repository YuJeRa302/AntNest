using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Source.Game.Scripts
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private GraphicRaycaster _graphicRaycaster;
        [SerializeField] private EventSystem _eventSystem;

        private List<RaycastResult> _results = new ();

        public bool IsAttackBlocked { get; private set; }

        private void Update()
        {
            PointerEventData pointerEventData = new (_eventSystem)
            {
                position = Input.mousePosition
            };

            _graphicRaycaster.Raycast(pointerEventData, _results);
            IsAttackBlocked = _results.Count > 0;
            _results.Clear();
        }
    }
}
