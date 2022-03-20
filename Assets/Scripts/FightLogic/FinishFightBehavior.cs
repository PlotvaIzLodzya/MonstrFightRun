using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishFightBehavior : AttackBehavior
{
    public override void Fight(Monster self, Monster monster)
    {
        self.SetTarget(monster);
        self.Attack();
    }
}
