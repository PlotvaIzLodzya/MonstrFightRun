using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MonsterPlace))]
public class MonsterPlaceAccepter : MonoBehaviour, IMonsterHolder
{
    [SerializeField] private MonstersHandler _monstersHandler;

    private MonsterPlace _monsterPlace;
    private Monster _monster;

    public Monster Monster => _monster;

    private void Awake()
    {
        _monsterPlace = GetComponent<MonsterPlace>();
    }

    public bool TryAcquireMonster(Monster monster)
    {
        bool isPlaceFree = _monsterPlace.Monster == null;

        if (isPlaceFree)
        {
            _monster = monster;
            _monstersHandler.TrySetMonsterToPlace(_monster, _monsterPlace, 10);
        }

        return isPlaceFree;
    }

    public bool Grab(out Monster monster)
    {
        bool _isMonsterSetted = _monster != null;
        monster = null;

        if (_isMonsterSetted)
        {
            _monsterPlace.Free(true);
            _monstersHandler.DecreasseCounter();
            monster = _monster;
        }

        return _isMonsterSetted;
    }
}