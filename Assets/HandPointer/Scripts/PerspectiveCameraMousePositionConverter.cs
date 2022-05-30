using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerspectiveCameraMousePositionConverter : MousePositionConverter
{
    public PerspectiveCameraMousePositionConverter(Camera camera) : base(camera) { }

    public override Vector3 GetCursorPosition(Vector2 mousePosition, float distance)
    {
        //Ray ray = _camera.ScreenPointToRay(mousePosition);
        //return _camera.transform.position + ray.direction * distance;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane xzPlane = new Plane(Vector3.forward, new Vector3(0, 0, Camera.main.transform.position.z + distance));
        xzPlane.Raycast(ray, out distance);
        return ray.GetPoint(distance);
    }
}
