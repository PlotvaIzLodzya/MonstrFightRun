using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Monster : MonoBehaviour, IMergeable
{
    [SerializeField] private Enlargable _enlargable;

    private int _level = 0;
    private int _maxLevel = 30;

    public int Level => _level;

    public event Action<int> LevelChanged;

    private void Awake()
    {
        _enlargable.SetMaxStep(_maxLevel);
    }

    public void Merge(int level)
    {
        _level+= level;
        _level = Mathf.Clamp(_level, 1, _maxLevel);
        _enlargable.PlayEnlargeAnimation(_level);
        LevelChanged?.Invoke(_level);
    }

    public void UnMerge(int level)
    {
        _level-= level;
        _level = Mathf.Clamp(_level, 1, _maxLevel);
        _enlargable.ShrinkAnimation(_level);
        LevelChanged?.Invoke(_level);
    }
}
