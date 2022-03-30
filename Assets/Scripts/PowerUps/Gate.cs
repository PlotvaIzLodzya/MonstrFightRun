using System;
using UnityEngine;

public class Gate : PowerUp
{
    [SerializeField] private GateIcon _gateIcon;

    public Monster Monster { get; private set; }
    private bool _isActivated;
    private const int _level = 10;

    public event Action GateActivated;
    public event Action<Monster, Gate> NeedAnotherMonster;

    private void OnEnable()
    {
        _gateIcon.NeedAnotherMonster += OnNeedAnotherMonster;
    }

    private void OnDisable()
    {
        _gateIcon.NeedAnotherMonster -= OnNeedAnotherMonster;
    }

    public void SetMonster(Monster monster)
    {
        Monster = monster;
        _gateIcon.CreateIcon(Monster);
    }

    public void PlacePowerUp(PowerUp powerUp)
    {
        _gateIcon.CreateIcon(powerUp);
    }

    public void ReplaceMonster(Monster monster)
    {
        Monster = monster;
        _gateIcon.ReplaceIcon(monster);
    }

    public void OnNeedAnotherMonster(Monster monster)
    {
        NeedAnotherMonster?.Invoke(monster, this);
    }

    public override void Use(MonstersHandler monstersHandler)
    {
        if (_isActivated)
            return;

        _isActivated = monstersHandler.TrySetMonsterToPlace(Monster, _level);

        if (_isActivated)
            GateActivated?.Invoke();
    }

    public void Disable()
    {
        _isActivated = true;
    }
}
