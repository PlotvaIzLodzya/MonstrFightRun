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
    [SerializeField] private MonsterUpgraderHandler _monsterUpgraderOpener;
    [SerializeField] private Material _inactiveMaterial;

    private Monster _initialMonster;
    private Material _initialMaterial;
    private string SaveName => $"MonsterCellIsOpened{_monster.Name}";

    public bool IsOpened { get; private set; }

    public MonsterUpgraderHandler MonsterUpgraderHandler => _monsterUpgraderOpener;
    public Monster Monster => _monster;
    public Monster InitialMonster => _initialMonster;
    public Transform MonsterPoint => _monsterPoint;

    private void OnEnable()
    {
        _monsterUpgraderOpener.Opened += Open;
    }

    private void OnDisable()
    {
        _monsterUpgraderOpener.Opened -= Open;
    }

    public void Init()
    {
        _initialMaterial = _monster.FormsHandler.CurrentForm.SkinnedMeshRenderer.material;
        _initialMonster = _monster;
        _image.sprite = _monstersIcons.GetAttackRangeIconSprite(_monster.GetComponent<Attack>().InitialRange);
        DisableRotator();

        if (PlayerPrefs.HasKey(SaveName))
            Open();
        else
            Hide();
    }

    public bool Grab(out Monster monster)
    {
        bool _isMonsterSetted = _monster != null;
        monster = null;

        if (IsOpened == false)
            return false;

        if (_isMonsterSetted)
        {
            monster = _monster;
            _monster = null;
        }

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
        _monsterUpgraderOpener.EnableUpgradeButton();
        _monsterUpgraderOpener.gameObject.SetActive(false);
        _monster.FormsHandler.CurrentForm.SkinnedMeshRenderer.material = _initialMaterial;
        _monster.MonsterAnimator.VictoryAnimation(true);
        PlayerPrefs.SetString(SaveName, SaveName);
    }

    private void DisableRotator()
    {
        Monster.GetComponentInChildren<Rotator>().enabled = false;
    }
}
