using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using RunnerMovementSystem.Examples;
using UnityEngine.UI;

public class StartLevelButton : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private GameObject _shop;
    [SerializeField] private ShopAnimator[] _shopAnimators;
    [SerializeField] private RoadMap _roadMap;
    [SerializeField] private Image _buttonImage;

    public event Action RunStarted;

    private bool _canStartRun;

    public void OnPointerDown(PointerEventData eventData)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit[] raycastHits = Physics.RaycastAll(ray, 50f);

        if (_canStartRun == false)
            return;

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
        var monsterHandler = FindObjectOfType<MonstersHandler>();
        monsterHandler.TurnAllMonsterForward();
        monsterHandler.GetComponent<MonsterLevelPresenter>().Enable();

        DisableMonsterShop();
        InitializeMonsterPool();

        var monsterShop = FindObjectOfType<MonsterShop>();
        monsterShop.SaveMonsterParty();
        monsterShop.GetComponent<ShopAnimator>().CloseAnimation();
        //FindObjectOfType<Week>().DisableIndicator();

        FindObjectOfType<MonsterCounter>().gameObject.SetActive(false);
    }

    public void SetActive(bool isPartyFull)
    {
        _canStartRun = isPartyFull;
        
        if (_canStartRun)
            _buttonImage.color = new Color(_buttonImage.color.r, _buttonImage.color.g, _buttonImage.color.b, 1);
        else
            _buttonImage.color = new Color(_buttonImage.color.r, _buttonImage.color.g, _buttonImage.color.b, 0.5f);
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
