using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using RunnerMovementSystem;

[RequireComponent(typeof(MonsterAnimator), typeof(MonsterDeathHandler), typeof(Rigidbody))]
public class Monster : MonoBehaviour, IMergeable
{
    [SerializeField] private float _health;
    [SerializeField] private float _speed;
    [SerializeField] private int _level;

    private int _maxLevel = 50;
    private ResizeAnimation ResizeAnimation;

    public MovementSystem MovementSystem { get; private set; }
    public MonsterAnimator MonsterAnimator { get; private set; }
    public Monster Target { get; private set; }
    public Health Health { get; private set; }
    public Rigidbody Rigidbody { get; private set; }
    public FormsHandler FormsHandler { get; private set; }
    public MonsterDeathHandler MonsterDeathHandler { get; private set; }
    public bool IsAllive { get; private set; }
    public float Speed => _speed;
    public int Level => _level;
    private float _damage => _level;

    public event Action<int> LevelChanged;
    public event Action DealtDamage;

    private void Awake()
    {
        Health = new Health(_health);
        IsAllive = true;

        Rigidbody = GetComponent<Rigidbody>();

        MonsterAnimator = GetComponent<MonsterAnimator>();
        ResizeAnimation = GetComponentInChildren<ResizeAnimation>();
        FormsHandler = GetComponentInChildren<FormsHandler>();
        MonsterDeathHandler = GetComponent<MonsterDeathHandler>();

        ResizeAnimation.SetMaxStep(_maxLevel);
    }

    public void SetTarget(Monster monster)
    {
        Target = monster;
    }

    public void ApplyDamage(float damage)
    {
        Health.Decrease(damage);

        if (Health.CurrentHealth <= 0)
            Die();
    }

    public void DealDamage()
    {
        DealtDamage?.Invoke();

        if (Target == null)
            return;

        if (Target.IsAllive == false)
        {
            MonsterAnimator.RunAnimation();

            return;
        }

        Target.ApplyDamage(_damage);
    }

    public void Die()
    {
        MonsterDeathHandler.Die();
        IsAllive = false;
    }

    public bool TryMerge(int level)
    {
        if (FormsHandler.TryEnableNextForm())
        {
            LevelUp(level);

            return true;
        }

        return false;
    }

    public void LevelUp(int level)
    {
        _level += level;
        _level = Mathf.Clamp(_level, 0, _maxLevel);
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
        FormsHandler.EnablePreviousForm();
        LevelChanged?.Invoke(_level);
    }
}
