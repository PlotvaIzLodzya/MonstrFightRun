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
    private string _fightAttack = "FightAttack";
    private string _attack = "Attack";
    private const string Idle = "Idle";

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

    public void TriggerAttackAnimation()
    {
        _animator.SetTrigger(_attack);

        StartCoroutine(ResetTrigger(_attack));
    }

    public void RunAnimation()
    {
        _animator.SetBool(Run, true);
    }

    public void DieAnimation()
    {
        if(_isDead == false)
        {
            _animator.SetBool(Idle, false);
            _animator.SetBool(Die, true);
            _animator.SetLayerWeight(1, 0);
        }

        _isDead = true;
    }

    public void IdleAnimation()
    {
        _animator.SetBool(Idle, true);
    }

    public void ToFightTransition()
    {
        IdleAnimation();
        _attack = _fightAttack;
        _animator.SetLayerWeight(1, 0);
    }

    private IEnumerator ResetTrigger(string name)
    {
        yield return new WaitForSeconds(0.01f);

        _animator.ResetTrigger(name);
    }

    private IEnumerator ResetWeight()
    {
        yield return new WaitForSeconds(0.01f);

        _animator.SetLayerWeight(1,0);
    }

    private void OnStateChanged(StateBehavior stateBehavior)
    {
        if (_isDead)
            return;

        if (stateBehavior is MoveState)
        {
            RunAnimation();
        }
        else if (stateBehavior is AttackState)
        {
            IdleAnimation();
            _animator.SetBool(Run, false);
        }
        else if (stateBehavior is IdleState)
        {
            IdleAnimation();
            _animator.SetBool(Run, false);
        }
    }
}
