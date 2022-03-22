using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private MonstersHandler _monstersHandler;
    [SerializeField] private PlayerDeathHandler _deathHandler;

    public int Might => _monstersHandler.MonsterMight;
    private int _counter;

    public void RaiseMight(int level)
    {
        _monstersHandler.LevelUpAllMonster(level);
    }

    public void Die()
    {
        _deathHandler.Die();
    }

    public void KillAllMonsters()
    {
        _monstersHandler.KillAllMonsters();
    }

    public void OnMonsterDie()
    {
        _counter++;

        if (_counter >= _monstersHandler.MonsterCounter)
            Die();
    }
}
