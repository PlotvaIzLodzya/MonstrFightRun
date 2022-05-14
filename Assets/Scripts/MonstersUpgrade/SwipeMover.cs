using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeMover : MonoBehaviour
{
    private Vector3 _rightCorner;
    private Vector3 _leftCorner;

    private float _leftOffset;
    private float _rightOffset;

    private void Update()
    {
        if (transform.localPosition.x < _rightCorner.x)
        {
            transform.localPosition = new Vector3(_leftOffset + transform.localPosition.x, transform.localPosition.y, transform.localPosition.z) ;
        }

        if (transform.localPosition.x > _leftCorner.x)
        {
            transform.localPosition = new Vector3(transform.localPosition.x + _rightOffset, transform.localPosition.y, transform.localPosition.z);
        }
    }

    public void Init(Transform rightCorner, Transform leftCorner, float spacing)
    {
        _rightCorner = rightCorner.localPosition;
        _leftCorner = leftCorner.localPosition;

        float distance = Mathf.Abs(_leftCorner.x) + Mathf.Abs(_leftCorner.x);

        _leftOffset = distance + spacing;
        _rightOffset = -distance - spacing;
    }

    public void Move(float speed)
    {
        transform.localPosition += (Vector3.left * Time.deltaTime * speed);
    }
}
