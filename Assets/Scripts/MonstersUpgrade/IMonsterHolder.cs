using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMonsterHolder
{
    public bool TryAcquireMonster(Monster monster);

    public bool Grab(out Monster monster);
}