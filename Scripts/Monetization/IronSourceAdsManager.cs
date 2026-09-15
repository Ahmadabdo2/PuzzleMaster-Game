using UnityEngine;

public class IronSourceAdsManager : MonoBehaviour
{
    public static IronSourceAdsManager Instance { get; private set; }

    private string appKey = "YOUR_IRONSOURCE_APP_KEY"; // استبدل بـ App Key الخاص بك

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
        InitializeIronSource();
    }

    /// <summary>
    /// تهيئة IronSource SDK
    /// </summary>
    private void InitializeIronSource()
    {
        // استخدم إضافة IronSource من Asset Store
        // IronSource.Agent.init(appKey);
        Debug.Log("📱 IronSource SDK جاهز للربط");
    }

    /// <summary>
    /// عرض إعلان Banner
    /// </summary>
    public void ShowBannerAd()
    {
        // IronSource.Agent.loadBanner(IronSourceBannerSize.BANNER, IronSourceBannerPosition.BOTTOM);
        Debug.Log("📺 عرض إعلان Banner");
    }

    /// <summary>
    /// إخفاء إعلان Banner
    /// </summary>
    public void HideBannerAd()
    {
        // IronSource.Agent.destroyBanner();
        Debug.Log("🚫 إخفاء إعلان Banner");
    }

    /// <summary>
    /// عرض إعلان Interstitial
    /// </summary>
    public void ShowInterstitialAd()
    {
        // if (IronSource.Agent.isInterstitialReady())
        // {
        //     IronSource.Agent.showInterstitial();
        // }
        Debug.Log("📺 عرض إعلان Interstitial");
    }

    /// <summary>
    /// عرض إعلان Rewarded
    /// </summary>
    public void ShowRewardedAd(System.Action onRewardEarned)
    {
        // if (IronSource.Agent.isRewardedVideoAvailable())
        // {
        //     IronSource.Agent.showRewardedVideo();
        // }
        Debug.Log("🎁 عرض إعلان Rewarded");
        onRewardEarned?.Invoke();
    }

    /// <summary>
    /// التحقق من توفر الإعلانات المجانية
    /// </summary>
    public bool IsRewardedVideoAvailable()
    {
        // return IronSource.Agent.isRewardedVideoAvailable();
        return true; // اختبار
    }
}
