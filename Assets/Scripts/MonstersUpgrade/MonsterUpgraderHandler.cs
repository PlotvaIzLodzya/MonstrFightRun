using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterUpgraderHandler : ShopButton
{
    [SerializeField] private MonsterLevelUpgrader _monsterLevelUpgrader;

    private const string SaveName = "MonsterUpgraderHandler";

    public event Action Opened;
    private void Awake()
    {
        LoadProgression(SaveName);
    }

    public override void Buy(float cost)
    {
        Opened?.Invoke();
        EnableUpgradeButton();
        gameObject.SetActive(false);
    }

    public void EnableUpgradeButton()
    {
        _monsterLevelUpgrader.gameObject.SetActive(true);
    }
}
