using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartLevelButton : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private GameObject _shop;

    public event Action RunStarted;

    public void OnPointerDown(PointerEventData eventData)
    {
        gameObject.SetActive(false);
        _shop.SetActive(false);
        RunStarted?.Invoke();
    }
}
