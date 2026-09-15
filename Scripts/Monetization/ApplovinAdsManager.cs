using UnityEngine;

public class ApplovinAdsManager : MonoBehaviour
{
    public static ApplovinAdsManager Instance { get; private set; }

    private string sdkKey = "YOUR_APPLOVIN_SDK_KEY"; // استبدل بـ SDK Key الخاص بك

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
        InitializeAppLovin();
    }

    /// <summary>
    /// تهيئة AppLovin SDK
    /// </summary>
    private void InitializeAppLovin()
    {
        // استخدم إضافة AppLovin من Asset Store
        // MaxSdk.SetSdkKey(sdkKey);
        // MaxSdk.InitializeSdk();
        Debug.Log("📱 AppLovin SDK جاهز للربط");
    }

    /// <summary>
    /// عرض إعلان Banner
    /// </summary>
    public void ShowBannerAd()
    {
        // MaxSdk.CreateBanner("BANNER_AD_UNIT_ID", MaxSdkBase.BannerPosition.BottomCenter);
        // MaxSdk.ShowBanner("BANNER_AD_UNIT_ID");
        Debug.Log("📺 عرض إعلان Banner من AppLovin");
    }

    /// <summary>
    /// عرض إعلان Interstitial
    /// </summary>
    public void ShowInterstitialAd()
    {
        // if (MaxSdk.IsInterstitialReady("INTERSTITIAL_AD_UNIT_ID"))
        // {
        //     MaxSdk.ShowInterstitial("INTERSTITIAL_AD_UNIT_ID");
        // }
        Debug.Log("📺 عرض إعلان Interstitial من AppLovin");
    }

    /// <summary>
    /// عرض إعلان Rewarded
    /// </summary>
    public void ShowRewardedAd(System.Action onRewardEarned)
    {
        // if (MaxSdk.IsRewardedAdReady("REWARDED_AD_UNIT_ID"))
        // {
        //     MaxSdk.ShowRewardedAd("REWARDED_AD_UNIT_ID");
        // }
        Debug.Log("🎁 عرض إعلان Rewarded من AppLovin");
        onRewardEarned?.Invoke();
    }

    /// <summary>
    /// التحقق من توفر الإعلانات المجانية
    /// </summary>
    public bool IsRewardedAdReady()
    {
        // return MaxSdk.IsRewardedAdReady("REWARDED_AD_UNIT_ID");
        return true; // اختبار
    }
}
