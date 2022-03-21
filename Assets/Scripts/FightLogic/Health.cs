using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health
{
    private float _maxHealth;
    private float _currentHealth;

    public float MaxHealth => _maxHealth;
    public float CurrentHealth => _currentHealth;

    public Health(float maxHealth)
    {
        _maxHealth = maxHealth;
        _currentHealth = maxHealth;
    }

    public void Decrease(float damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0)
            _currentHealth = 0;
    }

    public void IncreaseMaxHealth(float health)
    {
        _maxHealth += health;
    }
}
