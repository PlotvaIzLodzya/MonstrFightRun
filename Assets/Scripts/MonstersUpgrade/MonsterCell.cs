using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MonsterCell : MonoBehaviour, IMonsterHolder
{
    [SerializeField] private Monster _monster;
    [SerializeField] private Transform _monsterPoint;
    [SerializeField] private MonsterOpener _monsterUpgraderOpener;
    [SerializeField] private Material _inactiveMaterial;
    [SerializeField] private MonsterInfoPanel _monsterInfoPanel;
    [SerializeField] private CameraTransition _cameraTransitionToInfoPanel;
    [SerializeField] private CameraTransition _cameraTransitionToDefaultPosition;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private FrameHandler _frameHandler;
    [SerializeField] private Transform _placeForMonster;
    [SerializeField] private SwipeZone _swipeZone;
    [HideInInspector]public bool IsInCenter;

    private Monster _initialMonster;
    private Material _initialMaterial;

    private string SaveName => $"MonsterCellIsOpened{_monster.Name}";

    public bool IsOpened { get; private set; }
    public bool IsMonsterUsed { get; private set; }

    public MonsterInfoPanel MonsterInfoPanel => _monsterInfoPanel;
    public CameraTransition CameraTransitionToDefaultPosition => _cameraTransitionToDefaultPosition;
    public MonsterOpener MonsterUpgraderHandler => _monsterUpgraderOpener;
    public Monster Monster => _monster;
    public Monster InitialMonster => _initialMonster;
    public Transform MonsterPoint => _monsterPoint;

    private void OnMouseUp()
    {
        if (EventSystem.current.IsPointerOverGameObject() == false && IsMonsterUsed == false)
        {
            TryPlaceMonster();
        }
    }

    public void Init()
    {
        _initialMaterial = _monster.FormsHandler.CurrentForm.SkinnedMeshRenderer.material;
        _initialMonster = _monster;
        DisableRotator();

        if (PlayerPrefs.HasKey(SaveName))
            Open();
        else
            Hide();

        if (Mathf.Abs(transform.localPosition.x) <=1f)
        {
            IsInCenter = true;
            TryOpenInfoPanel();
        }
    }

    public bool TryGrab(out Monster monster, bool instaShrink = false)
    {
        monster = null;

        if (IsOpened == false || IsMonsterUsed || IsInCenter == false)
            return false;

        SwitchState(true);

        _swipeZone.ShrinkToSwipeMover(GetComponent<SwipeMover>(), transform.localPosition.x, instaShrink);

        monster = _monster;

        return true;
    }

    public void Clear()
    {
        _monster = null;
    }

    public bool TryTakeMonster(out Monster monster)
    {
        bool _isMonsterSetted = _monster != null;

        SwitchState(true);

        monster = _monster;

        return _isMonsterSetted;
    }

    public bool TryAcquireMonster(Monster monster)
    {
        if (monster.GetType() != InitialMonster.GetType() || IsOpened == false)
            return false;

        SwitchState( false);

        MonsterPositioning(monster);

        _swipeZone.ExpandFromSwipeMover(GetComponent<SwipeMover>());

        DisableRotator();

        return _monster != null;
    }

    public bool IsMonsterPlaced()
    {
        return _monster == null;
    }

    public void Activate()
    {

    }

    public void Hide()
    {
        IsOpened = false;

        if(_monsterUpgraderOpener.Reward == false)
            _monsterUpgraderOpener.gameObject.SetActive(true);

        _monster.FormsHandler.CurrentForm.SkinnedMeshRenderer.material = _inactiveMaterial;
    }

    public void Open()
    {
        IsOpened = true;
        _monsterUpgraderOpener.DisableRewardPanel();
        _monsterUpgraderOpener.gameObject.SetActive(false);
        _monster.FormsHandler.CurrentForm.SkinnedMeshRenderer.material = _initialMaterial;
        _monster.MonsterAnimator.VictoryAnimation(true);
        PlayerPrefs.SetString(SaveName, SaveName);
    }

    public bool TryOpenInfoPanel(bool needCameraTransition = true)
    {
        bool canOpen = _monster != null && IsOpened && IsInCenter;

        if (canOpen)
            _monsterInfoPanel.Open(this);

        return canOpen;
    }

    public void CloseInfoPanel(bool needCameraTransition = true)
    {
        _monsterInfoPanel.Close();
    }

    public void LightUp()
    {
        _particleSystem.Play();
    }

    public void LightDown()
    {
        _particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }

    public bool TryPlaceMonster()
    {
        var monsterPlaceAccepters = FindObjectOfType<MonstersHandler>().GetComponentsInChildren<MonsterPlaceAccepter>();

        foreach (var monsterPlace in monsterPlaceAccepters)
        {
            if (monsterPlace.IsFree == false && monsterPlace.Monster.GetType() == _monster.GetType())
                return false;
        }

        var place = monsterPlaceAccepters.FirstOrDefault(place => place.CanAcquireMonster);

        if (place == default)
            return false;

        if (TryTakeMonster(out Monster monster))
        {
            if (place.CanAcquireMonster)
            {
                monster.transform.position = place.transform.position + monster.transform.right*Random.Range(-5,5) - place.transform.forward*7;

                monster.GetComponent<Mover>().MoveTo(monster, place, place.transform.position);

                SwitchState(true);

                return true;
            }
        }

        return false;
    }

    private void MonsterPositioning(Monster monster)
    {
        monster.transform.parent = null;
        monster.transform.SetParent(_placeForMonster);
        monster.transform.localRotation = Quaternion.identity;
        monster.transform.localPosition = Vector3.zero;
        monster.transform.localScale = Vector3.one;
    }

    private void DisableRotator()
    {
        Monster.GetComponentInChildren<Rotator>().enabled = false;
    }

    private void SwitchState(bool isUsed)
    {
        IsMonsterUsed = isUsed;
        _frameHandler.SwitchState(isUsed);
    }
}
