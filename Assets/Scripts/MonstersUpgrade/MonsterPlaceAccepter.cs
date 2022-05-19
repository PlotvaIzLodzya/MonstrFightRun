using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MonsterPlace))]
public class MonsterPlaceAccepter : MonoBehaviour, IMonsterHolder
{
    [SerializeField] private MonstersHandler _monstersHandler;
    [SerializeField] private GameObject _freeEffect;
    [SerializeField] private BoxCollider _boxCollider;

    private MonsterPlace _monsterPlace;
    private Monster _monster;
    private Vector3 _initialColliderScale;

    public Rotator _rotator { get; private set; }
    public Monster Monster => _monster;
    public bool IsFree => _monster == null;
    public bool CanAcquireMonster => IsFree && _boxCollider.enabled;
    public bool IsOpened { get; private set; }

    public event Action Changed;

    private void Awake()
    {
        _initialColliderScale = _boxCollider.size;
    }

    public bool TryAcquireMonster(Monster monster)
    {
        bool isPlaceFree = _monsterPlace.Monster == null && IsOpened;

        if (isPlaceFree)
        {
            _monster = monster;
            _monstersHandler.TrySetMonsterToPlace(_monster, _monsterPlace, _monster.Level);

            DisableRotator();
            LightDown();
            Changed?.Invoke();
        }

        return isPlaceFree;
    }

    public bool TryGrab(out Monster monster)
    {
        bool _isMonsterSetted = _monster != null;
        monster = null;

        if (_isMonsterSetted)
        {
            _monsterPlace.Free(true);
            _monstersHandler.DecreasseCounter(_monster.Level);
            monster = _monster;
            LightUp();
            _monster = null;

            Changed?.Invoke();
        }

        return _isMonsterSetted;
    }

    public void EnableRotator()
    {
        if(_rotator != null)
            _rotator.enabled = true;
    }

    public void Open()
    {
        _monsterPlace.EnableCollider();
        IsOpened = true;

        if(CanAcquireMonster)
            LightUp();

        Changed?.Invoke();
    }

    public void Hide()
    {
        _monsterPlace.DeActivate();
    }

    public void Activate()
    {
        if (_monsterPlace == null)
            _monsterPlace = GetComponent<MonsterPlace>();

        gameObject.SetActive(true);
    }

    public void LightUp()
    {
        _freeEffect.SetActive(true);

        _boxCollider.size = _boxCollider.size + (Vector3.right+Vector3.forward) *1.2f;
    }

    private void DisableRotator()
    {
        _rotator = _monster.GetComponentInChildren<Rotator>();
        _rotator.enabled = false;
    }

    public void LightDown()
    {
        _freeEffect.SetActive(false);
        _boxCollider.size = _initialColliderScale;
    }
}
