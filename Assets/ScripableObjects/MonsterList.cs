using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/MonsterList")]
public class MonsterList : ScriptableObject
{
    [SerializeField] private List<Monster> _monsters;

    public Monster GetRandomMonster()
    {
        int index = Random.Range(0, _monsters.Count);
        return _monsters[index];
    }
}
