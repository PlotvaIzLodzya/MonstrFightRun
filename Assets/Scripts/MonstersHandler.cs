using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class MonstersHandler : MonoBehaviour
{
    [SerializeField] private Monster _initialMonster;

    private MonsterPlace[] _monsterPlaces;
    private const int AddLevelOnMerge = 10;
    private int _monstersMight;

    public int MonsterMight => _monstersMight;

    public event Action<MonsterAnimator> MonsterAdded;

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
            ChangeMonstersMight(AddLevelOnMerge);

            if (CanMerge(monster, place))
            {
                place.Monster.Merge(AddLevelOnMerge);

                return true;
            }

            SetMonsterToPlace(monster, place);

            return true;
        }

        return false;
    }

    public void LevelUpAllMonster(int level)
    {
        var monsters = GetAllMonsters();

        foreach (var monster in monsters)
        {
            ChangeMonstersMight(level);
            monster.LevelUp(level);
        }
    }

    public void LevelDownAllMonster()
    {
        var monsters = GetAllMonsters();

        foreach (var monster in monsters)
        {
            monster.LevelDown(1);
        }
    }

    private void ChangeMonstersMight(int level)
    {
        _monstersMight+= level;
    }

    private void SetMonsterToPlace(Monster monsterType, MonsterPlace monsterPlace)
    {
        var monster = Instantiate(monsterType);
        monsterPlace.Take(monster);
        monster.transform.SetParent(monsterPlace.transform, false);
        MonsterAdded?.Invoke(monster.GetComponent<MonsterAnimator>());
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
