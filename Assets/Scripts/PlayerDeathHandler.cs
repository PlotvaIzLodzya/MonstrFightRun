using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathHandler : MonoBehaviour, IDeathBehavior
{
    public event Action PlayerLost;

    public void Die()
    {
        PlayerLost?.Invoke();
    }
}
