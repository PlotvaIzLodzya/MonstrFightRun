using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RunerFight: AttackBehavior
{
    [SerializeField] private Chest _chest;

    private Monster _monsterOfPlayer;
    private Player _player;
    private Monster _enemyMonster;

    public override void Fight(Monster enemyMonster, Monster monsterOfPlayer)
    {
        _enemyMonster = enemyMonster;
        _player = monsterOfPlayer.transform.root.GetComponent<Player>();
        MonstersAnimatorHandler playerMonsterAnimatorHandler = monsterOfPlayer.transform.root.GetComponent<MonstersAnimatorHandler>();
        _monsterOfPlayer = monsterOfPlayer;

        bool isPlayerWin = _player.Might >= enemyMonster.Level;

        enemyMonster.MonsterAnimator.TriggerAttackAnimation();
        playerMonsterAnimatorHandler.TriggerAttackAnimation();

        if (isPlayerWin)
        {
            monsterOfPlayer.SetTarget(enemyMonster);
            monsterOfPlayer.DealtDamage += Push;
            _enemyMonster.Die();
        }
        else
        {
            enemyMonster.DealtDamage += Die;
            enemyMonster.SetTarget(monsterOfPlayer);
        }
    }

    private void Push()
    {
        _chest.Push(50f);
        _player.RaiseMight(_enemyMonster.Level);
        _monsterOfPlayer.DealtDamage -= Push;
    }

    private void Die()
    {
        _player.Die();
        _player.KillAllMonsters();
        _enemyMonster.DealtDamage -= Die;
    }
}
