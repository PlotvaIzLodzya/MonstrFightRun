using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private MonstersHandler _monstersHandler;
    [SerializeField] private PlayerDeathHandler _deathHandler;

    public int Might => _monstersHandler.MonsterMight;

    public void RaiseMight(int level)
    {
        _monstersHandler.MergeAllMonster(level);
    }

    public void Die()
    {
        _deathHandler.Die();
        Debug.Log("hi");
    }
}
