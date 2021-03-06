using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameLoader : MonoBehaviour
{
    [SerializeField] private LevelsHandler _levelsHandler;

    private IntegrationMetric _integrationMetric = new IntegrationMetric();
    private Amplitude _amplitude;

    private void Awake()
    {
        _amplitude = Amplitude.Instance;
        _amplitude.logging = true;
        _amplitude.init("ff6e88dc4f6c537509627e74706ece7a");
    }

    private void Start()
    {
        _integrationMetric.OnGameStart();
        _levelsHandler.LoadNextLevel();
        _integrationMetric.SetUserProperty(_amplitude);
    }
}
