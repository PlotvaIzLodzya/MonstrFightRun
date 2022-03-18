using System;
using RunnerMovementSystem.Examples;
using UnityEngine;

public class PlayerDeathHandler : MonoBehaviour, IDeathBehavior
{
    [SerializeField] private MonstersAnimatorHandler _animatorHandler;
    [SerializeField] private MouseInput _mouseInput;

    public event Action PlayerLost;

    public void Die()
    {
        PlayerLost?.Invoke();
        _mouseInput.enabled = false;
        _animatorHandler.SetDeathAnimation();
    }
}
