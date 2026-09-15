using UnityEngine;
using GoogleMobileAds.Client;

/// <summary>
/// مدير موحد لجميع شبكات الإعلانات
/// يستخدم AdNetworkConfig لسهولة الربط
/// </summary>
public class UnifiedAdManager : MonoBehaviour
{
    public static UnifiedAdManager Instance { get; private set; }

    private BannerView bannerView;
    private InterstitialAd interstitialAd;
    private RewardedAd rewardedAd;
    private int levelsSinceLastInterstitial = 0;

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
        if (AdNetworkConfig.GeneralSettings.ADS_ENABLED)
        {
            InitializeAllAds();
        }
        else
        {
            Debug.LogWarning("⚠️ الإعلانات معطلة - ADS_ENABLED = false");
        }
    }

    /// <summary>
    /// تهيئة جميع شبكات الإعلانات بناءً على الاستراتيجية المختارة
    /// </summary>
    private void InitializeAllAds()
    {
        Debug.Log($"🎬 تهيئة الإعلانات - الاستراتيجية: {AdNetworkConfig.GeneralSettings.ACTIVE_STRATEGY}");

        switch (AdNetworkConfig.GeneralSettings.ACTIVE_STRATEGY)
        {
            case AdNetworkConfig.GeneralSettings.MediationStrategy.ADMOB_FIRST:
                InitializeAdMob();
                break;

            case AdNetworkConfig.GeneralSettings.MediationStrategy.IRONSOURCE_FIRST:
                InitializeIronSource();
                break;

            case AdNetworkConfig.GeneralSettings.MediationStrategy.APPLOVIN_FIRST:
                InitializeAppLovin();
                break;

            case AdNetworkConfig.GeneralSettings.MediationStrategy.ROUND_ROBIN:
                InitializeAdMob();
                // يمكن إضافة منطق التنسيق هنا
                break;
        }
    }

    // ============================================================
    // ADMOB - جاهز تماماً
    // ============================================================
    private void InitializeAdMob()
    {
        Debug.Log("📱 تهيئة Google AdMob...");
        
        MobileAds.Initialize(initStatus => { });

        // تحديد المعرفات بناءً على النظام الأساسي
        string bannerUnitId = GetPlatformSpecificId(AdNetworkConfig.AdMob.Android.BANNER_AD_UNIT_ID, 
                                                     AdNetworkConfig.AdMob.iOS.BANNER_AD_UNIT_ID);
        string interstitialUnitId = GetPlatformSpecificId(AdNetworkConfig.AdMob.Android.INTERSTITIAL_AD_UNIT_ID, 
                                                           AdNetworkConfig.AdMob.iOS.INTERSTITIAL_AD_UNIT_ID);
        string rewardedUnitId = GetPlatformSpecificId(AdNetworkConfig.AdMob.Android.REWARDED_AD_UNIT_ID, 
                                                       AdNetworkConfig.AdMob.iOS.REWARDED_AD_UNIT_ID);

        LoadBannerAd(bannerUnitId);
        LoadInterstitialAd(interstitialUnitId);
        LoadRewardedAd(rewardedUnitId);

        Debug.Log("✅ Google AdMob جاهز");
    }

    // ============================================================
    // IRONSOURCE - جاهز للربط
    // ============================================================
    private void InitializeIronSource()
    {
        Debug.Log("📱 تهيئة IronSource...");
        
        if (string.IsNullOrEmpty(AdNetworkConfig.IronSource.APP_KEY))
        {
            Debug.LogError("❌ IronSource App Key فارغ! ضع القيمة في AdNetworkConfig.IronSource.APP_KEY");
            return;
        }

        // تعليق: هنا ستضع كود تهيئة IronSource
        // IronSource.Agent.init(AdNetworkConfig.IronSource.APP_KEY);
        
        Debug.Log($"✅ IronSource جاهز - App Key: {AdNetworkConfig.IronSource.APP_KEY}");
    }

    // ============================================================
    // APPLOVIN - جاهز للربط
    // ============================================================
    private void InitializeAppLovin()
    {
        Debug.Log("📱 تهيئة AppLovin...");
        
        if (string.IsNullOrEmpty(AdNetworkConfig.AppLovin.SDK_KEY))
        {
            Debug.LogError("❌ AppLovin SDK Key فارغ! ضع القيمة في AdNetworkConfig.AppLovin.SDK_KEY");
            return;
        }

        // تعليق: هنا ستضع كود تهيئة AppLovin
        // MaxSdk.SetSdkKey(AdNetworkConfig.AppLovin.SDK_KEY);
        // MaxSdk.InitializeSdk();
        
        Debug.Log($"✅ AppLovin جاهز - SDK Key: {AdNetworkConfig.AppLovin.SDK_KEY}");
    }

    // ============================================================
    // UNITY ADS - جاهز للربط
    // ============================================================
    private void InitializeUnityAds()
    {
        Debug.Log("📱 تهيئة Unity Ads...");
        
        if (string.IsNullOrEmpty(AdNetworkConfig.UnityAds.GAME_ID))
        {
            Debug.LogError("❌ Unity Ads Game ID فارغ! ضع القيمة في AdNetworkConfig.UnityAds.GAME_ID");
            return;
        }

        // تعليق: هنا ستضع كود تهيئة Unity Ads
        // UnityAds.Initialize(AdNetworkConfig.UnityAds.GAME_ID);
        
        Debug.Log($"✅ Unity Ads جاهز - Game ID: {AdNetworkConfig.UnityAds.GAME_ID}");
    }

    // ============================================================
    // تحميل الإعلانات (AdMob)
    // ============================================================
    private void LoadBannerAd(string adUnitId)
    {
        var adRequest = new AdRequest();
        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);
        bannerView.LoadAd(adRequest);
        Debug.Log($"📱 تم تحميل Banner: {adUnitId}");
    }

    private void LoadInterstitialAd(string adUnitId)
    {
        var adRequest = new AdRequest();
        InterstitialAd.Load(adUnitId, adRequest, (InterstitialAd ad, LoadAdError error) =>
        {
            if (error == null)
            {
                interstitialAd = ad;
                Debug.Log($"📺 تم تحميل Interstitial: {adUnitId}");
            }
            else
            {
                Debug.LogError($"❌ فشل تحميل Interstitial: {error}");
            }
        });
    }

    private void LoadRewardedAd(string adUnitId)
    {
        var adRequest = new AdRequest();
        RewardedAd.Load(adUnitId, adRequest, (RewardedAd ad, LoadAdError error) =>
        {
            if (error == null)
            {
                rewardedAd = ad;
                Debug.Log($"🎁 تم تحميل Rewarded: {adUnitId}");
            }
            else
            {
                Debug.LogError($"❌ فشل تحميل Rewarded: {error}");
            }
        });
    }

    // ============================================================
    // عرض الإعلانات
    // ============================================================

    /// <summary>
    /// عرض إعلان Interstitial (بناءً على التكرار)
    /// </summary>
    public void ShowInterstitialAdIfReady()
    {
        levelsSinceLastInterstitial++;

        if (levelsSinceLastInterstitial >= AdNetworkConfig.GeneralSettings.INTERSTITIAL_FREQUENCY)
        {
            ShowInterstitialAd();
            levelsSinceLastInterstitial = 0;
        }
    }

    /// <summary>
    /// عرض إعلان Interstitial مباشرة
    /// </summary>
    public void ShowInterstitialAd()
    {
        if (interstitialAd != null && interstitialAd.CanShowAd())
        {
            Debug.Log("🎬 عرض Interstitial Ad");
            interstitialAd.Show();
        }
        else
        {
            Debug.LogWarning("⚠️ Interstitial Ad ليس جاهزاً");
        }
    }

    /// <summary>
    /// عرض إعلان Rewarded مع مكافأة
    /// </summary>
    public void ShowRewardedAd(System.Action onRewardEarned, System.Action onAdClosed = null)
    {
        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            Debug.Log("🎁 عرض Rewarded Ad");
            rewardedAd.Show((Reward reward) =>
            {
                Debug.Log($"✅ حصل اللاعب على: {reward.Amount} {reward.Type}");
                onRewardEarned?.Invoke();
            });
        }
        else
        {
            Debug.LogWarning("⚠️ Rewarded Ad ليس جاهزاً");
            // يمكن إعطاء المكافأة بدون إعلان (حسب السياسة)
        }
    }

    /// <summary>
    /// التحقق من وجود معرفات صحيحة
    /// </summary>
    public bool ValidateAllConfigs()
    {
        bool isValid = true;

        // التحقق من AdMob
        if (string.IsNullOrEmpty(AdNetworkConfig.AdMob.APP_ID) || AdNetworkConfig.AdMob.APP_ID.Contains("YOUR_"))
        {
            Debug.LogWarning("⚠️ AdMob App ID غير مكتمل");
            isValid = false;
        }

        // التحقق من IronSource
        if (string.IsNullOrEmpty(AdNetworkConfig.IronSource.APP_KEY) || AdNetworkConfig.IronSource.APP_KEY.Contains("YOUR_"))
        {
            Debug.LogWarning("⚠️ IronSource App Key غير مكتمل");
        }

        // التحقق من AppLovin
        if (string.IsNullOrEmpty(AdNetworkConfig.AppLovin.SDK_KEY) || AdNetworkConfig.AppLovin.SDK_KEY.Contains("YOUR_"))
        {
            Debug.LogWarning("⚠️ AppLovin SDK Key غير مكتمل");
        }

        if (isValid && !AdNetworkConfig.GeneralSettings.IS_TEST_MODE)
        {
            Debug.Log("✅ جميع المعرفات صحيحة!");
        }
        else if (AdNetworkConfig.GeneralSettings.IS_TEST_MODE)
        {
            Debug.Log("🧪 وضع الاختبار مفعّل - استخدم معرفات الاختبار");
        }

        return isValid;
    }

    /// <summary>
    /// الحصول على المعرف بناءً على النظام الأساسي
    /// </summary>
    private string GetPlatformSpecificId(string androidId, string iosId)
    {
        #if UNITY_ANDROID
            return androidId;
        #elif UNITY_IOS
            return iosId;
        #else
            return androidId; // الافتراضي
        #endif
    }
}
