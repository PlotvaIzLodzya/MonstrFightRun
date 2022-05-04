using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class MonsterPlace : MonoBehaviour, IJumpable
{
    [HideInInspector] public int Position;

    private bool _battleMode;
    private BoxCollider _boxCollider;
    private JumpAnimation _jumpAnimation = new JumpAnimation();
    public bool IsTaken { get; private set; }
    public Monster Monster { get; private set; }

    public event Action<MonsterPlace> PlaceFree;
    public event Action<int> CurrencyPickedUp;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();
    }

    public void Take(Monster monster)
    {
        if(_boxCollider == null)
            _boxCollider = GetComponent<BoxCollider>();

        Monster = monster;
        Monster.LevelUp(1);
        Monster.Died += MonsterDied;
        _boxCollider.enabled = true;
        IsTaken = true;
    }

    public void MonsterDied()
    {
        FindObjectOfType<MonsterPoolCreator>().ResetMonster(Monster);
        Free();

       if(_battleMode == false)
            PlaceFree?.Invoke(this);
    }

    public void Free()
    {
        Monster.Died -= MonsterDied;
        IsTaken = false;
        Monster = null;
        _boxCollider.enabled = false;
    }

    public void Jump(AnimationCurve animationCurve)
    {
        StartCoroutine(_jumpAnimation.Play(animationCurve, transform));
    }

    public void PickUpCurrency(int amount)
    {
        CurrencyPickedUp?.Invoke(amount);
    }

    public void BattleMode()
    {
        _battleMode = true;
    }
}
