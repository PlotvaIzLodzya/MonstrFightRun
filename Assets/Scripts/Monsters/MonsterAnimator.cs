using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAnimator : MonoBehaviour
{
    [SerializeField] private FormsHandler _formsHandler;
    [SerializeField] private StateMachine _stateMachine;

    private Animator _animator => _formsHandler.CurrentFormAnimator;

    private const string Run = "Run";
    private const string Die = "Die";
    private const string Attack = "Attack";

    private bool _isDead;

    private void OnEnable()
    {
        _formsHandler.FormChanged += RunAnimation;
        _stateMachine.StateChanged += OnStateChanged;
    }

    private void OnDisable()
    {
        _formsHandler.FormChanged -= RunAnimation;
        _stateMachine.StateChanged -= OnStateChanged;
    }

    public void AttackAnimation()
    {
        _animator.SetTrigger(Attack);

        StartCoroutine(ResetTrigger(Attack));
    }

    public void RunAnimation()
    {
        _animator.SetBool(Run, true);
    }

    public void DieAnimation()
    {
        if(_isDead == false)
            _animator.SetTrigger(Die);

        _isDead = true;
    }

    public void IdleAnimation()
    {

    }

    private IEnumerator ResetTrigger(string name)
    {
        yield return new WaitForSeconds(0.01f);

        _animator.ResetTrigger(name);
    }

    private void OnStateChanged(StateBehavior stateBehavior)
    {
        if (stateBehavior is MoveState)
            RunAnimation();
        else if (stateBehavior is AttackState)
            AttackAnimation();
        else if (stateBehavior is IdleState)
            IdleAnimation();
    }
}
