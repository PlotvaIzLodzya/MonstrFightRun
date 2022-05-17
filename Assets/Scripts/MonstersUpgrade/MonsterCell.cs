using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(BoxCollider))]
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
    [HideInInspector]public bool IsInCenter;

    private Monster _initialMonster;
    private Material _initialMaterial;
    private BoxCollider _boxCollider;
    private Vector3 _initialBoxColliderSize;

    private string SaveName => $"MonsterCellIsOpened{_monster.Name}";

    public bool IsOpened { get; private set; }

    public MonsterInfoPanel MonsterInfoPanel => _monsterInfoPanel;
    public CameraTransition CameraTransitionToDefaultPosition => _cameraTransitionToDefaultPosition;
    public MonsterOpener MonsterUpgraderHandler => _monsterUpgraderOpener;
    public Monster Monster => _monster;
    public Monster InitialMonster => _initialMonster;
    public Transform MonsterPoint => _monsterPoint;

    private void OnMouseUp()
    {
        if (ViewState.IsViewed)
            TryPlaceMonster();
    }

    public void Init()
    {
        _initialMaterial = _monster.FormsHandler.CurrentForm.SkinnedMeshRenderer.material;
        _initialMonster = _monster;
        DisableRotator();

        _boxCollider = GetComponent<BoxCollider>();
        _initialBoxColliderSize = _boxCollider.size;

        if (PlayerPrefs.HasKey(SaveName))
            Open();
        else
            Hide();
    }

    public bool TryGrab(out Monster monster)
    {
        bool _isMonsterSetted = _monster != null;
        monster = null;

        if (IsOpened == false || ViewState.IsViewed)
            return false;

        if (_isMonsterSetted)
        {
            monster = _monster;
            _monster = null;
        }

        return _isMonsterSetted;
    }

    public void Clear()
    {
        _monster = null;
    }

    public bool TryTakeMonster(out Monster monster)
    {
        bool _isMonsterSetted = _monster != null;

        monster = _monster;

        return _isMonsterSetted;
    }

    public bool TryAcquireMonster(Monster monster)
    {
        if (monster.GetType() != InitialMonster.GetType() || IsOpened == false)
            return false;

        _monster = monster;

        monster.transform.parent = null;
        monster.transform.SetParent(_monsterPoint);
        monster.transform.localRotation = Quaternion.identity;
        monster.transform.localPosition = Vector3.zero;
        monster.transform.localScale = Vector3.one;

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
        {
            _monsterInfoPanel.Open(this);

            if (needCameraTransition && ViewState.IsViewed == false)
            {
                ViewState.IsViewed = true;
                _cameraTransitionToInfoPanel.TryTransit();
            }
        }

        _boxCollider.size = _boxCollider.size + Vector3.up * 4;

        return canOpen;
    }

    public void CloseInfoPanel(bool needCameraTransition = true)
    {
        _monsterInfoPanel.Close();

        if (needCameraTransition)
        {
            _cameraTransitionToDefaultPosition.TryTransit();

            ViewState.IsViewed = false;
        }

        _boxCollider.size = _initialBoxColliderSize;
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

        var place = monsterPlaceAccepters.FirstOrDefault(place => place.IsFree);

        CloseInfoPanel();

        if (TryTakeMonster(out Monster monster))
        {
            if (place.CanAcquireMonster)
            {
                Clear();

                monster.GetComponent<Mover>().MoveTo(monster, place);

                return true;
            }
        }

        return false;
    }
    private void DisableRotator()
    {
        Monster.GetComponentInChildren<Rotator>().enabled = false;
    }
}
