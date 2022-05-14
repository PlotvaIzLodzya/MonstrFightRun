using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterSwipeZone : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out SwipeMover swipeMover))
        {
            if (Mathf.Abs(swipeMover.transform.localPosition.x) < 0.2f)
                swipeMover.SlowDown();


            if(swipeMover.Speed == 0 && swipeMover.transform.localPosition.x != 0)
            {
                swipeMover.OnStop(-swipeMover.transform.localPosition.x);
            }
        }
    }
}
