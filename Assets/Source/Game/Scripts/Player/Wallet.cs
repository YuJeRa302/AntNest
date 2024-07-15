using System;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    private readonly int _minValue = 0;

    private int _currentCoins = 100;
    private int _points = 0;

    public event Action<int> CoinCountChanged;
    public event Action<int> GoldenRuneTaked;

    public int Coins => _currentCoins;
    public int Points => _points;

    public void Initialize(int value)
    {
        _currentCoins = (value == 0) ? _currentCoins : value;
    }

    public void BuyItem(int value)
    {
        _currentCoins = Mathf.Clamp(_currentCoins - value, _minValue, _currentCoins);
        CoinCountChanged?.Invoke(_currentCoins);
    }

    public void BuyAbility(int value)
    {
        _points = Mathf.Clamp(_points - value, _minValue, _points);
    }

    public void TakeGoldenRune(int value)
    {
        _currentCoins += value;
        GoldenRuneTaked?.Invoke(value);
        CoinCountChanged?.Invoke(_currentCoins);
    }

    public void TakeCoins(int value)
    {
        _currentCoins += value;
        CoinCountChanged?.Invoke(_currentCoins);
    }

    public void SetDefaultAbilityPoints(int value)
    {
        _points = value;
    }
}