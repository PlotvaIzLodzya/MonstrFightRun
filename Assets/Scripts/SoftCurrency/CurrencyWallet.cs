using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyWallet
{
    private int _amount;
    private int _maxAmount = 1000000;

    public const string SaveAmount = "SaveAmount";

    public event Action<int> AmountIncreased;
    public event Action<int> AmountDecreased;
    public event Action<int> AmountLoaded;

    public void InceaseCurrency(int amount)
    {
        AmountChange(amount);
        AmountIncreased?.Invoke(_amount);
    }

    public bool TrySpend(int amount)
    {
        if (IsEnoughCurrency(amount))
        {
            DecreaseCurrency(amount);
            return true;
        }
        else
        {
            return false;
        }    
    }

    public bool IsEnoughCurrency(int amount)
    {
        return _amount >= amount;
    }

    private void DecreaseCurrency(int amount)
    {
        AmountChange(-amount);
        AmountDecreased?.Invoke(_amount);
    }

    public void AmountChange(int amount)
    {
        _amount += amount;

        Mathf.Clamp(_amount, 0, _maxAmount);
        Save();
    }

    public void Save()
    {
        PlayerPrefs.SetInt(SaveAmount, _amount);
    }

    public int LoadAmount()
    {
        _amount = 0;

        if (PlayerPrefs.HasKey(SaveAmount))
            _amount =  PlayerPrefs.GetInt(SaveAmount);

        AmountLoaded?.Invoke(_amount);
        return _amount;
    }
}
