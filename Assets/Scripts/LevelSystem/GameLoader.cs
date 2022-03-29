using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameLoader : MonoBehaviour
{
    [SerializeField] private LevelsHandler _levelsHandler;

    private IntegrationMetric _integrationMetric = new IntegrationMetric();
    private Amplitude _amplitude;

    private const string _regDay = "regDay";
    private const string _daysInGame = "daysInGame";

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
        _amplitude.setUserProperty("session_count", _integrationMetric.SessionCount);

        if (PlayerPrefs.HasKey(_regDay) == false)
        {
            _amplitude.setUserProperty("reg_day", DateTime.Now.ToString());

            PlayerPrefs.SetInt(_regDay, DateTime.Now.Day);
        }
        else
        {
            int firstDay = PlayerPrefs.GetInt(_regDay);
            int daysInGame = DateTime.Now.Day - firstDay;

            _amplitude.setUserProperty("days_in_game", daysInGame);
        }
    }
}
