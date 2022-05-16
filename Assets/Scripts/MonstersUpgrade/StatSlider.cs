using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class StatSlider : MonoBehaviour
{
    [SerializeField] private Image _slider;
    [SerializeField] private TMP_Text _statLvlText;

    private float _step;
    private float _statValue;

    public void Init(int level, float step, float divider)
    {
        _step = step/ divider;
        _statValue = (level * _step);
        _slider.fillAmount = _statValue - (int)_statValue;
        SetLevel();
    }

    public void UpdateInfo()
    {
        _slider.fillAmount += _step;
        _statValue += _step;

        if (_slider.fillAmount >= 0.99f)
        {
            _slider.fillAmount = 0;
            SetLevel();
        }
    }

    private void SetLevel()
    {
        float textValue = (_statValue)+1;
        _statLvlText.text = $"x{(int)textValue}";
    }
}
