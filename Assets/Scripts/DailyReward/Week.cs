using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Week : MonoBehaviour
{
    [SerializeField] private DailyRewardPresenter[] _dailyRewardPresenters;
    [SerializeField] private Timer _timer;

    private int _daysSinceLastVisit;

    private const string SaveName = "WeeklyRewardSaveName";

    private void Awake()
    {
        Init();

        for (int i = 0; i < _dailyRewardPresenters.Length; i++)
        {
            _dailyRewardPresenters[i].Init(i);
        }
    }

    private void OnEnable()
    {
        _timer.DayEnded += OpenNextReward;
    }

    private void OnDisable()
    {
        _timer.DayEnded -= OpenNextReward;
    }

    public void Init()
    {
        if (PlayerPrefs.HasKey(SaveName) == false)
            SetFirstDay();

        int firstDay = PlayerPrefs.GetInt(SaveName);

        //_daysSinceLastVisit = DateTime.Today.Day - firstDay;

        _daysSinceLastVisit = 7;

        if (_daysSinceLastVisit >7)
        {
            _daysSinceLastVisit = 0;
            PlayerPrefs.DeleteKey(SaveName);
            ResetDailyRewards();
        }

        OpenDailyRewards();
    }

    private void OpenNextReward()
    {
        _dailyRewardPresenters.FirstOrDefault(reward => reward.IsOpen == false).Open();
    }

    private void OpenDailyRewards()
    {
        for (int i = 0; i < _daysSinceLastVisit; i++)
        {
            _dailyRewardPresenters[i].Open();
        }
    }

    private void ResetDailyRewards()
    {
        for (int i = 0; i < _dailyRewardPresenters.Length; i++)
        {
            _dailyRewardPresenters[i].Unclaim();
        }
    }

    private void SetFirstDay()
    {
        PlayerPrefs.SetInt(SaveName, DateTime.Now.Day);
    }
}
