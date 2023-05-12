using GameAnalyticsSDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalyticsEvent : MonoBehaviour
{
    public static void SendReportEvent(string massage, Dictionary<string, object> properties)
    {
        GameAnalytics.NewDesignEvent(massage, properties);
        AppMetrica.Instance.ReportEvent(massage, properties);
    }
}
