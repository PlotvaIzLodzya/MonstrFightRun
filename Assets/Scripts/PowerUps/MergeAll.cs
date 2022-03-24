using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class MergeAll : PowerUp
{
    [SerializeField] private PowerUpAlongPath _heightHandler = new PowerUpAlongPath();

    private PathCreator _pathCreator;

    private void Start()
    {
        _pathCreator = FindObjectOfType<PathCreator>();
        transform.position = _heightHandler.GetHeight(transform.position, _pathCreator.path);
        _pathCreator.path.GetClosestPointOnPath(transform.position);
    }

    public override void Use(MonstersHandler monstersHandler)
    {
        monstersHandler.LevelUpAllMonster(1);
    }
}
