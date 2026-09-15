using UnityEngine;

public class AdMediation : MonoBehaviour
{
    public static AdMediation Instance { get; private set; }

    public enum AdNetwork { AdMob, IronSource, AppLovin, UnityAds }

    private AdNetwork currentNetwork = AdNetwork.AdMob;

    private AdsManager admobManager;
    private IronSourceAdsManager ironSourceManager;
    private ApplovinAdsManager applovinManager;
    private UnityAdsManager unityAdsManager;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        InitializeAllNetworks();
    }

    /// <summary>
    /// تهيئة جميع شبكات الإعلانات
    /// </summary>
    private void InitializeAllNetworks()
    {
        admobManager = GetComponent<AdsManager>();
        ironSourceManager = GetComponent<IronSourceAdsManager>();
        applovinManager = GetComponent<ApplovinAdsManager>();
        unityAdsManager = GetComponent<UnityAdsManager>();

        Debug.Log("🌐 تم تهيئة وسيط الإعلانات (Ad Mediation)");
    }

    /// <summary>
    /// تبديل شبكة الإعلانات
    /// </summary>
    public void SetAdNetwork(AdNetwork network)
    {
        currentNetwork = network;
        Debug.Log($"📡 تم التبديل إلى شبكة: {network}");
    }

    /// <summary>
    /// عرض إعلان Banner
    /// </summary>
    public void ShowBannerAd()
    {
        switch (currentNetwork)
        {
            case AdNetwork.AdMob:
                admobManager?.LoadBannerAd();
                break;
            case AdNetwork.IronSource:
                ironSourceManager?.ShowBannerAd();
                break;
            case AdNetwork.AppLovin:
                applovinManager?.ShowBannerAd();
                break;
            case AdNetwork.UnityAds:
                // Unity Ads لا يدعم Banner مباشرة
                break;
        }
    }

    /// <summary>
    /// عرض إعلان Interstitial
    /// </summary>
    public void ShowInterstitialAd()
    {
        switch (currentNetwork)
        {
            case AdNetwork.AdMob:
                admobManager?.ShowInterstitialAd();
                break;
            case AdNetwork.IronSource:
                ironSourceManager?.ShowInterstitialAd();
                break;
            case AdNetwork.AppLovin:
                applovinManager?.ShowInterstitialAd();
                break;
            case AdNetwork.UnityAds:
                unityAdsManager?.ShowInterstitialAd();
                break;
        }
    }

    /// <summary>
    /// عرض إعلان Rewarded
    /// </summary>
    public void ShowRewardedAd(System.Action onRewardEarned)
    {
        switch (currentNetwork)
        {
            case AdNetwork.AdMob:
                admobManager?.ShowRewardedAd(onRewardEarned);
                break;
            case AdNetwork.IronSource:
                ironSourceManager?.ShowRewardedAd(onRewardEarned);
                break;
            case AdNetwork.AppLovin:
                applovinManager?.ShowRewardedAd(onRewardEarned);
                break;
            case AdNetwork.UnityAds:
                unityAdsManager?.ShowRewardedAd(onRewardEarned);
                break;
        }
    }

    /// <summary>
    /// التحقق من توفر الإعلانات المجانية
    /// </summary>
    public bool IsRewardedAdAvailable()
    {
        switch (currentNetwork)
        {
            case AdNetwork.AdMob:
                return true; // AdMob جاهز دائماً في الوضع الاختباري
            case AdNetwork.IronSource:
                return ironSourceManager?.IsRewardedVideoAvailable() ?? false;
            case AdNetwork.AppLovin:
                return applovinManager?.IsRewardedAdReady() ?? false;
            case AdNetwork.UnityAds:
                return unityAdsManager?.IsRewardedVideoReady() ?? false;
            default:
                return false;
        }
    }

    /// <summary>
    /// الحصول على الشبكة الحالية
    /// </summary>
    public AdNetwork GetCurrentNetwork() => currentNetwork;
}
