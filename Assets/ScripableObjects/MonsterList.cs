using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

[CreateAssetMenu(menuName = "Scriptable Objects/MonsterList")]
public class MonsterList : ScriptableObject
{
    [SerializeField] private List<Monster> _monsters;
    [SerializeField] private PowerUp _powerUp;

    private List<Monster> _monsterPool = new List<Monster>();

    public void CreateMonsterPool()
    {
        if(_monsterPool.Count>0)
            _monsterPool.RemoveRange(0, _monsterPool.Count);

        foreach (var monster in _monsters)
        {
            _monsterPool.Add(monster);
        }
    }

    public bool TryGetRandomMonster(out Monster monster)
    {

        if (_monsterPool.Count > 0)
        {
            int index = UnityEngine.Random.Range(0, _monsterPool.Count);
            monster = _monsterPool[index];

            return true;
        }
        else
        {
            monster = null;
            return false;
        }

    }

    public PowerUp GetPowerUp()
    {
        return _powerUp;
    }

    public bool TryGetRandomMonsterExcept(Monster monster, out Monster newMonster)
    {
        newMonster = _monsterPool.FirstOrDefault(tempMonster => tempMonster.GetType() != monster.GetType());

        return newMonster != default;
    }

    public void RemoveMonster(Monster monster)
    {
        var monsterToRemove = _monsterPool.FirstOrDefault(currentMonster => currentMonster.GetType() == monster.GetType());

        _monsterPool.Remove(monsterToRemove);
    }
}
