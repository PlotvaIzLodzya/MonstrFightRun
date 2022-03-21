using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImproveGatesHandler : MonoBehaviour
{
    private Gate[] _gates;

    private void Awake()
    {
        _gates = GetComponentsInChildren<Gate>();
    }

    private void OnEnable()
    {
        foreach (var gate in _gates)
        {
            gate.GateActivated += DisableGates;
        }
    }

    private void OnDisable()
    {
        foreach (var gate in _gates)
        {
            gate.GateActivated -= DisableGates;
        }
    }

    public void DisableGates()
    {
        foreach (var gate in _gates)
        {
            gate.Disable();
            gate.GateActivated -= DisableGates;
        }
    }
}
