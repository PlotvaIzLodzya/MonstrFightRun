using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    private Monster _monster;

    public void MoveTo(Monster monster, MonsterPlaceAccepter monsterPlaceAccepter)
    {
        _monster = monster;
        StartCoroutine(MoveAnimation(monsterPlaceAccepter));
    }

    private IEnumerator MoveAnimation(MonsterPlaceAccepter monsterPlaceAccepter)
    {
        _monster.MonsterAnimator.RunAnimation();

        float distance = Vector3.Distance(_monster.transform.position, monsterPlaceAccepter.transform.position);

        while (distance > 0.2f)
        {
            Vector3 direction = (monsterPlaceAccepter.transform.position - transform.position).normalized;

            Rotate(direction);

            transform.position += (transform.forward * 10f * Time.deltaTime);

            distance = Vector3.Distance(_monster.transform.position, monsterPlaceAccepter.transform.position);

            yield return null;
        }


        monsterPlaceAccepter.TryAcquireMonster(_monster);
        _monster.MonsterAnimator.IdleAnimation();
    }

    private void Rotate(Vector3 direction)
    {
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        lookRotation.x = 0;
        lookRotation.z = 0;

        transform.rotation = lookRotation;
    }

}
