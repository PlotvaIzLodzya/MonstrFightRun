using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

public class SwipeZone : MonoBehaviour
{
    [SerializeField] private float _sensitivity;

    private List<SwipeMover> _swipeMovers;
    private float _stopSpeed = 7f;
    private float _xPointerthreshold = 1f;
    private SwipeMover _centralMover;
    private float _xPointerDistance;
    private float _spacing;

    private const float _centerXPosition = 0f;
    public Vector3 LeftBorder { get; private set; }
    public Vector3 RightBorder { get; private set; }
    public float LeftOffset { get; private set; }
    public float RightOffset { get; private set; }
    public static bool IsMoving { get; private set; }
    public static bool Clicked { get; private set; }
    public float Speed { get; private set; }

    private void Awake()
    {
        _swipeMovers = GetComponentsInChildren<SwipeMover>().ToList();

        _spacing = Mathf.Abs(_swipeMovers[0].transform.localPosition.x) - Mathf.Abs(_swipeMovers[1].transform.localPosition.x);

        RightBorder = _swipeMovers[0].transform.localPosition - Vector3.right * _spacing;
        LeftBorder = _swipeMovers[_swipeMovers.Count - 1].transform.localPosition + Vector3.right *_spacing;

        float distance = Mathf.Abs(LeftBorder.x) + Mathf.Abs(RightBorder.x);

        LeftOffset = distance - _spacing;
        RightOffset = -distance + _spacing;

        foreach (var swipeMover in _swipeMovers)
        {
            swipeMover.Init(this);
        }

    }

    private void Update()
    {
        if (Graber.Grabed)
        {
            Speed = 0;
            return;
        }

        IsMoving = Mathf.Abs(Speed) > 0.001f;

        if (Input.GetMouseButtonUp(0))
        {
            _xPointerDistance = 0;
            Clicked = false;
        }

        if (Clicked )
        {
            Speed = Input.GetAxis("Mouse X") * _sensitivity * Time.deltaTime;
            Speed = Mathf.Clamp(Speed, -30, 30);
            _xPointerDistance += Input.GetAxis("Mouse X") * Time.deltaTime;
        }

        if (Speed != 0)
        {
            foreach (var swipeMover in _swipeMovers)
            {
                swipeMover.Move(Speed);
            }
        }

        if(Mathf.Abs(Speed) < 1.5f && Mathf.Abs(_xPointerDistance) < _xPointerthreshold)
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
        if (_centralMover.IsInTransition)
            return;

        float offset = _centralMover.transform.localPosition.x;

        foreach (var swipeMover in _swipeMovers)
        {
            float xPosition = swipeMover.transform.localPosition.x;
            float targetXPosition = xPosition - offset;

            xPosition = Mathf.MoveTowards(xPosition, targetXPosition, 1f * Time.deltaTime);

            swipeMover.transform.localPosition = new Vector3(xPosition, swipeMover.transform.localPosition.y, swipeMover.transform.localPosition.z);
        }
    }

    public void ShrinkToSwipeMover(SwipeMover initialSwipeMover, float xPos, bool instaShrink)
    {
        _swipeMovers.Remove(initialSwipeMover);
        _swipeMovers = _swipeMovers.OrderByDescending(mover => mover.transform.localPosition.x).ToList();

        Shrink(instaShrink, xPos);

        ShrinkOffstes();
    }

    public void Shrink(bool instaShrink, float xPos)
    {
        float counter = xPos;
        float targetXPosition;


        foreach (var swipeMover in _swipeMovers)
        {
            if (swipeMover.transform.localPosition.x < xPos)
            {
                targetXPosition = -counter;
                counter+=_spacing;
                Debug.Log($"xPos: {xPos}, target{targetXPosition}, counter: {counter}");

                swipeMover.TranslateLeft(targetXPosition);

                //if (instaShrink == false)
                //    swipeMover.TranslateLeft(targetXPosition);
                //else
                //{

                //    swipeMover.transform.localPosition = new Vector3(targetXPosition, swipeMover.transform.localPosition.y, swipeMover.transform.localPosition.z);
                //}
            }
        }
    }

    public void ExpandFromSwipeMover(SwipeMover initialSwipeMover)
    {
        initialSwipeMover.transform.localPosition = new Vector3(_centerXPosition, initialSwipeMover.transform.localPosition.y, initialSwipeMover.transform.localPosition.z);
        _centralMover = initialSwipeMover;

        Expand();

        _swipeMovers.Add(initialSwipeMover);
        ExpandOffsets();
    }

    public void Expand()
    {
        int counter = 0;
        _swipeMovers = _swipeMovers.OrderByDescending(mover => mover.transform.localPosition.x).ToList();

        foreach (var swipeMover in _swipeMovers)
        {
            if (swipeMover.transform.localPosition.x <= _centerXPosition)
            {
                counter++;
                float targetXPosition = -counter * _spacing;

                swipeMover.TranslateRight(targetXPosition);
            }
        }
    }

    private void ExpandOffsets()
    {
        ChangeOffset(_spacing);
    }

    private void ShrinkOffstes()
    {
        ChangeOffset(-_spacing);
    }

    private void ChangeOffset(float spacing)
    {
        LeftBorder += Vector3.right * spacing;
        RightBorder -= Vector3.right * spacing;

        LeftOffset += spacing;
        RightOffset -= spacing;
    }
}
