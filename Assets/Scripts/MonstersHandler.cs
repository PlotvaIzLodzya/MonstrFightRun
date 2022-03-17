using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MonstersHandler : MonoBehaviour
{
    [SerializeField] private Monster _initialMonster;

    private MonsterPlace[] _monsterPlaces;
    private int _counter;

    private void Awake()
    {
        _monsterPlaces = GetComponentsInChildren<MonsterPlace>();
        Error.CheckOnNull(_monsterPlaces[0], nameof(MonsterPlace));
        TrySetMonsterToPlace(_initialMonster);
    }

    public bool TrySetMonsterToPlace(Monster monster)
    {
        MonsterPlace place = _monsterPlaces.FirstOrDefault(place => place.IsTaken == false || place.Monster.GetType() == monster.GetType());

        if (place != default)
        {
            if (CanMerge(monster, place))
            {
                place.Monster.Merge();

                return true;
            }

            SetMonsterToPlace(monster, place);

            return true;
        }

        return false;
    }

    public void MergeAllMonster()
    {
        var monsters = GetAllMonsters();

        foreach (var monster in monsters)
        {
            monster.Merge();
        }
    }

    public void UnMergeAllMonster()
    {
        var monsters = GetAllMonsters();

        foreach (var monster in monsters)
        {
            monster.UnMerge();
        }
    }

    private void SetMonsterToPlace(Monster monsterType, MonsterPlace monsterPlace)
    {
        var monster = Instantiate(monsterType);
        monsterPlace.Take(monster);
        monster.transform.SetParent(monsterPlace.transform, false);
    }

    private bool CanMerge(Monster monster, MonsterPlace place)
    {       
        return place.Monster != null && place.Monster.GetType() == monster.GetType();
    }

    private IEnumerable<Monster> GetAllMonsters()
    {
        var monsters = from MonsterPlace monsterPlace in _monsterPlaces
                       where monsterPlace.Monster != null
                       select monsterPlace.Monster;

        return monsters;
    }
}
