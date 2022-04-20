using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Health
{
    [SerializeField] private float _healthPerLevel;

    private float _maxHealth;
    private float _currentHealth;

    public float MaxHealth => _maxHealth;
    public float CurrentHealth => _currentHealth;

    public event Action<float, float> HealthChanged;

    public void Init(float level)
    {
        var health = CalctulateHealth(level);

        _maxHealth = health;
        _currentHealth = health;

        HealthChanged?.Invoke(_currentHealth, _maxHealth);
    }

    public void Decrease(float damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0)
            _currentHealth = 0;

        HealthChanged?.Invoke(_currentHealth, _maxHealth);
    }

    public void Increase(float health)
    {
        _currentHealth += health;

        if (_currentHealth >= MaxHealth)
            _currentHealth = MaxHealth;
    }

    public void IncreaseMaxHealth(float level)
    {
        var health = CalctulateHealth(level);

        _maxHealth = health;
        _currentHealth = health;

        HealthChanged?.Invoke(_currentHealth, _maxHealth);
    }

    private float CalctulateHealth(float level)
    {
        return level * _healthPerLevel;
    }
}
