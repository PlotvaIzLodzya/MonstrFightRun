using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterShop : MonoBehaviour
{
    [SerializeField] private MonsterCell[] _monsterCells;

    private MonsterPlaceAccepter[] _monsterPlaceAcepters;

    private ValueHandler _monsterCount = new ValueHandler(1, 5, "MonsterCountShopSaveName");
    private ValueHandler _openedMonstersCellCount = new ValueHandler(1, 9, "OpenedMonsterCellCountShopSaveName");

    private void Awake()
    {
        _monsterPlaceAcepters = FindObjectOfType<MonstersHandler>().GetComponentsInChildren<MonsterPlaceAccepter>();
        _monsterCount.LoadAmount();
        _openedMonstersCellCount.LoadAmount();

        StartCoroutine(Delay());
    }

    private void OnEnable()
    {
        foreach (var monsterCell in _monsterCells)
        {
            monsterCell.MonsterUpgraderHandler.Opened += OnCellOpened;
        }
    }

    private void OnDisable()
    {
        foreach (var monsterCell in _monsterCells)
        {
            monsterCell.MonsterUpgraderHandler.Opened -= OnCellOpened;
        }
    }

    private void OnCellOpened()
    {
        _monsterCount.Increase(1);
        int index = (int)_monsterCount.Value - 1;
        _monsterPlaceAcepters[index].Activate();
        _monsterPlaceAcepters[index].Open();
    }

    private void PrepareMonsterHolders(IMonsterHolder[] monsterHolders, int progressionCount, int openedPlacesCount)
    {
        for (int i = 0; i < monsterHolders.Length; i++)
        {
            monsterHolders[i].Activate();

            if (i < progressionCount)
                monsterHolders[i].Open();

            if (i >= progressionCount && i > openedPlacesCount)
                monsterHolders[i].Hide();
        }
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.1f);

        foreach (var monsterCell in _monsterCells)
        {
            monsterCell.Init();
        }

        _monsterCells[0].Open();
        PrepareMonsterHolders(_monsterPlaceAcepters, (int)_monsterCount.Value, 2);
    }
}
