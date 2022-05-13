using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickHandler : MonoBehaviour
{
    [SerializeField] private float _sensitivity;

    private bool _isInBraking;

    public bool Clicked { get; private set; }

    public float Speed { get; private set; }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
            Clicked = false;

        if (Clicked)
        {
            Speed = Input.GetAxis("Mouse X") * _sensitivity;
            Speed = Mathf.Clamp(Speed, - 30, 30);
        }
    }

    private void OnMouseDown()
    {
        Clicked = true;
    }

    public void SlowDown()
    {
        if (_isInBraking)
            return;

        if (Speed > 0)
            Speed -= Mathf.Abs(Speed)/2;

        if (Speed < 0)
            Speed += Mathf.Abs(Speed)/2;

        if (Mathf.Abs(Speed) < 6f)
            Speed = 0;
    }
}
