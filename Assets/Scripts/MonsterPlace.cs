using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPlace : MonoBehaviour
{
    public bool IsTaken { get; private set; }
    public Monster Monster { get; private set; }

    public void Take(Monster monster)
    {
        Monster = monster;
        Monster.Merge(1);
        IsTaken = true;
    }

    public void Free()
    {
        IsTaken = false;
    }
}
