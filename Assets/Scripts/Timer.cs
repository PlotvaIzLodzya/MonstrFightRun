using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text _time;

    private float _elapsedTime = 0;
    private void Update()
    {
        _elapsedTime += Time.deltaTime;
        _time.text = $"{(int)_elapsedTime}";
    }

    private void OnDisable()
    {
        Debug.Log(_elapsedTime);
    }
}
