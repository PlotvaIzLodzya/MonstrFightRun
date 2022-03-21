using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDeathHandler : MonoBehaviour, IDeathBehavior
{
    [SerializeField] private StateMachine _stateMachine;
    [SerializeField] private MonsterAnimator _monsterAnimator;

    private Player _player;

    public void Die()
    {
        _stateMachine.enabled = false;
        _monsterAnimator.DieAnimation();
        gameObject.layer = 9;

        _player = transform.root.GetComponent<Player>();

        if (_player != null)
            _player.OnMonsterDie();
    }
}
