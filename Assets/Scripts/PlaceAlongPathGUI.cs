using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[CustomEditor(typeof(RoadPositioning)), CanEditMultipleObjects]
public class PlaceAlongPathGUI : Editor
{
    [SerializeField] private float _offset;
    [SerializeField] private float _height;

    public override void OnInspectorGUI()
    {

        _offset = EditorGUILayout.Slider("Offset", _offset, -2, 2);
        _height = EditorGUILayout.Slider("Height", _height, 0, 1);

        _offset = Constrainer(_offset);
        _height = Constrainer(_height);
        

        if (GUILayout.Button("Place"))
        {
            foreach (RoadPositioning roadPositioning in targets)
            {
                roadPositioning.Place(_height, _offset);
            }
        }

        if(GUILayout.Button("Place without rotation"))
        {
            foreach (RoadPositioning roadPositioning in targets)
            {
                roadPositioning.PlaceWithoutRotation(_height, _offset);
            }
        }
    }

    private float Constrainer(float value)
    {
        return Mathf.Round(value * 10) * 0.1f;
    }
}
