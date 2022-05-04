using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartLevelButton : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private GameObject _shop;
    [SerializeField] private ShopAnimator _abilityShopAnimator;
    [SerializeField] private RoadMap _roadMap;

    public event Action RunStarted;

    public void OnPointerDown(PointerEventData eventData)
    {
        gameObject.SetActive(false);
        _shop.SetActive(false);
        _abilityShopAnimator.CloseAnimation();
        _abilityShopAnimator.HideIcon();
        _roadMap.Disable();
        RunStarted?.Invoke();
    }
}
