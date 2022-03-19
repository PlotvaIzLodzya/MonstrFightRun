using System;
using UnityEngine;

public class Gate : PowerUp
{
    [SerializeField] private MonsterList _monsters;
    [SerializeField] private GateIcon _gateIcon;

    private Monster _monster;
    private bool _isActivated;

    public event Action GateActivated;

    private void Start()
    {
        _monster = _monsters.GetRandomMonster();
        _gateIcon.CreateIcon(_monster);
    }

    public override void Use(MonstersHandler monstersHandler)
    {
        if (_isActivated)
            return;

        monstersHandler.TrySetMonsterToPlace(_monster);
        GateActivated?.Invoke();
    }

    public void Disable()
    {
        _isActivated = true;
    }
}
