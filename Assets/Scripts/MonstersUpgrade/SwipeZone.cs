using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeZone : MonoBehaviour
{
    [SerializeField] private float _sensitivity;

    private SwipeMover[] _swipeMovers;
    private bool _centred;
    private float _threshold = 6f;
    private float _stopSpeed = 5f;
    public bool Clicked { get; private set; }

    public float Speed { get; private set; }

    private void Awake()
    {
        _swipeMovers = GetComponentsInChildren<SwipeMover>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
            Clicked = false;

        if (Clicked)
        {
            Speed = Input.GetAxis("Mouse X") * _sensitivity;
            Speed = Mathf.Clamp(Speed, -30, 30);

            if (Mathf.Abs(Speed) > _threshold)
                _centred = false;
        }

        if(Speed != 0 && _centred == false)
        {
            foreach (var swipeMover in _swipeMovers)
            {
                swipeMover.Move();
            }
        }
    }

    private void OnMouseDown()
    {
        Clicked = true;
    }

    public void SlowDown(float slowCoeficient)
    {
        float speedValue = Mathf.Abs(Speed);

        if (Speed > 0)
            Speed -= speedValue / slowCoeficient;

        if (Speed < 0)
            Speed += speedValue / slowCoeficient;

        if (speedValue < _stopSpeed)
            Speed = 0;
    }

    public void Centration()
    {
        if (_centred)
            return;

        foreach (var swipeMover in _swipeMovers)
        {
            float offset1 = Mathf.RoundToInt(swipeMover.transform.localPosition.x);

            swipeMover.transform.localPosition = new Vector3(offset1, swipeMover.transform.localPosition.y, swipeMover.transform.localPosition.z);
        }

        _centred = true;
    }
}
