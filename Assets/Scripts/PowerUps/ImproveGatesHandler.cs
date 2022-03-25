using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class ImproveGatesHandler : MonoBehaviour
{
    [SerializeField] private MonsterList _monsters;
    [SerializeField] private PositionAlongPath _positionAlongPath = new PositionAlongPath();

    private Gate[] _gates;
    private PathCreator _pathCreator;
    private void Awake()
    {
        _pathCreator = FindObjectOfType<PathCreator>();
        _gates = GetComponentsInChildren<Gate>();
        transform.position = _positionAlongPath.GetPosition(transform.position, _pathCreator.path);
        transform.rotation = _positionAlongPath.GetRotation(transform.position, _pathCreator.path);
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
