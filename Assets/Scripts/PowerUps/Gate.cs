using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : PowerUp
{
    [SerializeField] private MonsterList _monsters;
    [SerializeField] private GateIcon _gateIcon;

    private Monster _monster;

    private void Start()
    {
        _monster = _monsters.GetRandomMonster();
        _gateIcon.CreateIcon(_monster);
    }

    public override void Use(MonstersHandler monstersHandler)
    {
        monstersHandler.TrySetMonsterToPlace(_monster);
    }
}
