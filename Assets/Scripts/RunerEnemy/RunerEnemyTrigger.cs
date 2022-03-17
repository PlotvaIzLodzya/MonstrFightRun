using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class RunerEnemyTrigger : MonoBehaviour
{
    [SerializeField] private float _triggerRadius;

    private SphereCollider _sphereCollider;

    public event Action<Vector3> PlayerInTriggerZone;
    public event Action PlayerOutOfTriggerZone;

    private void Start()
    {
        _sphereCollider = GetComponent<SphereCollider>();
        _sphereCollider.radius = _triggerRadius;
        _sphereCollider.isTrigger = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            PlayerInTriggerZone?.Invoke(direction);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            PlayerOutOfTriggerZone?.Invoke();
        }
    }
}
