using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class IntegrationMetric
{
    private const string SessionCountName = "sessionCount";
    private const string _regDay = "regDay";

    private string _profileId;
    private const string ProfileId = "ProfileId";
    private const int ProfileIdLength = 10;

    public int SessionCount;

    public void OnGameStart()
    {
        Dictionary<string, object> count = new Dictionary<string, object>();
        count.Add("count", CountSession());

        Amplitude.Instance.logEvent("game_start", count);
        AppMetrica.Instance.ReportEvent("game_start", count);
    }

    public void OnLevelStart(int levelIndex)
    {
        var levelProperty = CreateLevelProperty(levelIndex);

        Amplitude.Instance.logEvent("level_start", levelProperty);
        AppMetrica.Instance.ReportEvent("level_start", levelProperty);
    }

    public void OnLevelComplete(int levelComplitioTime, int levelIndex, int amountCollected, int amountInWallet)
    {
        Dictionary<string, object> userInfo = new Dictionary<string, object> { { "level", levelIndex }, { "time_spent", levelComplitioTime }, { "soft_lvl", amountCollected }, { "soft_all", amountInWallet } };

        Amplitude.Instance.logEvent("level_complete", userInfo);
        AppMetrica.Instance.ReportEvent("level_complete", userInfo);
    }

    public void OnLevelFail(int levelFailTime, int levelIndex, string lostCouse)
    {
        Dictionary<string, object> userInfo = new Dictionary<string, object> { { "level", levelIndex }, { "time_spent", levelFailTime }, { "reason", lostCouse } };

        Amplitude.Instance.logEvent("fail", userInfo);
        AppMetrica.Instance.ReportEvent("fail", userInfo);
    }

    public void OnRestartLevel(int levelIndex)
    {
        var levelProperty = CreateLevelProperty(levelIndex);

        Amplitude.Instance.logEvent("restart", levelProperty);
        AppMetrica.Instance.ReportEvent("restart", levelProperty);
    }

    public void OnSoftCurrencySpend(string type, string name, int currencySpend)
    {
        Dictionary<string, object> userInfo = new Dictionary<string, object> { { "type", type }, { "name", name }, {"amount", currencySpend } };

        Amplitude.Instance.logEvent("soft_spent", userInfo);
        AppMetrica.Instance.ReportEvent("soft_spent", userInfo);
    }

    public void OnAbiltyUsed(string name)
    {
        Dictionary<string, object> userInfo = new Dictionary<string, object> { { "name", name } };

        Amplitude.Instance.logEvent("ability_used", userInfo);
        AppMetrica.Instance.ReportEvent("ability_used", userInfo);
    }

    public void SetUserProperty(Amplitude amplitude)
    {
        amplitude.setUserProperty("session_count", SessionCount);

        YandexAppMetricaUserProfile userProfile = new YandexAppMetricaUserProfile();
        userProfile.Apply(YandexAppMetricaAttribute.CustomCounter("session_count").WithDelta(SessionCount));
        ReportUserProfile(userProfile);

        if (PlayerPrefs.HasKey(_regDay) == false)
        {
            RegDay(amplitude);
        }
        else
        {
            int firstDay = PlayerPrefs.GetInt(_regDay);
            int daysInGame = DateTime.Now.Day - firstDay;

            DaysInGame(amplitude, daysInGame);
        }
    }

    private void RegDay(Amplitude amplitude)
    {
        amplitude.setUserProperty("reg_day", DateTime.Now.ToString());

        YandexAppMetricaUserProfile userProfile = new YandexAppMetricaUserProfile();
        userProfile.Apply(YandexAppMetricaAttribute.CustomString("reg_day").WithValue(DateTime.Now.ToString()));
        ReportUserProfile(userProfile);

        PlayerPrefs.SetInt(_regDay, DateTime.Now.Day);
    }

    private void DaysInGame(Amplitude amplitude, int daysInGame)
    {
        amplitude.setUserProperty("days_in_game", daysInGame);

        YandexAppMetricaUserProfile userProfile = new YandexAppMetricaUserProfile();
        userProfile.Apply(YandexAppMetricaAttribute.CustomCounter("days_in_game").WithDelta(daysInGame));
        ReportUserProfile(userProfile);
    }

    private void ReportUserProfile(YandexAppMetricaUserProfile userProfile)
    {
        AppMetrica.Instance.SetUserProfileID(GetProfileId());
        AppMetrica.Instance.ReportUserProfile(userProfile);
    }

    private string GetProfileId()
    {
        if (PlayerPrefs.HasKey(ProfileId))
        {
            _profileId = PlayerPrefs.GetString(ProfileId);
        }
        else
        {
            _profileId = GenerateProfileId(ProfileIdLength);
            PlayerPrefs.SetString(ProfileId, _profileId);
        }

        return _profileId;
    }

    private string GenerateProfileId(int length)
    {
        const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";

        var random = new System.Random();

        return new string(Enumerable.Repeat(chars, length)
            .Select(letter => letter[random.Next(letter.Length)]).ToArray());
    }

    private Dictionary<string, object> CreateLevelProperty( int levelIndex)
    {
        Dictionary<string, object> level = new Dictionary<string, object>();
        level.Add("level", levelIndex);

        return level;
    }

    private int CountSession()
    {
        int count = 1;

        if (PlayerPrefs.HasKey(SessionCountName))
        {
            count = PlayerPrefs.GetInt(SessionCountName);
            count++;
        }

        PlayerPrefs.SetInt(SessionCountName, count);
        SessionCount = count;

        return count;
    }
}
