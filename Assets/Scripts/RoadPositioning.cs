using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using UnityEditor;

[System.Serializable]
public class RoadPositioning : MonoBehaviour
{
    [HideInInspector] public float Height;
    [HideInInspector] public float Offset;

    private PathCreator _pathCreator;
    private void OnValidate()
    {
        if (_pathCreator == null)
            _pathCreator = FindObjectOfType<PathCreator>();
    }

    public void Place(float height, float offset)
    {
        transform.position = GetPosition(transform.position, height, offset);
        transform.rotation = GetRotation(transform.position, height, offset);
    }

    public void PlaceWithoutRotation(float height, float offset)
    {
        transform.position = GetPosition(transform.position, height, offset);
    }

    public Vector3 GetPosition(Vector3 position, float height, float offset)
    {
        Vector3 point = _pathCreator.path.GetClosestPointOnPath(position);
        position = new Vector3(point.x + offset, point.y + height, point.z);

        return position;
    }

    public Quaternion GetRotation(Vector3 position, float height, float offset)
    {
        Vector3 point = GetPosition(position, height, offset);

        float time = _pathCreator.path.GetClosestTimeOnPath(point);
        Quaternion rotation = _pathCreator.path.GetRotation(time);

        rotation = new Quaternion(0, rotation.y, 0, rotation.w);

        return rotation;
    }
}
