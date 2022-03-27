using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class ImproveGatesHandler : MonoBehaviour
{
    [SerializeField] private MonsterList _monsters;

    private Gate[] _gates;
    private void Awake()
    {
        _gates = GetComponentsInChildren<Gate>();
        PlaceMonster();
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

    private void PlaceMonster()
    {
        Monster previousMonster = null;
        Monster monster = _monsters.GetRandomMonster();

        foreach (var gate in _gates)
        {
            while (monster == previousMonster)
            {
                monster = _monsters.GetRandomMonster();
            }

            gate.SetMonster(monster);
            previousMonster = monster;
        }
    }
}
