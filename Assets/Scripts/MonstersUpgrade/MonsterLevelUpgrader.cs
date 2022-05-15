using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MonsterLevelUpgrader : ShopButton
{
    [SerializeField] private MonsterCell _monsterCell;
    [SerializeField] private int _levelPerBuy;

    private string SaveName => $"MonsterCellLevelHandler{_monsterCell.Monster.Name}";
    private MonstersHandler _monstersHandler;

    private void Awake()
    {
        LoadProgression(SaveName);
        AddLvl(_monsterCell.Monster, (int)ValueHandler.LoadAmount());
        UpdateInfo();
    }

    public override void Buy(float cost)
    {
        ValueHandler.Increase(_levelPerBuy);
        AddLvl(_monsterCell.InitialMonster, _levelPerBuy);

        if (_monsterCell.IsMonsterPlaced())
            IncreaseMonstersMight();
    }

    private void AddLvl(Monster monster, int level)
    {
        monster.LevelUp(level);
    }

    private void IncreaseMonstersMight()
    {
        if (_monstersHandler == null)
            _monstersHandler = FindObjectOfType<MonstersHandler>();

        _monstersHandler.ChangeMonstersMight(_levelPerBuy);
    }
}
