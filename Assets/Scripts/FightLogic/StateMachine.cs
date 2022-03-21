using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine: MonoBehaviour
{
    [SerializeField] private float _agroRadius;
    [SerializeField] private float _attackRadius;
    [SerializeField] private AttackBehavior _attackBehavior;
    [SerializeField] private Monster _self;
    [SerializeField] private LayerMask _monsterLayerMask;

    private Monster _target;
    private StateBehavior _currentBehavior;
    private MoveState _moveState;
    private AttackState _attackState;
    private IdleState _idleState;

    public event Action<StateBehavior> StateChanged;

    private void Awake()
    {
        _moveState = new MoveState();
        _attackState = new AttackState(_attackBehavior);
        _idleState = new IdleState();
        _currentBehavior = _idleState;
    }

    private void FixedUpdate()
    {
        if(TryFindTarget(_agroRadius, out Monster monster))
        {
            _target = monster;

            if (TryFindTarget(_attackRadius, out monster))
            {
                _target = monster;

                SetBehavior(_attackState);

                if(monster.IsAllive)
                    _currentBehavior.Act(_self, _target);

                return;
            }

            SetBehavior(_moveState);
        }
        else
        {
            SetBehavior(_idleState);
        }

        _currentBehavior.Act(_self, _target);
    }

    public void SetBehavior(StateBehavior stateBehavior)
    {
        _currentBehavior = stateBehavior;
        StateChanged?.Invoke(_currentBehavior);
    }

    private bool TryFindTarget(float range, out Monster monster)
    {
        Collider[] monstersColliders = Physics.OverlapSphere(transform.position, range, _monsterLayerMask);

        monster = GetClosestTargetCollider(monstersColliders);

        return monster != null;
    }

    private Monster GetClosestTargetCollider(Collider[] monstersColliders)
    {
        Monster target = null;
        float minDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (var monsterCollider in monstersColliders)
        {
            if (monsterCollider.TryGetComponent(out Monster tempEnemy) && tempEnemy != _self && tempEnemy.IsAllive)
            {
                float dist = Vector3.Distance(tempEnemy.transform.position, currentPosition);

                if (dist < minDistance)
                {
                    target = tempEnemy;
                    minDistance = dist;
                }
            }
        }

        return target;
    }
}
