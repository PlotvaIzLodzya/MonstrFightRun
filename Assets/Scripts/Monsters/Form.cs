using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Form : MonoBehaviour
{
    [SerializeField] private Monster _monster;
    [SerializeField] private bool _isBoss;

    private Animator _animator;

    public Animator FormAnimator => _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        if (_isBoss)
            _monster.MonsterAnimator.ToFightTransition();
    }

    private void RangeAttack()
    {
        if (_monster.IsAllive)
        {
            _monster.DealDamage();
        }
    }

    private void Run() { }
}
