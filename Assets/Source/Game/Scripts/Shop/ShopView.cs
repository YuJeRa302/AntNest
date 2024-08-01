using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Game.Scripts
{
    public class ShopView : MonoBehaviour
    {
        [SerializeField] private Shop _shop;
        [SerializeField] private Text _coins;
        [SerializeField] private Text _points;

        private void Awake()
        {
            _shop.PlayerResourceChanged += PlayerResourceUpdate;
        }

        private void OnDestroy()
        {
            _shop.PlayerResourceChanged -= PlayerResourceUpdate;
        }

        private void PlayerResourceUpdate(int coins, int abilityPoints)
        {
            _coins.text = coins.ToString();
            _points.text = abilityPoints.ToString();
        }
    }
}
