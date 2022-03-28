using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToFightTrigger : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out StateMachine stateMachine))
            stateMachine.enabled = true;

        if (other.TryGetComponent(out MonsterAnimator monsterAnimator))
            monsterAnimator.ToFightTransition();

        if (other.TryGetComponent(out UIHandler healthbarEnabler))
            healthbarEnabler.SwitchState();
    }
}
