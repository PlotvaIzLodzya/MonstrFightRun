using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinnerDecider : MonoBehaviour
{
    [SerializeField] private GameObject _winScreen;
    private Boss[] _bosses;
    private int _counter;

    public event Action Victory;

    private void Awake()
    {
        if (_winScreen.activeInHierarchy)
            _winScreen.SetActive(false);

        _bosses = FindObjectsOfType<Boss>();
    }

    private void OnEnable()
    {
        foreach (var boss in _bosses)
        {
            boss.GetComponent<Monster>().Died += OnMonsterDied;
        }
    }

    private void OnDisable()
    {
        foreach (var boss in _bosses)
        {
            boss.GetComponent<Monster>().Died -= OnMonsterDied;
        }
    }

    public void OnMonsterDied()
    {
        _counter++;

        if (_counter >= _bosses.Length)
        {
            _winScreen.SetActive(true);
            Victory?.Invoke();
        }
    }
}
