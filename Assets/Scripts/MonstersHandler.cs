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
    public event Action<int, int> MightChanged;
    public event Action<int> CurrencyPickedUp;

    private void Awake()
    {
        _monsterHandlerColliders = GetComponent<MonsterHandlerColliders>();

        _monsterPlaces = GetComponentsInChildren<MonsterPlace>();
        Error.CheckOnNull(_monsterPlaces[0], nameof(MonsterPlace));

        TrySetMonsterToPlace(_initialMonster, 1);
    }

    public void KillAllMonsters()
    {
        var monsters = GetAllMonsters();

        foreach (var monster in monsters)
        {
            monster.Die();
        }
    }

    public bool TrySetMonsterToPlace(Monster monster, int level)
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

            SetMonsterToPlace(monster, place, level);
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
            monster.LevelUp(level);
        }

        ChangeMonstersMight(level);
    }

    public void LevelDownAllMonster(int level)
    {
        var monsters = GetAllMonsters();

        foreach (var monster in monsters)
        {
            monster.LevelDown(level);
        }

        ChangeMonstersMight(-level);
    }

    public int GetMonsterForm(Monster monster)
    {
        var tempMonster = GetMonster(monster);

        if (tempMonster != null)
            return tempMonster.FormCounter;

        return 0;
    }

    public void PickUpCurrency(int amount)
    {
        CurrencyPickedUp?.Invoke(amount);
    }

    public IEnumerable<Monster> GetAllMonsters()
    {
        var monsters = from MonsterPlace monsterPlace in _monsterPlaces
                       where monsterPlace.Monster != null
                       select monsterPlace.Monster;

        return monsters;
    }

    private void ChangeMonstersMight(int level)
    {
        _monstersMight+= level;

        if (_monstersMight <= 0)
            _monstersMight = 0;

        MightChanged?.Invoke(_monstersMight, level);
    }

    private void SetMonsterToPlace(Monster monsterType, MonsterPlace monsterPlace, int level)
    {
        var monster = Instantiate(monsterType, monsterPlace.transform);
        monsterPlace.Take(monster);

        ChangeMonstersMight(level);
        _monstersAnimatorHandler.AddAnimator(monster.MonsterAnimator);
    }

    private bool CanMerge(Monster monster, MonsterPlace place)
    {       
        return place.Monster != null && place.Monster.GetType() == monster.GetType();
    }

    private Monster GetMonster(Monster monster)
    {
        var existMonster = _monsterPlaces.FirstOrDefault(monsterPlace => monsterPlace.Monster != null && monsterPlace.Monster.GetType() == monster.GetType());

        if (existMonster != default)
            return existMonster.Monster;

        return null;
    }
}
