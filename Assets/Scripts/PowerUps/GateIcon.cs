using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateIcon : MonoBehaviour
{
    public void CreateIcon(Monster monster)
    {
        var monsterIcon = Instantiate(monster, transform);
        monsterIcon.transform.localScale = Vector3.one * 1.5f;
        monsterIcon.Rigidbody.isKinematic = true;
        monsterIcon.gameObject.layer = 0;
        monsterIcon.GetComponent<MonsterLevelPresenter>().Disable();
        monsterIcon.GetComponent<StateMachine>().enabled = false;
        monsterIcon.GetComponentInChildren<Rotator>().enabled = false;
    }
}
