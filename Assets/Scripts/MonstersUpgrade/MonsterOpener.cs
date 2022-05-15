using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterOpener : ShopButton
{
    [SerializeField] private MonsterCell _monsterCell;

    private const string SaveName = "MonsterUpgraderHandler";

    public event Action Opened;

    private void Awake()
    {
        LoadProgression(SaveName);
    }

    public override void Buy(float cost)
    {
        if (SwipeZone.IsMoving)
            return;

        Opened?.Invoke();
        OpenCell();
        gameObject.SetActive(false);
    }

    public void OpenCell()
    {
        _monsterCell.Open();
        _monsterCell.TryOpenInfoPanel();
    }
}
