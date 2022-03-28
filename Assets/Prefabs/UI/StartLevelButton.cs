using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartLevelButton : MonoBehaviour, IPointerDownHandler
{
    public event Action RunStarted;
    public void OnPointerDown(PointerEventData eventData)
    {
        gameObject.SetActive(false);
        RunStarted?.Invoke();
    }
}
