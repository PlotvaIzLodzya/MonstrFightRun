using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteAll : AbilitiyButtonView
{
    [SerializeField] private Meteorite _meteorite;

    public override void Cast()
    {
        Vector3 spawnPoint = Camera.main.transform.GetComponentInChildren<MeteoritePoint>().transform.position;

        var meteorite = Instantiate(_meteorite, spawnPoint, Quaternion.identity);
        meteorite.Init(ValueHandler.Amount);
    }
}
