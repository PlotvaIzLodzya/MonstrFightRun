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

    private void Update()
    {
        foreach (var monsterPlace in _monsterPlaceAccepters)
        {
            _hideButton = true;

            if (monsterPlace.CanAcquireMonster)
            {
                _hideButton = false;
                _noPlaceForMonster.gameObject.SetActive(_hideButton);
                return;
            }
        }

        _noPlaceForMonster.gameObject.SetActive(_hideButton);
    }
    public void Init(MonsterCell monsterCell)
    {
        _monsterCell = monsterCell;
        _monsterPlaceAccepters = FindObjectOfType<MonstersHandler>().GetComponentsInChildren<MonsterPlaceAccepter>();
        Error.CheckOnNull(_monsterPlaceAccepters, nameof(MonstersHandler));
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        var place = _monsterPlaceAccepters.FirstOrDefault(place => place.IsFree);

        if (_monsterCell.TryTakeMonster(out Monster monster))
        {
            if (place.TryAcquireMonster(monster))
            {
                _monsterCell.Clear();
                _monsterCell.CloseInfoPanel();
            }
        }
    }
}
