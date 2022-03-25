using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class MergeAll : PowerUp
{
    [SerializeField] private PositionAlongPath _heightHandler = new PositionAlongPath();

    private PathCreator _pathCreator;

    private void Start()
    {
        _pathCreator = FindObjectOfType<PathCreator>();
        transform.position = _heightHandler.GetPosition(transform.position, _pathCreator.path);
        _pathCreator.path.GetClosestPointOnPath(transform.position);
    }

    public override void Use(MonstersHandler monstersHandler)
    {
        monstersHandler.LevelUpAllMonster(1);
    }
}
