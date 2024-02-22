using UnityEngine;
using UnityEngine.UI;

public class ShopView : MonoBehaviour
{
    [Header("[Shop]")]
    [SerializeField] private Shop _shop;
    [Header("[Text]")]
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
