using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MonsterCell : MonoBehaviour, IMonsterHolder
{
    [SerializeField] private Monster _monster;
    [SerializeField] private Transform _monsterPoint;
    [SerializeField] private MonsterOpener _monsterUpgraderOpener;
    [SerializeField] private Material _inactiveMaterial;
    [SerializeField] private MonsterInfoPanel _monsterInfoPanel;
    [SerializeField] private CameraTransition _cameraTransitionToInfoPanel;
    [SerializeField] private CameraTransition _cameraTransitionToDefaultPosition;

    private Monster _initialMonster;
    private Material _initialMaterial;

    private string SaveName => $"MonsterCellIsOpened{_monster.Name}";

    public bool IsOpened { get; private set; }

    public MonsterInfoPanel MonsterInfoPanel => _monsterInfoPanel;
    public MonsterOpener MonsterUpgraderHandler => _monsterUpgraderOpener;
    public Monster Monster => _monster;
    public Monster InitialMonster => _initialMonster;
    public Transform MonsterPoint => _monsterPoint;

    public void Init()
    {
        _initialMaterial = _monster.FormsHandler.CurrentForm.SkinnedMeshRenderer.material;
        _initialMonster = _monster;
        DisableRotator();

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
        _monsterUpgraderOpener.gameObject.SetActive(true);
        _monster.FormsHandler.CurrentForm.SkinnedMeshRenderer.material = _inactiveMaterial;
    }

    public void Open()
    {
        IsOpened = true;
        _monsterUpgraderOpener.gameObject.SetActive(false);
        _monster.FormsHandler.CurrentForm.SkinnedMeshRenderer.material = _initialMaterial;
        _monster.MonsterAnimator.VictoryAnimation(true);
        PlayerPrefs.SetString(SaveName, SaveName);
    }

    public bool TryOpenInfoPanel(bool needCameraTransition = true)
    {
        bool canOpen = _monster != null && IsOpened;

        if (canOpen)
        {
            _monsterInfoPanel.Open(this);

            if (needCameraTransition && ViewState.IsViewed == false)
            {
                ViewState.IsViewed = true;
                _cameraTransitionToInfoPanel.TryTransit();
            }
        }

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
    }

    private void DisableRotator()
    {
        Monster.GetComponentInChildren<Rotator>().enabled = false;
    }
}
