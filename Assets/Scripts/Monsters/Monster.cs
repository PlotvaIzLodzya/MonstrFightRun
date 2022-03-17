using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Monster : MonoBehaviour, IMergeable
{
    [SerializeField] private Enlargable _enlargable;

    private int _level = 0;
    private int _maxLevel = 20;

    public event Action<int> LevelChanged;

    private void Awake()
    {
        _enlargable.SetMaxStep(_maxLevel);
    }

    public void Merge()
    {
        _level++;
        _level = Mathf.Clamp(_level, 0, _maxLevel);
        _enlargable.PlayEnlargeAnimation();
        LevelChanged?.Invoke(_level);
    }

    public void UnMerge()
    {
        _level--;
        _level = Mathf.Clamp(_level, 0, _maxLevel);
        _enlargable.ShrinkAnimation(_level);
        LevelChanged?.Invoke(_level);
    }
}
