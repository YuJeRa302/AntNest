using UnityEngine;

public class Wallet : MonoBehaviour
{
    private readonly int _minCoins = 0;
    private int _currentCoins = 100;

    public void Buy(int itemCost)
    {
        _currentCoins = Mathf.Clamp(_currentCoins - itemCost, _minCoins, _currentCoins);
    }

    public void SetDefaltCoins(int value)
    {
        _currentCoins = (value == 0) ? _currentCoins : value;
    }

    public void TakeCoin(int value)
    {
        _currentCoins += value;
    }

    public int GiveCoin()
    {
        return _currentCoins;
    }
}