using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeZone : MonoBehaviour
{
    [SerializeField] private float _sensitivity;

    private SwipeMover[] _swipeMovers;
    public bool Clicked { get; private set; }
    private bool _centred;

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

            if (Speed > 7.5f)
                _centred = false;
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

        if (speedValue < 5f)
            Speed = 0;
    }

    public void Centration(float offset)
    {
        if (_centred)
            return;

        foreach (var swipeMover in _swipeMovers)
        {
            swipeMover.transform.localPosition = new Vector3(swipeMover.transform.localPosition.x + offset, swipeMover.transform.localPosition.y, swipeMover.transform.localPosition.z);
        }

        _centred = true;
    }
}
