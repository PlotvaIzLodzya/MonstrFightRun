using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPoolCreator : MonoBehaviour
{
    [SerializeField] private MonsterList _monsterList;

    private ImproveGatesHandler[] _improveGatesHandlers;
    private void Awake()
    {
        _improveGatesHandlers = FindObjectsOfType<ImproveGatesHandler>();

        _monsterList.CreateMonsterPool();

        foreach (var improveGatesHandler in _improveGatesHandlers)
        {
            improveGatesHandler.Init(_monsterList);
            improveGatesHandler.PlaceMonster();
        }
    }
}
