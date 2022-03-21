using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    [SerializeField] private float _triggerRadius;
    [SerializeField] private StateMachine _stateMachine;

    private MoveState _moveState;

    public float TriggerRadius => _triggerRadius;

    private SphereCollider _sphereCollider;

    private void Start()
    {
        _sphereCollider = GetComponent<SphereCollider>();
        _sphereCollider.radius = _triggerRadius;
        _sphereCollider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Monster monster))
        {
            _stateMachine.SetBehavior(_moveState);
        }
    }
}
