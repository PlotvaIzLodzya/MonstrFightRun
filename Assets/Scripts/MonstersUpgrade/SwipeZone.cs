using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeZone : MonoBehaviour
{
    [SerializeField] private float _sensitivity;

    private float _stopSpeed = 7f;
    private float _threshold = 6;
    private SwipeMover[] _swipeMovers;
    private SwipeMover _centralMover;
    private float _xPointerDistance;

    public static bool IsMoving;

    public bool Clicked { get; private set; }
    public float Speed { get; private set; }

    private void Awake()
    {
        _swipeMovers = GetComponentsInChildren<SwipeMover>();

        float spacing = Mathf.Abs(_swipeMovers[0].transform.localPosition.x) - Mathf.Abs(_swipeMovers[1].transform.localPosition.x);

        foreach (var swipeMover in _swipeMovers)
        {
            swipeMover.Init(_swipeMovers[0].transform, _swipeMovers[_swipeMovers.Length - 1].transform, spacing);
        }
    }

    private void Update()
    {
        IsMoving = Speed != 0;

        if (Input.GetMouseButtonUp(0))
        {
            _xPointerDistance = 0;
            Clicked = false;
        }

        if (Clicked && EventSystem.current.IsPointerOverGameObject() == false)
        {
            Speed = Input.GetAxis("Mouse X") * _sensitivity;
            Speed = Mathf.Clamp(Speed, -30, 30);
            _xPointerDistance += Input.GetAxis("Mouse X");
        }

        if(Speed != 0)
        {
            foreach (var swipeMover in _swipeMovers)
            {
                swipeMover.Move(Speed);
            }
        }

        if(Mathf.Abs(Speed) < 0.2f && Mathf.Abs(_xPointerDistance) < _threshold)
            Centrate();
    }

    private void OnMouseDown()
    {
        Clicked = true;
    }

    public void SlowDown(float speedDivider, SwipeMover swipeMover)
    {
        _centralMover = swipeMover;
        float speedValue = Mathf.Abs(Speed);

        if (Speed > 0)
            Speed -= speedValue / speedDivider;

        if (Speed < 0)
            Speed += speedValue / speedDivider;

        if (speedValue < _stopSpeed)
            Speed = 0;
    }

    public void Centrate()
    {
        float offset = _centralMover.transform.localPosition.x;

        foreach (var swipeMover in _swipeMovers)
        {
            float xPosition = swipeMover.transform.localPosition.x;
            float targetXPosition = xPosition - offset;

            xPosition = Mathf.MoveTowards(xPosition, targetXPosition, 3.5f * Time.deltaTime);

            swipeMover.transform.localPosition = new Vector3(xPosition, swipeMover.transform.localPosition.y, swipeMover.transform.localPosition.z);
        }
    }
}
