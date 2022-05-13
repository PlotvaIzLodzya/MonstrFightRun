using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMonsterReward : DailyRewardBehaviour
{
    public override void Acquire()
    {
        FindObjectOfType<MonsterShop>().OpenNextCell();
    }
}
