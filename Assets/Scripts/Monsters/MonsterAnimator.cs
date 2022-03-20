using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAnimator : MonoBehaviour
{
    [SerializeField] private FormsHandler _formsHandler;

    private Animator _animator => _formsHandler.CurrentFormAnimator;

    private const string Run = "Run";
    private const string Die = "Die";
    private const string Attack = "Attack";

    private void OnEnable()
    {
        _formsHandler.FormChanged += RunAnimation;
    }

    private void OnDisable()
    {
        _formsHandler.FormChanged -= RunAnimation;
    }

    public void AttackAnimation()
    {
        _animator.SetBool(Attack, true);
    }

    public void RunAnimation()
    {
        _animator.SetBool(Attack, false);
        _animator.SetBool(Run, true);
    }

    public void DieAnimation()
    {
        _animator.SetTrigger(Die);
    }
}
