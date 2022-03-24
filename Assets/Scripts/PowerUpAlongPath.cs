using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

[System.Serializable]
public class PowerUpAlongPath
{
    [SerializeField] private float height;
    [SerializeField] private float offset;

    public Vector3 GetHeight(Vector3 position, VertexPath vertexPath)
    {
        if (Physics.Raycast(position, Vector3.down, out RaycastHit hitInfo))
        {
            Vector3 point = vertexPath.GetClosestPointOnPath(position);

            position = new Vector3(point.x + offset, hitInfo.point.y + height, point.z);
        }


        return position;
    }
}
