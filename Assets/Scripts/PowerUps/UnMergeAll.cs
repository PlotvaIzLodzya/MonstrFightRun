using System.Collections;
using System.Collections.Generic;
using PathCreation;
using UnityEngine;

public class UnMergeAll : PowerUp
{
    [SerializeField] private PowerUpAlongPath _heightHandler = new PowerUpAlongPath();

    private PathCreator _pathCreator;

    private void Start()
    {
        _pathCreator = FindObjectOfType<PathCreator>();
        transform.position = _heightHandler.GetHeight(transform.position, _pathCreator.path);
    }

    public override void Use(MonstersHandler monstersHandler)
    {
        monstersHandler.LevelDownAllMonster();
    }
}
