using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Monster : MonoBehaviour, IMergeable
{
    [SerializeField] private ResizeAnimation ResizeAnimation;
    [SerializeField] private FormsHandler _formsHandler;

    private int _level = 0;
    private int _maxLevel = 50;

    public int Level => _level;
    private bool IsFinalForm => _formsHandler.IsFinalForm;

    public event Action<int> LevelChanged;

    private void Awake()
    {
        ResizeAnimation.SetMaxStep(_maxLevel);
    }

    public void Merge(int level)
    {
        if (IsFinalForm)
            return;

        LevelUp(level);
        _formsHandler.EnableNextForm();
    }

    public void LevelUp(int level)
    {
        _level += level;
        _level = Mathf.Clamp(_level, 1, _maxLevel);
        ResizeAnimation.PlayEnlargeAnimation(_level);
        LevelChanged?.Invoke(_level);
    }

    public void LevelDown(int level)
    {
        _level -= level;
        _level = Mathf.Clamp(_level, 1, _maxLevel);

        ResizeAnimation.ShrinkAnimation(_level);
        LevelChanged?.Invoke(_level);
    }

    public void UnMerge(int level)
    {
        LevelDown(level);
        _formsHandler.EnablePreviousForm();
        LevelChanged?.Invoke(_level);
    }
}
