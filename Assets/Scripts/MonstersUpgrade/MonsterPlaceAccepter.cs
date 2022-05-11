using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MonsterPlace))]
public class MonsterPlaceAccepter : MonoBehaviour, IMonsterHolder
{
    [SerializeField] private MonstersHandler _monstersHandler;

    private MonsterPlace _monsterPlace;
    private Monster _monster;
    public Rotator _rotator { get; private set; }
    public Monster Monster => _monster;

    public bool TryAcquireMonster(Monster monster)
    {
        bool isPlaceFree = _monsterPlace.Monster == null;

        if (isPlaceFree)
        {
            _monster = monster;
            _monstersHandler.TrySetMonsterToPlace(_monster, _monsterPlace, _monster.Level);

            DisableRotator();
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
            _monstersHandler.DecreasseCounter(_monster.Level);
            monster = _monster;
            _monster = null;
        }

        return _isMonsterSetted;
    }

    public void EnableRotator()
    {
        if(_rotator != null)
            _rotator.enabled = true;
    }

    public void Open()
    {
        _monsterPlace.EnableCollider();
    }

    public void Hide()
    {
        _monsterPlace.DeActivate();
    }

    private void DisableRotator()
    {
        _rotator = _monster.GetComponentInChildren<Rotator>();
        _rotator.enabled = false;
    }

    public void Activate()
    {
        if (_monsterPlace == null)
            _monsterPlace = GetComponent<MonsterPlace>();

        gameObject.SetActive(true);
    }
}
