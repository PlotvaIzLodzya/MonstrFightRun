using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using UnityEngine.UI;

public class AddToPartyButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image _noPlaceForMonster;

    private MonsterPlaceAccepter[] _monsterPlaceAccepters;
    private MonsterCell _monsterCell;

    private bool _hideButton;

    private bool _initialized;

    private void Update()
    {
        if (_initialized == false)
            return;

        _hideButton = true;


        foreach (var monsterPlaceAccepter in _monsterPlaceAccepters)
        {
            if (_monsterCell.IsMonsterUsed == false && monsterPlaceAccepter.CanAcquireMonster)
                _hideButton = false;
        }

        _noPlaceForMonster.gameObject.SetActive(_hideButton);
    }

    public void Init(MonsterCell monsterCell)
    {
        _monsterCell = monsterCell;
        _monsterPlaceAccepters = FindObjectOfType<MonstersHandler>().GetComponentsInChildren<MonsterPlaceAccepter>();
        Error.CheckOnNull(_monsterPlaceAccepters, nameof(MonstersHandler));
        _initialized = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_hideButton)
            return;

        _monsterCell.TryPlaceMonster();
    }
}
