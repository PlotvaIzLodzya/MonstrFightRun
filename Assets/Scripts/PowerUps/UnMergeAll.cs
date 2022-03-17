using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnMergeAll : Usable
{
    public override void Use(MonstersHandler monstersHandler)
    {
        monstersHandler.UnMergeAllMonster();
    }
}
