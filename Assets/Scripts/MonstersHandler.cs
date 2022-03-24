using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

[RequireComponent(typeof(MonsterHandlerColliders))]
public class MonstersHandler : MonoBehaviour
{
    [SerializeField] private Monster _initialMonster;
    [SerializeField] private MonstersAnimatorHandler _monstersAnimatorHandler;

    private MonsterHandlerColliders _monsterHandlerColliders;
    private MonsterPlace[] _monsterPlaces;
    private const int AddLevelOnMerge = 10;
    private int _monstersMight;
    private int _counter = 0;

    public int MonsterCounter { get; private set; }
    public int MonsterMight => _monstersMight;

    public event Action<MonsterAnimator> MonsterAdded;
    public event Action<Monster> MonsterMerged;

    private void Awake()
    {
        _monsterHandlerColliders = GetComponent<MonsterHandlerColliders>();

        _monsterPlaces = GetComponentsInChildren<MonsterPlace>();
        Error.CheckOnNull(_monsterPlaces[0], nameof(MonsterPlace));

        TrySetMonsterToPlace(_initialMonster);
    }

    public void KillAllMonsters()
    {
        var monsters = GetAllMonsters();

        foreach (var monster in monsters)
        {
            monster.Die();
        }
    }

    public bool TrySetMonsterToPlace(Monster monster)
    {
        MonsterPlace place = _monsterPlaces.FirstOrDefault(place => place.IsTaken == false || place.Monster.GetType() == monster.GetType());

        if (place != default)
        {
            if (CanMerge(monster, place))
            {
                ChangeMonstersMight(AddLevelOnMerge);

                MonsterMerged?.Invoke(place.Monster);

                return place.Monster.TryMerge(AddLevelOnMerge);
            }


            SetMonsterToPlace(monster, place);
            _monsterHandlerColliders.CreateBoxCollider(place);
            MonsterCounter++;

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
        var monster = Instantiate(monsterType, monsterPlace.transform);
        monsterPlace.Take(monster);

        ChangeMonstersMight(1);
        _monstersAnimatorHandler.AddAnimator(monster.MonsterAnimator);
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
