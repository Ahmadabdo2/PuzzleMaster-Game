using UnityEngine;

public class UnityAdsManager : MonoBehaviour
{
    public static UnityAdsManager Instance { get; private set; }

    private string gameID = "YOUR_UNITY_GAME_ID"; // استبدل بـ Game ID الخاص بك

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

    private void Start()
    {
        InitializeUnityAds();
    }

    /// <summary>
    /// تهيئة Unity Ads SDK
    /// </summary>
    private void InitializeUnityAds()
    {
        // استخدم مدير Unity Ads من Dashboard
        // UnityAds.Initialize(gameID);
        Debug.Log("📱 Unity Ads SDK جاهز للربط");
    }

    /// <summary>
    /// عرض إعلان Non-Skippable
    /// </summary>
    public void ShowInterstitialAd()
    {
        // ShowAd("video");
        Debug.Log("📺 عرض إعلان Non-Skippable");
    }

    /// <summary>
    /// عرض إعلان Rewarded
    /// </summary>
    public void ShowRewardedAd(System.Action onRewardEarned)
    {
        // ShowAd("rewardedVideo");
        Debug.Log("🎁 عرض إعلان Rewarded من Unity Ads");
        onRewardEarned?.Invoke();
    }

    /// <summary>
    /// عرض الإعلان
    /// </summary>
    private void ShowAd(string placementId)
    {
        // if (UnityAds.isInitialized && UnityAds.IsReady(placementId))
        // {
        //     UnityAds.Show(placementId);
        // }
    }

    /// <summary>
    /// التحقق من توفر الإعلانات المجانية
    /// </summary>
    public bool IsRewardedVideoReady()
    {
        // return UnityAds.isInitialized && UnityAds.IsReady("rewardedVideo");
        return true; // اختبار
    }
}
