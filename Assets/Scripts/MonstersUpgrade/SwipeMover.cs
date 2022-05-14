using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeMover : MonoBehaviour
{
    [SerializeField] private SwipeZone _clickHandler;
    [SerializeField] private Transform _rightCorner;
    [SerializeField] private Transform _leftCorner;

    public float Speed => _clickHandler.Speed;

    private void Update()
    {
        transform.localPosition += (Vector3.left * Time.deltaTime * _clickHandler.Speed);

        if (transform.localPosition.x < _rightCorner.localPosition.x)
        {
            transform.localPosition = _leftCorner.localPosition;
        }

        if (transform.localPosition.x > _leftCorner.localPosition.x)
        {
            transform.localPosition = _rightCorner.localPosition;
        }
    }

    public void SlowDown()
    {
        _clickHandler.SlowDown(5);
    }

    public void OnStop(float offset)
    {
        _clickHandler.Centration(offset);
    }
}
