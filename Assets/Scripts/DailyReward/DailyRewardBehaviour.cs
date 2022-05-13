using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DailyRewardBehaviour : MonoBehaviour
{
    public abstract void Acquire();

    private void Expire()
    {
        gameObject.SetActive(false);
    }
}
