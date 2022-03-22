using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishFightBehavior : AttackBehavior
{
    private Coroutine _coroutine;

    public override void Fight(Monster self, Monster monster)
    {
        self.SetTarget(monster);
        self.MonsterAnimator.TriggerAttackAnimation();
    }
}
