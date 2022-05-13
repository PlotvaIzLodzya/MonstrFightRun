using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMonsterReward : DailyRewardBehaviour
{
    [SerializeField] private Monster _monster;

    public override void Acquire()
    {
        FindObjectOfType<MonsterShop>().OpenCellWithMonster(_monster);
    }
}
