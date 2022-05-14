using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterSwipeZone : MonoBehaviour
{
    [SerializeField] private SwipeZone _swipeZone;

    private float _speedDivider = 2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out SwipeMover swipeMover))
        {
            _swipeZone.SlowDown(_speedDivider, swipeMover);
        }
    }
}
