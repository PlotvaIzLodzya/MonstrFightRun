using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterCell : MonoBehaviour, IMonsterHolder
{
    [SerializeField] private Monster _monster;
    [SerializeField] private MonstersIcons _monstersIcons;
    [SerializeField] private Image _image;
    [SerializeField] private Transform _monsterPoint;

    private Monster InitialMonster;

    public Monster Monster => _monster;
    public Transform MonsterPoint => _monsterPoint;

    private void Start()
    {
        InitialMonster = _monster;
        _image.sprite = _monstersIcons.GetAttackRangeIconSprite(_monster.GetComponent<Attack>().InitialRange);
        DisableRotator();
    }
    public bool Grab(out Monster monster)
    {
        bool _isMonsterSetted = _monster != null;
        monster = null;

        if (_isMonsterSetted)
        {
            monster = _monster;
            _monster = null;
        }

        return _isMonsterSetted;
    }

    public bool TryAcquireMonster(Monster monster)
    {
        if (monster.GetType() != InitialMonster.GetType())
            return false;

        _monster = monster;

        monster.transform.parent = null;
        monster.transform.SetParent(_monsterPoint);
        monster.transform.localRotation = Quaternion.identity;
        monster.transform.localPosition = Vector3.zero;

        DisableRotator();
        return _monster != null;
    }

    private void DisableRotator()
    {
        Monster.GetComponentInChildren<Rotator>().enabled = false;
    }
}
