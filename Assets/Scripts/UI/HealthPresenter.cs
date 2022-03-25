using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthPresenter : MonoBehaviour
{
    [SerializeField] private Monster _monster;
    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_Text health;

    private float _currentValue;
    private float _changeSpeed => _slider.maxValue / 0.4f;

    private Coroutine _coroutine;

    private void OnEnable()
    {
        _monster.Health.HealthChanged += OnHealthChange;
        OnHealthChange(_monster.Health.CurrentHealth, _monster.Health.MaxHealth);
    }

    private void OnDisable()
    {
        _monster.Health.HealthChanged -= OnHealthChange;
    }

    private void OnHealthChange(float currentHealth, float maxHealth)
    {
        _currentValue = currentHealth;
        _slider.maxValue = maxHealth;

        health.text = $"{currentHealth}/{maxHealth}";

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        StartCoroutine(ChangeAnimation());
    }

    private IEnumerator ChangeAnimation()
    {
        while (_slider.value != _currentValue)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, _currentValue, _changeSpeed * Time.deltaTime);

            yield return null;
        }

        _coroutine = null;
    }
}
