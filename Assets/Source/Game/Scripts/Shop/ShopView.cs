using UnityEngine;
using UnityEngine.UI;

public class ShopView : MonoBehaviour
{
    [Header("[Shop Panel]")]
    [SerializeField] private Shop _shop;
    [Header("[Panels]")]
    [SerializeField] private Shop[] _shopPanels;
    [Header("[Text]")]
    [SerializeField] private Text _coins;
    [SerializeField] private Text _points;

    private void Awake()
    {
        _shop.PlayerResourceUpdated += PlayerResourceUpdate;
    }

    private void OnDestroy()
    {
        _shop.PlayerResourceUpdated -= PlayerResourceUpdate;
    }

    private void PlayerResourceUpdate(int coins, int abilityPoints)
    {
        _coins.text = coins.ToString();
        _points.text = abilityPoints.ToString();
    }
}
