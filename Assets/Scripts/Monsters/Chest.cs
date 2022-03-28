using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Chest : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Push(float pushForce)
    {
        _rigidbody.AddForce((-transform.forward+ transform.right*0.25f).normalized * pushForce, ForceMode.VelocityChange);
    }
}
