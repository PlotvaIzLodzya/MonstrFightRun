using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

public class AddToPartyButton : MonoBehaviour, IPointerClickHandler
{
    private MonsterPlaceAccepter[] _monsterPlaceAccepters;
    private MonsterCell _monsterCell;

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
            if (place.TryAcquireMonster(monster))
            {
                _monsterCell.Clear();
                _monsterCell.CloseInfoPanel();
            }
    }
}
