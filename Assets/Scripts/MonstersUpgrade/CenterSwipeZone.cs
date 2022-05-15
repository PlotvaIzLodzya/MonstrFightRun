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

        if(other.TryGetComponent(out MonsterCell monsterCell))
        {
            if(ViewState.IsViewed)
                monsterCell.TryOpenInfoPanel(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out MonsterCell monsterCell))
        {
            monsterCell.CloseInfoPanel(false);
        }
    }
}
