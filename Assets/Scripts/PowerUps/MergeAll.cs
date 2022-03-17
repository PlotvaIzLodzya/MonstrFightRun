using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MergeAll : Usable
{
    public override void Use(MonstersHandler monstersHandler)
    {
        Debug.Log("Bonjour");
        monstersHandler.MergeAllMonster();
    }

}
