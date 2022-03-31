using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private GameObject _interfaceObject;

    public void SwitchState()
    {
        _interfaceObject.gameObject.SetActive(!_interfaceObject.gameObject.activeInHierarchy);
    }

    public void Enable()
    {
        _interfaceObject.gameObject.SetActive(true);
    }

    public void Disable()
    {
        _interfaceObject.gameObject.SetActive(false);
    }
}
