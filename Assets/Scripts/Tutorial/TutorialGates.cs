using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialGates : MonoBehaviour
{
    [SerializeField] private MonsterList _leftGate;
    [SerializeField] private MonsterList _rightGate;

    private Gate[] _gates;

    public void SetGate()
    {
        _gates = GetComponentsInChildren<Gate>();

        _leftGate.TryGetRandomMonster(out Monster monster);
        _gates[0].SetMonster(monster);

        _leftGate.TryGetRandomMonster(out monster);
        _gates[1].SetMonster(monster);
    }
}
