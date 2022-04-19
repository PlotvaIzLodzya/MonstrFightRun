using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private MonstersHandler _monstersHandler;
    [SerializeField] private PlayerDeathHandler _deathHandler;
    [SerializeField] private UIHandler _uIHandler;
    [SerializeField] private Collider _collider;

    private int _counter;
    private CurrencyWallet _currencyWallet = new CurrencyWallet();

    public int Might => _monstersHandler.MonsterMight;
    public CurrencyWallet CurrencyWallet => _currencyWallet;
    public UIHandler UiHandler => _uIHandler;

    private void Awake()
    {
        _currencyWallet.LoadAmount();
    }

    private void OnEnable()
    {
        _monstersHandler.CurrencyPickedUp += _currencyWallet.InceaseCurrency;
    }

    private void OnDisable()
    {
        _monstersHandler.CurrencyPickedUp -= _currencyWallet.InceaseCurrency;
    }

    public void RaiseMight(int level)
    {
        _monstersHandler.LevelUpAllMonster(level);
    }

    public void DisableCollider()
    {
        _collider.enabled = false;
    }

    public void Die()
    {
        _deathHandler.Die();
    }

    public void KillAllMonsters()
    {
        _monstersHandler.KillAllMonsters();
    }

    public void OnMonsterDie()
    {
        _counter++;

        if (_counter >= _monstersHandler.MonsterCounter)
            Die();
    }
}
