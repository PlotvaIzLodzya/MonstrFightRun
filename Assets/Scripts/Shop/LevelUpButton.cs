using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class LevelUpButton : ShopButton
{
    [SerializeField] private int _levelsAmount;
    [SerializeField] private TMP_Text value;
    private void Awake()
    {
        value.text = $"+{_levelsAmount}";
    }

    public override void Buy(int cost)
    {
        FindObjectOfType<MonstersHandler>().LevelUpAllMonster(_levelsAmount);
    }
}
