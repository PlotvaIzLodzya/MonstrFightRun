using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunerFight: AttackBehavior
{
    [SerializeField] private Chest _chest;

    public override void Fight(Monster enemyMonster, Monster monsterOfPlayer)
    {
        Player player = monsterOfPlayer.transform.root.GetComponent<Player>();
        bool iSPlayerWin = player.Might >= enemyMonster.Level;

        if (iSPlayerWin)
        {
            player.RaiseMight(enemyMonster.Level);
            enemyMonster.Die();
            _chest.Push(50f);
        }
        else
        {
            player.Die();
        }
    }
}
