using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Form : MonoBehaviour
{
    [SerializeField] private Monster _monster;

    private Animator _animator;

    public Animator FormAnimator => _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnAttack()
    {
        if (_monster.IsAllive)
        {
            _monster.DealDamage();
        }
    }
}
