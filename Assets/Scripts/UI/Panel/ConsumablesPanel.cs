using UnityEngine;
using TMPro;

public class ConsumablesPanel : ShopPanel
{
    [Header("[Health Potion Cost]")]
    [SerializeField] private int costPotion = 50;
    [Header("[Text Cost Item]")]
    [SerializeField] private TMP_Text _textCostItem;

    private void Start()
    {
        Player = FindObjectOfType<Player>();
        _textCostItem.text = costPotion.ToString();
    }

    public void TryBuyItem()
    {
        if (costPotion <= Player.Coins)
        {
            Player.BuyConsumables(costPotion);
            UpdateCoins();
        }
    }
}