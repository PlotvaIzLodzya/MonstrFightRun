using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class MonsterLevelUpgrader : ShopButton
{
    [SerializeField] private MonsterCell _monsterCell;
    [SerializeField] private int _levelPerBuy;
    [SerializeField] private StatSlider _attackSlider;
    [SerializeField] private StatSlider _healthSlider;

    private string SaveName => $"MonsterCellLevelHandler{_monsterCell.Monster.Name}";
    private MonstersHandler _monstersHandler;
    private MonsterPlaceAccepter[] _monsterPlaceAccepters;

    private void Awake()
    {
        LoadProgression(SaveName);
        AddLvl(_monsterCell.Monster, (int)ValueHandler.LoadAmount() - 1);
        UpdateInfo();

        _attackSlider.Init(_monsterCell.InitialMonster.Level, _monsterCell.Monster.DamagePerLevel, 50);
        _healthSlider.Init(_monsterCell.InitialMonster.Level, _monsterCell.Monster.Health.HealthPerLevel, 100);
    }

    public override void Buy(float cost)
    {
        ValueHandler.Increase(_levelPerBuy);
        AddLvl(_monsterCell.InitialMonster, _levelPerBuy);

        if (_monsterPlaceAccepters == null)
            _monsterPlaceAccepters = FindObjectOfType<MonstersHandler>().GetComponentsInChildren<MonsterPlaceAccepter>();

        var accepter = _monsterPlaceAccepters.FirstOrDefault(accepter =>accepter.Monster != null && accepter.Monster.GetType() == _monsterCell.Monster.GetType());

        if(accepter != null)
            AddLvl(accepter.Monster, _levelPerBuy);

        if (_monsterCell.IsMonsterUsed)
            IncreaseMonstersMight();
    }

    protected override void UpdateInfo()
    {
        _monsterCell.MonsterInfoPanel.UpdateInfo();
        _attackSlider.UpdateInfo();
        _healthSlider.UpdateInfo();
        base.UpdateInfo();
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
