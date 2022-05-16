using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MonsterPlace))]
public class MonsterPlaceAccepter : MonoBehaviour, IMonsterHolder
{
    [SerializeField] private MonstersHandler _monstersHandler;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private BoxCollider _boxCollider;

    private MonsterPlace _monsterPlace;
    private Monster _monster;
    private bool _opened;
    private Vector3 _initialColliderScale;
    public Rotator _rotator { get; private set; }
    public Monster Monster => _monster;

    public bool IsFree => _monster == null;

    public bool CanAcquireMonster => IsFree && _boxCollider.enabled;

    private void Awake()
    {
        _initialColliderScale = _boxCollider.size;
    }

    public bool TryAcquireMonster(Monster monster)
    {
        bool isPlaceFree = _monsterPlace.Monster == null && _opened;

        if (isPlaceFree)
        {
            _monster = monster;
            _monstersHandler.TrySetMonsterToPlace(_monster, _monsterPlace, _monster.Level);

            DisableRotator();
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
            _monster = null;
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
        _opened = true;
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
        _particleSystem.Play();

        _boxCollider.size = _boxCollider.size * 2;
    }

    private void DisableRotator()
    {
        _rotator = _monster.GetComponentInChildren<Rotator>();
        _rotator.enabled = false;
    }

    public void LightDown()
    {
        _particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        _boxCollider.size = _initialColliderScale;
    }
}
