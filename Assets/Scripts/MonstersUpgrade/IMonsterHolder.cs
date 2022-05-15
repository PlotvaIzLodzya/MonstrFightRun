using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IMonsterHolder
{
    public bool TryAcquireMonster(Monster monster);

    public bool TryGrab(out Monster monster);

    public void Activate();

    public void Open();

    public void Hide();
}
