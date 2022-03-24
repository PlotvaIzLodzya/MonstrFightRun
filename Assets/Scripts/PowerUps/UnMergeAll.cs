using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnMergeAll : PowerUp
{
    private HeightHandler _heightHandler = new HeightHandler();

    private void Start()
    {
        transform.position = _heightHandler.GetHeight(transform.position, 0.85f);
    }

    public override void Use(MonstersHandler monstersHandler)
    {
        monstersHandler.LevelDownAllMonster();
    }
}
