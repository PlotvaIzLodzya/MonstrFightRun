using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class MergeAll : PowerUp
{
    public override void Use(MonstersHandler monstersHandler)
    {
        monstersHandler.LevelUpAllMonster(1);
    }
}
