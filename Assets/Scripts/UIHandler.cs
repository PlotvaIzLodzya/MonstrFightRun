using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private GameObject _interfaceObject;

    public void SwitchState()
    {
        Debug.Log("hi");
        _interfaceObject.gameObject.SetActive(!_interfaceObject.gameObject.activeInHierarchy);
    } 
}
