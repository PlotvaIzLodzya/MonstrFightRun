using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectAll : AbilitiyButtonView
{
    private IEnumerable<Monster> _monsters;

    public override void Cast()
    {
        if(_monsters == null)
            _monsters = FindObjectOfType<MonstersHandler>().GetAllMonsters();

        StartCoroutine(ProtectionTime(ValueHandler.Amount));
    }

    private IEnumerator ProtectionTime(float time)
    {
        foreach (var monster in _monsters)
        {
            monster.Protected = true;
        }

        yield return new WaitForSeconds(time);

        foreach (var monster in _monsters)
        {
            monster.Protected = false;
        }
    }
}
