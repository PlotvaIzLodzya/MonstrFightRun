using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrencyMultilplierButton : ShopButton
{
    [SerializeField] private TMP_Text value;
    [SerializeField] private int _multiplier;
    private void Awake()
    {
        value.text = $"x{_multiplier}";
    }
    public override void Buy(int cost)
    {
        FindObjectOfType<Player>().SetMuliplier(_multiplier);
        SetInactive();
    }
}
