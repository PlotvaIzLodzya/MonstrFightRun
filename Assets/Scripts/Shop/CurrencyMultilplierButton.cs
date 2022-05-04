using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrencyMultilplierButton : ShopButton
{
    [SerializeField] private TMP_Text _multiplier;
    [SerializeField] private float _mulitplierPerBuy;

    private const string SaveName = "CurrencyMultiplier";

    private void Awake()
    {
        LoadProgression(SaveName);
        UpdateInfo();
        Player = FindObjectOfType<Player>();
        Player.SetMuliplier(ValueHandler.Value);
    }

    public override void Buy(float cost)
    {
        ValueHandler.Increase(_mulitplierPerBuy);
        Player.SetMuliplier(ValueHandler.Value);
    }

    protected override void UpdateInfo()
    {
        _multiplier.text = $"X{ValueHandler.Value}";
        base.UpdateInfo();
    }
}
