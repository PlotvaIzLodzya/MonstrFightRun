using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseScreenEnabler : MonoBehaviour
{
    [SerializeField] private LoseScreen _loseScreen;

    private PlayerDeathHandler _playerDeathHandler;

    private void Awake()
    {
        _loseScreen.gameObject.SetActive(false);
        _playerDeathHandler = FindObjectOfType<PlayerDeathHandler>();
    }

    private void OnEnable()
    {
        _playerDeathHandler.PlayerLost += Enable;
    }

    private void OnDisable()
    {
        _playerDeathHandler.PlayerLost -= Enable;
    }

    private void Enable()
    {
        _loseScreen.gameObject.SetActive(true);
    }
}
