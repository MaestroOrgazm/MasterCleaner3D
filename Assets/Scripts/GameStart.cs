using GameAnalyticsSDK;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    private int _sessionCount;
    private DateTime _currentDate;
    private static bool _isWork = false;

    public static int _daysSinceReg { get; private set; }

    private void Start()
    {
        if (!_isWork)
        {
            GameAnalytics.Initialize();
            _sessionCount = PlayerPrefs.GetInt("sessionCount");
            _sessionCount++;
            PlayerPrefs.SetInt("sessionCount", _sessionCount);
            _currentDate = DateTime.UtcNow;

            if (!PlayerPrefs.HasKey("StartDay"))
                PlayerPrefs.SetString("StartDay", DateTime.UtcNow.ToString());

            DateTime startDate = DateTime.Parse(PlayerPrefs.GetString("StartDay"));
            _daysSinceReg = (int)(_currentDate - startDate).TotalDays;
            SetEvent();
        }
    }

    private void SetEvent()
    {
        var properties = new Dictionary<string, object>()
        {
            {"count", _sessionCount},
            {"days_since_reg", _daysSinceReg}
        };

        AnalyticsEvent.SendReportEvent("game_start", properties);
        _isWork = true;
    }
}
