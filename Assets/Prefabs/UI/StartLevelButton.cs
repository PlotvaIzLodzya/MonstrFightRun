using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using RunnerMovementSystem.Examples;

public class StartLevelButton : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private GameObject _shop;
    [SerializeField] private ShopAnimator[] _shopAnimators;
    [SerializeField] private RoadMap _roadMap;

    public event Action RunStarted;

    public void OnPointerDown(PointerEventData eventData)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit[] raycastHits = Physics.RaycastAll(ray, 50f);

        foreach (var hitInfo in raycastHits)
        {
            if (hitInfo.collider.TryGetComponent(out IMonsterHolder monsterHolder) || hitInfo.collider.TryGetComponent(out SwipeZone swipeZone))
                return;
        }

        EnableRotators();
        gameObject.SetActive(false);
        _shop.SetActive(false);

        foreach (var shopAnimator in _shopAnimators)
        {
            shopAnimator.HideIcon();
            shopAnimator.CloseAnimation();
        }

        _roadMap.Disable();
        RunStarted?.Invoke();
        FindObjectOfType<CameraFollowing>().enabled = true;

        DisableMonsterShop();
        InitializeMonsterPool();

        FindObjectOfType<MonsterShop>().SaveMonsterParty();
        FindObjectOfType<Week>().DisableIndicator();

    }

    private void InitializeMonsterPool()
    {
        FindObjectOfType<MonsterPoolCreator>().Init();
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
