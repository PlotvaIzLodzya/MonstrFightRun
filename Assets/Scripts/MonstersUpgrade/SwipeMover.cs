using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeMover : MonoBehaviour
{
    [SerializeField] private ClickHandler _clickHandler;
    [SerializeField] private Transform _rightCorner;
    [SerializeField] private Transform _leftCorner;

    private void FixedUpdate()
    {
        transform.localPosition += (Vector3.left * Time.deltaTime * _clickHandler.Speed);

        if (transform.localPosition.x < _rightCorner.localPosition.x)
        {
            _clickHandler.SlowDown();
            transform.localPosition = _leftCorner.localPosition;
        }

        if (transform.localPosition.x > _leftCorner.localPosition.x)
        {
            _clickHandler.SlowDown();
            transform.localPosition = _rightCorner.localPosition;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CenterSwipeZone centerSwipeZone))
        {
            //_clickHandler.SlowDown();
        }
    }
}
