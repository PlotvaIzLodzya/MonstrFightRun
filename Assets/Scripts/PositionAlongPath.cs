using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

[System.Serializable]
public class PositionAlongPath
{
    [SerializeField] private float height;
    [SerializeField] private float offset;

    public Vector3 GetPosition(Vector3 position, VertexPath vertexPath)
    {
        Vector3 point = vertexPath.GetClosestPointOnPath(position);
        position = new Vector3(point.x + offset, point.y + height, point.z);

        return position;
    }

    public Quaternion GetRotation(Vector3 position, VertexPath vertexPath)
    {
        Vector3 point = GetPosition(position, vertexPath);

        float time = vertexPath.GetClosestTimeOnPath(point);
        Quaternion rotation = vertexPath.GetRotation(time);

        rotation = new Quaternion(0, rotation.y, 0, rotation.w);
       
        return rotation;
    }
}
