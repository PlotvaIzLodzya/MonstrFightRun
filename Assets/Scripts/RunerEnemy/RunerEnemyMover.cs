using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RunerEnemyMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    
    private Rigidbody _rigidbody;
    private RunerEnemyTrigger _enemyTrigger;
    private Vector3 _direction;
    private bool _playerInSight;

    public event Action Moving;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _enemyTrigger = GetComponentInChildren<RunerEnemyTrigger>();
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

    public void MoveForward()
    {
        StartCoroutine(MoveForwardAimation());
    }

    private IEnumerator MoveForwardAimation()
    {
        float timePassed = 0;

        while(timePassed < 0.1f)
        {
            _rigidbody.MovePosition(transform.position + (transform.right+transform.forward) * _speed*2f * Time.deltaTime);
            timePassed += Time.deltaTime;

            yield return null;
        }
    }

    public void Disable()
    {
        this.enabled = false;
    }
}
