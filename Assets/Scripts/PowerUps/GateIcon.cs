using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GateIcon : MonoBehaviour
{
    [SerializeField] private TMP_Text _monsterName;

    private Monster _monsterIcon;
    private MonstersHandler _monstersHandler;

    private void Awake()
    {
        _monstersHandler = FindObjectOfType<MonstersHandler>();
    }

    private void OnEnable()
    {
        _monstersHandler.MonsterMerged += TryUpdateIconForm;
    }

    private void OnDisable()
    {
        _monstersHandler.MonsterMerged -= TryUpdateIconForm;
    }

    public void CreateIcon(Monster monster)
    {
        _monsterIcon = Instantiate(monster);
        _monsterIcon.transform.position = transform.position;
        _monsterIcon.transform.rotation = transform.rotation;
        _monsterIcon.Rigidbody.isKinematic = true;
        _monsterIcon.gameObject.layer = 0;
        _monsterIcon.GetComponent<MonsterLevelPresenter>().Disable();
        _monsterIcon.GetComponent<Collider>().enabled = false;
        _monsterIcon.GetComponent<StateMachine>().enabled = false;
        _monsterIcon.GetComponentInChildren<Rotator>().enabled = false;
        _monsterIcon.MonsterAnimator.enabled = false;

        _monsterName.text = _monsterIcon.Name;
    }

    public void TryUpdateIconForm(Monster monster)
    {
        if(monster.GetType() == _monsterIcon.GetType())
            _monsterIcon.TryMerge(0);
    }
}
