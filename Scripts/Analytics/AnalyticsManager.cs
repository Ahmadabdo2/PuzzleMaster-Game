using UnityEngine;
using Firebase.Analytics;
using System.Collections.Generic;

public class AnalyticsManager : MonoBehaviour
{
    public static AnalyticsManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void LogLevelStart(int levelNumber)
    {
        FirebaseAnalytics.LogEvent("level_start", new Parameter("level", levelNumber));
    }

    public void LogLevelComplete(int levelNumber, int stars, int score)
    {
        var parameters = new[]
        {
            new Parameter("level", levelNumber),
            new Parameter("stars", stars),
            new Parameter("score", score)
        };
        FirebaseAnalytics.LogEvent("level_complete", parameters);
    }

    public void LogPurchase(string itemId, long price, string currency)
    {
        var parameters = new[]
        {
            new Parameter("item_id", itemId),
            new Parameter("price", price),
            new Parameter("currency", currency)
        };
        FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventPurchase, parameters);
    }

    public void LogAdImpression(string adType)
    {
        FirebaseAnalytics.LogEvent("ad_impression", new Parameter("ad_type", adType));
    }

    public void LogDailyActive()
    {
        FirebaseAnalytics.LogEvent("daily_active");
    }

    public void LogShareEvent(string levelNumber)
    {
        FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventShare, new Parameter("level", levelNumber));
    }
}
