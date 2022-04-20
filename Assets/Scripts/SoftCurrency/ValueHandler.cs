using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueHandler
{
    private float _amount;
    private float _maxAmount = 1000000;
    private float _minValue;

    private string SaveWord;

    public float Amount => _amount;

    public event Action<float> AmountIncreased;
    public event Action<float> AmountDecreased;
    public event Action<float> AmountLoaded;
    public event Action AmountChanged;

    public ValueHandler(float minValue, string saveAmount)
    {
        SaveWord = saveAmount;
        _minValue = minValue;
        _amount = _minValue;
    }

    public void Increase(float amount)
    {
        ChangeAmount(amount);
        AmountIncreased?.Invoke(_amount);
    }

    public bool TryDecrease(float amount)
    {
        if (IsEnough(amount))
        {
            Decrease(amount);
            return true;
        }
        else
        {
            return false;
        }    
    }

    public bool IsEnough(float amount)
    {
        return _amount >= amount;
    }

    private void Decrease(float amount)
    {
        ChangeAmount(-amount);
        AmountDecreased?.Invoke(_amount);
    }

    public void ChangeAmount(float amount)
    {
        _amount += amount;

        Mathf.Clamp(_amount, _minValue, _maxAmount);
        Save();
        AmountChanged?.Invoke();
    }

    public void Save()
    {
        PlayerPrefs.SetFloat(SaveWord, _amount);
    }

    public float LoadAmount()
    {
        _amount = _minValue;

        if (PlayerPrefs.HasKey(SaveWord))
            _amount =  PlayerPrefs.GetFloat(SaveWord);

        AmountLoaded?.Invoke(_amount);
        return _amount;
    }
}
