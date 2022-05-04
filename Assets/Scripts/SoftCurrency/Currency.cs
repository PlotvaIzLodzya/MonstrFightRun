using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Currency : Interactable
{
    [SerializeField] private int _amount;

    public override void Use(MonsterPlace monsterPlace)
    {
        monsterPlace.PickUpCurrency(_amount);
        Destroy(gameObject);
    }
}
