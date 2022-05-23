using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class MonsterLevelUpgrader : ShopButton
{
    [SerializeField] private MonsterPlaceAccepter _monstarAccepter;
    [SerializeField] private int _levelPerBuy;

    private MonstersHandler _monstersHandler;
    private bool _isInitialized;
    private string SaveName => $"MonsterCellLevelHandler{_monstarAccepter.Monster.Name}";

    public void Init()
    {
        LoadProgression(SaveName);

        if(_isInitialized == false)
        {
            AddLvl(_monstarAccepter.Monster, (int)ValueHandler.LoadAmount() - 1);
            IncreaseMonstersMight((int)ValueHandler.LoadAmount() - 1);
        }


        UpdateInfo();

        OnValueChanged();

        _isInitialized = true;
    }

    public override void Buy(float cost)
    {
        ValueHandler.Increase(_levelPerBuy);
        AddLvl(_monstarAccepter.Monster, _levelPerBuy);

        IncreaseMonstersMight(_levelPerBuy);

        //if (_monsterPlaceAccepters == null)
        //    _monsterPlaceAccepters = FindObjectOfType<MonstersHandler>().GetComponentsInChildren<MonsterPlaceAccepter>();

        //var accepter = _monsterPlaceAccepters.FirstOrDefault(accepter =>accepter.Monster != null && accepter.Monster.GetType() == _monsterCell.Monster.GetType());

        //if(accepter != null)
        //    AddLvl(accepter.Monster, _levelPerBuy);
    }

    protected override void UpdateInfo()
    {
        _monstarAccepter.MonsterInfoPanel.UpdateInfo();
        base.UpdateInfo();
    }

    private void AddLvl(Monster monster, int level)
    {
        monster.LevelUp(level);
    }

    private void IncreaseMonstersMight(int value)
    {
        if (_monstersHandler == null)
            _monstersHandler = FindObjectOfType<MonstersHandler>();

        _monstersHandler.ChangeMonstersMight(value);
    }
}
