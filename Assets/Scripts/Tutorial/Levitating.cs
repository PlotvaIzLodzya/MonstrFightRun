using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levitating : MonoBehaviour
{
    [SerializeField] private float _distance;
    [SerializeField] private float _speed;

    private float zPos = 0;
    private void Update()
    {
        zPos = _distance * Mathf.Sin(_speed * Time.time);

        transform.localPosition = new Vector3(0, 0, zPos);
    }
}
