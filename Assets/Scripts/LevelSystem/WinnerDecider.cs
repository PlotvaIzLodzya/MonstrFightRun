using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinnerDecider : MonoBehaviour
{
    [SerializeField] private GameObject _winScreen;

    private Boss[] _bosses;
    private MonsterAnimator[] _monsterAnimators;
    private PlayerDeathHandler _playerDeathHandler;
    private int _counter;

    public event Action Victory;

    private void Awake()
    {
        _playerDeathHandler = FindObjectOfType<PlayerDeathHandler>();
        Error.CheckOnNull(_playerDeathHandler, nameof(PlayerDeathHandler));

        if (_winScreen.activeInHierarchy)
            _winScreen.SetActive(false);

        _bosses = FindObjectsOfType<Boss>();
        Error.CheckOnNull(_bosses, nameof(Boss));
    }

    private void OnEnable()
    {
        _playerDeathHandler.PlayerLost += OnPlayerLost;

        foreach (var boss in _bosses)
        {
            boss.GetComponent<Monster>().Died += OnMonsterDied;
        }
    }

    private void OnDisable()
    {
        _playerDeathHandler.PlayerLost -= OnPlayerLost;

        foreach (var boss in _bosses)
        {
            boss.Monster.Died -= OnMonsterDied;
        }
    }

    public void OnMonsterDied()
    {
        _counter++;

        if (_counter >= _bosses.Length)
        {
            StartCoroutine(DelayedEnable());

            SetVictoryAnimation();

            Victory?.Invoke();
        }
    }

    private IEnumerator DelayedEnable()
    {
        yield return new WaitForSeconds(1f);

        _winScreen.SetActive(true);
    }

    public void OnPlayerLost()
    {
        SetVictoryAnimation();
    }

    private void SetVictoryAnimation()
    {
        _monsterAnimators = FindObjectsOfType<MonsterAnimator>();

        foreach (var monsterAnimator in _monsterAnimators)
        {
            monsterAnimator.VictoryAnimation();
        }
    }
}
