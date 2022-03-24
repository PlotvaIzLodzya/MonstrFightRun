using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightHandler
{
    public Vector3 GetHeight(Vector3 position, float height)
    {
        if (Physics.Raycast(position, Vector3.down, out RaycastHit hitInfo))
            position = new Vector3(position.x, hitInfo.point.y + height, position.z);


        return position;
    }
}
