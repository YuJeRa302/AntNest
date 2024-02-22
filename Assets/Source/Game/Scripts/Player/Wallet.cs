using UnityEngine;

public class Wallet : MonoBehaviour
{
    private readonly int _minCoins = 0;

    private int _currentCoins = 1000;

    public int Coins => _currentCoins;

    public void Initialize(int value)
    {
        _currentCoins = (value == 0) ? _currentCoins : value;
    }

    public void Buy(int itemCost)
    {
        _currentCoins = Mathf.Clamp(_currentCoins - itemCost, _minCoins, _currentCoins);
    }

    public void TakeCoins(int value)
    {
        _currentCoins += value;
    }
}