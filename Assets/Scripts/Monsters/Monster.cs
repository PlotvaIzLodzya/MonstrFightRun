using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(MonsterAnimator), typeof(BoxCollider), typeof(Rigidbody))]
public class Monster : MonoBehaviour, IMergeable
{
    [SerializeField] private float _health;
    [SerializeField] private float _speed;
    [SerializeField] private float _attackDelay;
    [SerializeField] private int _level;
    [SerializeField] private ResizeAnimation ResizeAnimation;
    [SerializeField] private FormsHandler _formsHandler;
    [SerializeField] private MonsterDeathHandler _monsterDeathHandler;

    private int _maxLevel = 50;
    private BoxCollider _boxCollider;

    public MonsterAnimator MonsterAnimator { get; private set; }
    public bool IsAllive { get; private set; }
    public Monster Target { get; private set; }
    public Health Health { get; private set; }
    public Rigidbody Rigidbody { get; private set; }
    public FormsHandler FormsHandler => _formsHandler;
    public MonsterDeathHandler MonsterDeathHandler => _monsterDeathHandler;
    public float AttackDelay => _attackDelay;
    public float Speed => _speed;
    public int Level => _level;
    private float _damage => _level;

    public event Action<int> LevelChanged;

    private void Awake()
    {
        Health = new Health(_health);
        IsAllive = true;

        _boxCollider = GetComponent<BoxCollider>();
        _boxCollider.center = Vector3.up * 0.5f;

        Rigidbody = GetComponent<Rigidbody>();

        MonsterAnimator = GetComponent<MonsterAnimator>();
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

    public void GiveDamage()
    {
        if (Target.IsAllive == false)
        {
            MonsterAnimator.RunAnimation();

            return;
        }

        Target.ApplyDamage(_damage);  
    }

    public void Die()
    {
        _monsterDeathHandler.Die();
        IsAllive = false;
    }

    public bool TryMerge(int level)
    {
        if (_formsHandler.TryEnableNextForm())
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
        _formsHandler.EnablePreviousForm();
        LevelChanged?.Invoke(_level);
    }
}
