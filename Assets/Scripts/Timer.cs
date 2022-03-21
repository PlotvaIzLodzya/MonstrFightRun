using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text _time;

    private void Update()
    {
        _time.text = $"{(int)Time.time}";
    }
}
