using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RunnerMovementSystem;
using RunnerMovementSystem.Examples;

[RequireComponent(typeof(MovementSystem), typeof(MouseInput))]
public class CameraTarget : MonoBehaviour
{
    private MovementSystem _movementSystem;
    private MouseInput _mouseInput;
    private PlayerDeathHandler _playerDeathHandler;

    private void Awake()
    {
        _playerDeathHandler = FindObjectOfType<PlayerDeathHandler>();
        Error.CheckOnNull(_playerDeathHandler, nameof(PlayerDeathHandler));

        _mouseInput = GetComponent<MouseInput>();

        _movementSystem = GetComponent<MovementSystem>();
    }

    private void OnEnable()
    {
        _playerDeathHandler.PlayerLost += DisableControl;
    }

    private void OnDisable()
    {
        _playerDeathHandler.PlayerLost -= DisableControl;
    }
    private void DisableControl()
    {
        _movementSystem.enabled = false;
        _playerDeathHandler.PlayerLost -= DisableControl;
    }
}
