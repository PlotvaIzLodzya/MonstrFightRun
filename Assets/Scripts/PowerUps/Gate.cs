using System;
using UnityEngine;

public class Gate : PowerUp
{
    [SerializeField] private GateIcon _gateIcon;

    private Monster _monster;
    private bool _isActivated;

    public event Action GateActivated;

    public void SetMonster(Monster monster)
    {
        _monster = monster;
        _gateIcon.CreateIcon(_monster);
    }

    public override void Use(MonstersHandler monstersHandler)
    {
        if (_isActivated)
            return;

        _isActivated = monstersHandler.TrySetMonsterToPlace(_monster, 10);

        if (_isActivated)
            GateActivated?.Invoke();
    }

    public void Disable()
    {
        _isActivated = true;
    }
}
