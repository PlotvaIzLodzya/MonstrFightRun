using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Currency : PowerUp
{
    [SerializeField] private int _amount;

    public override void Use(MonstersHandler monstersHandler)
    {
        monstersHandler.PickUpCurrency(_amount);
        Destroy(gameObject);
    }
}
