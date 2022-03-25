using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private GameObject _interfaceObject;

    public void ToFight()
    {
        _interfaceObject.gameObject.SetActive(!_interfaceObject.gameObject.activeInHierarchy);
    } 
}
