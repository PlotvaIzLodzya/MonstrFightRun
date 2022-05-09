using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using RunnerMovementSystem.Examples;

public class StartLevelButton : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private GameObject _shop;
    [SerializeField] private ShopAnimator _abilityShopAnimator;
    [SerializeField] private RoadMap _roadMap;

    public event Action RunStarted;

    public void OnPointerDown(PointerEventData eventData)
    {
        EnableRotators();
        gameObject.SetActive(false);
        _shop.SetActive(false);
        _abilityShopAnimator.CloseAnimation();
        _abilityShopAnimator.HideIcon();
        _roadMap.Disable();
        RunStarted?.Invoke();
        FindObjectOfType<CameraFollowing>().enabled = true;

        DisableMonsterShop();
    }

    private void DisableMonsterShop()
    {
        Graber graber = FindObjectOfType<Graber>();
        graber.enabled = false;
    }

    private void EnableRotators()
    {
        MonsterPlaceAccepter[] monsterPlaceAccepters = FindObjectsOfType<MonsterPlaceAccepter>();

        foreach (var monsterPlaceAccepter in monsterPlaceAccepters)
        {
            monsterPlaceAccepter.EnableRotator();
        }
    }
}
