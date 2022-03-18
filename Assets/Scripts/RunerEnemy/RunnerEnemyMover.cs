using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RunnerEnemyMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    
    private Rigidbody _rigidbody;
    private RunnerEnemyTrigger _enemyTrigger;
    private Vector3 _direction;
    private bool _playerInSight;

    public event Action Moving;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _enemyTrigger = GetComponentInChildren<RunnerEnemyTrigger>();
        _enemyTrigger.PlayerInTriggerZone += Move;
        _enemyTrigger.PlayerOutOfTriggerZone += StopMove;
    }

    private void OnDisable()
    {
        _enemyTrigger.PlayerInTriggerZone -= Move;
        _enemyTrigger.PlayerOutOfTriggerZone -= StopMove;
    }

    private void FixedUpdate()
    {
        if (_playerInSight)
        {
            _rigidbody.MovePosition(transform.position + _direction * _speed * Time.deltaTime);
            Rotate(_direction);
        }
    }

    private void Rotate(Vector3 direction)
    {
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        lookRotation.x = 0;
        lookRotation.z = 0;

        transform.rotation = lookRotation;
    }

    private void Move(Vector3 direction)
    {
        _playerInSight = true;
        _direction = direction;
        Moving?.Invoke(); 
    }

    private void StopMove()
    {
        _playerInSight = false;
    }

    public void Disable()
    {
        this.enabled = false;
    }
}
