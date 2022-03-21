using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunerFight: AttackBehavior
{
    [SerializeField] private Chest _chest;

    private Monster _monsterOfPlayer;

    public override void Fight(Monster enemyMonster, Monster monsterOfPlayer)
    {
        Player player = monsterOfPlayer.transform.root.GetComponent<Player>();
        MonstersAnimatorHandler monstersAnimatorHandler = monsterOfPlayer.transform.root.GetComponent<MonstersAnimatorHandler>();
        _monsterOfPlayer = monsterOfPlayer;

        bool isPlayerWin = player.Might >= enemyMonster.Level;
        monstersAnimatorHandler.TriggerAttackAnimation();

        if (isPlayerWin)
        {
            player.RaiseMight(enemyMonster.Level);
            enemyMonster.Die();
            monsterOfPlayer.SetTarget(enemyMonster);
            monsterOfPlayer.DealtDamage += Push;
        }
        else
        {
            player.Die();
        }
    }

    private void Push()
    {
        Debug.Log("hi");
        _chest.Push(50f);
        _monsterOfPlayer.DealtDamage -= Push;
    }
}
