using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLevelPresenter : LevelPresenter
{
    [SerializeField] private Monster _monster;

    private void Start()
    {
        Show(_monster.Level);
    }
}
