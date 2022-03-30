using System;
using RunnerMovementSystem.Examples;
using UnityEngine;

public class PlayerDeathHandler : MonoBehaviour, IDeathBehavior
{
    [SerializeField] private MonstersAnimatorHandler _animatorHandler;
    [SerializeField] private MouseInput _mouseInput;
    [SerializeField] private UIHandler _uIHandler;

    public event Action PlayerLost;

    private bool _isDead;
    public void Die()
    {
        if (_isDead)
            return;

        PlayerLost?.Invoke();
        _mouseInput.enabled = false;
        _animatorHandler.SetDeathAnimation();
        _uIHandler.SwitchState();

        _isDead = true;
    }
}
