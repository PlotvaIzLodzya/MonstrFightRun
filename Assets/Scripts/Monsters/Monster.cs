using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using RunnerMovementSystem;

[RequireComponent(typeof(MonsterAnimator), typeof(MonsterDeathHandler), typeof(Rigidbody))]
public class Monster : MonoBehaviour, IMergeable
{
    [SerializeField] private Health _health = new Health();
    [SerializeField] private float _speed;
    [SerializeField] private int _level;

    public string Name;
    private int _maxLevel = 50;
    private ResizeAnimation ResizeAnimation;

    public MovementSystem MovementSystem { get; private set; }
    public MonsterAnimator MonsterAnimator { get; private set; }
    public Monster Target { get; private set; }
    public Rigidbody Rigidbody { get; private set; }
    public FormsHandler FormsHandler { get; private set; }
    public MonsterDeathHandler MonsterDeathHandler { get; private set; }
    public bool IsAllive { get; private set; }
    public Health Health => _health;
    public float Speed => _speed;
    public int Level => _level;
    private float _damage => _level;

    public event Action<int> LevelChanged;
    public event Action<float> Damaged;
    public event Action DealtDamage;
    public event Action Died;
    public event Action<int> HittedAnObstacle;

    private void Awake()
    {
        IsAllive = true;

        Rigidbody = GetComponent<Rigidbody>();

        MonsterAnimator = GetComponent<MonsterAnimator>();
        ResizeAnimation = GetComponentInChildren<ResizeAnimation>();
        FormsHandler = GetComponentInChildren<FormsHandler>();
        MonsterDeathHandler = GetComponent<MonsterDeathHandler>();

        ResizeAnimation.SetMaxStep(_maxLevel);

        _health.Init(_level);
    }

    public void SetTarget(Monster monster)
    {
        Target = monster;
    }

    public void ApplyDamage(float damage)
    {
        Health.Decrease(damage);

        Damaged?.Invoke(damage);

        FormsHandler.CurrentForm.OnDamaged();

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
        if (IsAllive)
        {
            MonsterDeathHandler.Die();
            IsAllive = false;
            Died?.Invoke();
        }
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
        _health.IncreaseMaxHealth(_level);
        LevelChanged?.Invoke(_level);
    }

    public void ObstacleHitted(int level)
    {
        HittedAnObstacle?.Invoke(level);
        LevelDown(level);
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
