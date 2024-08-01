using UnityEngine;

namespace Assets.Source.Game.Scripts
{
    public class ObjectDisabler : MonoBehaviour
    {
        private void Start()
        {
            gameObject.SetActive(false);
        }
    }
}