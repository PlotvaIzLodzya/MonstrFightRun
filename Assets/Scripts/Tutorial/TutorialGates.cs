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
        _gates[0].SetMonster(_leftGate.GetRandomMonster());
        _gates[1].SetMonster(_rightGate.GetRandomMonster());
    }
}
