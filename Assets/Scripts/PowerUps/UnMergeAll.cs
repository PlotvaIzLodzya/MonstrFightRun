using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnMergeAll : PowerUp
{
    public override void Use(MonstersHandler monstersHandler)
    {
        monstersHandler.LevelDownAllMonster();
    }
}
