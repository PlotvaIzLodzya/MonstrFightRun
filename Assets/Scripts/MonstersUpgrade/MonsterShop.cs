using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MonsterShop : MonoBehaviour
{
    [SerializeField] private MonsterCell[] _monsterCells;
    [SerializeField] private MonsterCell[] _initialMonsterCells;

    private MonsterPlaceAccepter[] _monsterPlaceAcepters;

    private ValueHandler _monsterCount = new ValueHandler(1, 3, "MonsterCountShopSaveName");
    private ValueHandler _openedMonstersCellCount = new ValueHandler(1, 9, "OpenedMonsterCellCountShopSaveName");
    private ValueHandler _openedMonsterPlaces = new ValueHandler(3, 5, "OpenMonsterPlacesSave");

    public int OpenedMonsterPlaces => (int)_openedMonsterPlaces.Value;

    private void Awake()
    {
        _monsterPlaceAcepters = FindObjectOfType<MonstersHandler>().GetComponentsInChildren<MonsterPlaceAccepter>();
        _monsterCount.LoadAmount();
        _openedMonstersCellCount.LoadAmount();
        _openedMonsterPlaces.LoadAmount();

        StartCoroutine(Delay(3));
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

    public bool TryGetMonstersForPool(out List<Monster> monsters)
    {
        int count = RandomMonstersCount();
        monsters = new List<Monster>();

        if (count <= 0)
            return false;
        
        var tempMonsters = from MonsterCell monsterCell in _monsterCells
                    where monsterCell.IsOpened && monsterCell.IsMonsterPlaced() == false
                    select monsterCell.InitialMonster;

        List<Monster> tempMonstersList = tempMonsters.ToList();

        for (int i = 0; i < count; i++)
        {
            int index = Random.Range(0, tempMonstersList.Count);

            monsters.Add(tempMonstersList[index]);
            tempMonstersList.RemoveAt(index);
        } 

        return count>0;
    }

    public int RandomMonstersCount()
    {
        int count = 0;

        foreach (var monsterCell in _monsterCells)
        {
            if (monsterCell.IsMonsterPlaced())
                count++;
        }

        return Mathf.Abs((int)_openedMonsterPlaces.Value - count);
    }

    public void OpenCellWithMonster(Monster monster)
    {
        _monsterCells.FirstOrDefault(cell => cell.Monster.GetType() == monster.GetType()).Open();
        OnCellOpened();
    }

    private void OnCellOpened()
    {
        _monsterCount.Increase(1);
        _openedMonsterPlaces.Increase(1);
        int placeToOpenIndex = (int)_monsterCount.Value - 1;
        int placeToActivateIndex = (int)_openedMonsterPlaces.Value - 1;
        _monsterPlaceAcepters[placeToActivateIndex].Activate();
        _monsterPlaceAcepters[placeToOpenIndex].Open();
    }

    private void PrepareMonsterHolders(IMonsterHolder[] monsterHolders, int progressionCount, int openedPlacesCount)
    {
        for (int i = 0; i < monsterHolders.Length; i++)
        {
            monsterHolders[i].Activate();

            if (i < progressionCount)
                monsterHolders[i].Open();

            if (i >= progressionCount && i >= openedPlacesCount)
                monsterHolders[i].Hide();
        }
    }

    private IEnumerator Delay(int count)
    {
        yield return new WaitForSeconds(0.1f);

        foreach (var monsterCell in _monsterCells)
        {
            monsterCell.Init();
        }

        foreach (var initialMonsterCell in _initialMonsterCells)
        {
            initialMonsterCell.Open();
        }

        PrepareMonsterHolders(_monsterPlaceAcepters, (int)_monsterCount.Value, (int)_openedMonsterPlaces.Value);
    }
}
