using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishFightBehavior : AttackBehavior
{
    private Coroutine _coroutine;

    public override void Fight(Monster self, Monster monster)
    {
        if(_coroutine == null && monster.IsAllive)
            _coroutine = StartCoroutine(Attack(self, monster));
    }

    private IEnumerator Attack(Monster self, Monster monster)
    {
        while (monster.IsAllive)
        {
            self.SetTarget(monster);
            self.Attack();

            yield return new WaitForSeconds(self.AttackDelay);
        }

        _coroutine = null;
    }
}
