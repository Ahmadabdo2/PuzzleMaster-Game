using UnityEngine;

/// <summary>
/// إعدادات الشركات الإعلانية - مكان موحد لكل بيانات الإعلانات
/// استبدل القيم الفارغة ببيانات حسابك الفعلي
/// </summary>
public class AdNetworkConfig
{
    // ============================================================
    // 1️⃣ GOOGLE ADMOB - معرفات الإعلانات
    // ============================================================
    // اذهب إلى: https://admob.google.com/
    public static class AdMob
    {
        // ⚙️ معرف التطبيق (من AdMob Dashboard)
        public static string APP_ID = "ca-app-pub-xxxxxxxxxxxxxxxx~yyyyyyyyyy";
        
        // 📱 ANDROID - معرفات الإعلانات
        public static class Android
        {
            // 🅱️ Banner Ad Unit ID
            public static string BANNER_AD_UNIT_ID = "ca-app-pub-3940256099942544/6300978111";
            
            // 📺 Interstitial Ad Unit ID
            public static string INTERSTITIAL_AD_UNIT_ID = "ca-app-pub-3940256099942544/1033173712";
            
            // 🎁 Rewarded Ad Unit ID
            public static string REWARDED_AD_UNIT_ID = "ca-app-pub-3940256099942544/5224354917";
        }
        
        // 🍎 iOS - معرفات الإعلانات
        public static class iOS
        {
            // 🅱️ Banner Ad Unit ID
            public static string BANNER_AD_UNIT_ID = "ca-app-pub-3940256099942544/2934735945";
            
            // 📺 Interstitial Ad Unit ID
            public static string INTERSTITIAL_AD_UNIT_ID = "ca-app-pub-3940256099942544/4411468910";
            
            // 🎁 Rewarded Ad Unit ID
            public static string REWARDED_AD_UNIT_ID = "ca-app-pub-3940256099942544/1712485313";
        }
    }
    
    // ============================================================
    // 2️⃣ IRONSOURCE - معرفات التطبيق
    // ============================================================
    // اذهب إلى: https://www.ironsrc.com/
    public static class IronSource
    {
        // 🔑 App Key (مهم جداً - احصل عليه من Dashboard)
        public static string APP_KEY = "YOUR_IRONSOURCE_APP_KEY";
        
        // 📊 Mediation Provider ID (اختياري)
        public static string PROVIDER_ID = "";
    }
    
    // ============================================================
    // 3️⃣ APPLOVIN MAX - معرفات SDK
    // ============================================================
    // اذهب إلى: https://www.applovin.com/
    public static class AppLovin
    {
        // 🔑 SDK Key (احصل عليه من AppLovin Dashboard)
        public static string SDK_KEY = "YOUR_APPLOVIN_SDK_KEY";
        
        // 📱 ANDROID - معرفات الإعلانات
        public static class Android
        {
            // 🅱️ Banner Ad Unit ID
            public static string BANNER_AD_UNIT_ID = "YOUR_ANDROID_BANNER_AD_UNIT_ID";
            
            // 📺 Interstitial Ad Unit ID
            public static string INTERSTITIAL_AD_UNIT_ID = "YOUR_ANDROID_INTERSTITIAL_AD_UNIT_ID";
            
            // 🎁 Rewarded Ad Unit ID
            public static string REWARDED_AD_UNIT_ID = "YOUR_ANDROID_REWARDED_AD_UNIT_ID";
        }
        
        // 🍎 iOS - معرفات الإعلانات
        public static class iOS
        {
            // 🅱️ Banner Ad Unit ID
            public static string BANNER_AD_UNIT_ID = "YOUR_iOS_BANNER_AD_UNIT_ID";
            
            // 📺 Interstitial Ad Unit ID
            public static string INTERSTITIAL_AD_UNIT_ID = "YOUR_iOS_INTERSTITIAL_AD_UNIT_ID";
            
            // 🎁 Rewarded Ad Unit ID
            public static string REWARDED_AD_UNIT_ID = "YOUR_iOS_REWARDED_AD_UNIT_ID";
        }
    }
    
    // ============================================================
    // 4️⃣ UNITY ADS - معرفات التطبيق
    // ============================================================
    // اذهب إلى: https://unity.com/products/ads
    public static class UnityAds
    {
        // 🔑 Game ID (احصل عليه من Unity Dashboard)
        public static string GAME_ID = "YOUR_UNITY_GAME_ID";
        
        // 📱 ANDROID - معرفات الإعلانات
        public static class Android
        {
            // 📺 Interstitial Placement ID
            public static string INTERSTITIAL_PLACEMENT_ID = "Interstitial_Android";
            
            // 🎁 Rewarded Placement ID
            public static string REWARDED_PLACEMENT_ID = "Rewarded_Android";
            
            // 🅱️ Banner Placement ID (إن أمكن)
            public static string BANNER_PLACEMENT_ID = "Banner_Android";
        }
        
        // 🍎 iOS - معرفات الإعلانات
        public static class iOS
        {
            // 📺 Interstitial Placement ID
            public static string INTERSTITIAL_PLACEMENT_ID = "Interstitial_iOS";
            
            // 🎁 Rewarded Placement ID
            public static string REWARDED_PLACEMENT_ID = "Rewarded_iOS";
            
            // 🅱️ Banner Placement ID (إن أمكن)
            public static string BANNER_PLACEMENT_ID = "Banner_iOS";
        }
    }
    
    // ============================================================
    // 5️⃣ STARTAPP - معرفات التطبيق (خيار إضافي)
    // ============================================================
    // اذهب إلى: https://www.startapp.com/
    public static class StartApp
    {
        // 🔑 App ID (احصل عليه من StartApp Dashboard)
        public static string APP_ID = "YOUR_STARTAPP_APP_ID";
        
        // ⚙️ Is Test Mode
        public static bool IS_TEST_MODE = true; // غيّره إلى false في الإنتاج
    }
    
    // ============================================================
    // 6️⃣ MOPUB - معرفات التطبيق (خيار إضافي)
    // ============================================================
    // اذهب إلى: https://app.mopub.com/
    public static class MoPub
    {
        // 🔑 Ad Unit IDs
        public static string BANNER_AD_UNIT_ID = "YOUR_MOPUB_BANNER_AD_UNIT_ID";
        public static string INTERSTITIAL_AD_UNIT_ID = "YOUR_MOPUB_INTERSTITIAL_AD_UNIT_ID";
        public static string REWARDED_AD_UNIT_ID = "YOUR_MOPUB_REWARDED_AD_UNIT_ID";
    }
    
    // ============================================================
    // 🔧 إعدادات عامة للإعلانات
    // ============================================================
    public static class GeneralSettings
    {
        // 📊 Mediation Strategy - اختر استراتيجية توزيع الإعلانات
        public enum MediationStrategy
        {
            ADMOB_FIRST,      // ابدأ بـ AdMob
            IRONSOURCE_FIRST, // ابدأ بـ IronSource
            APPLOVIN_FIRST,   // ابدأ بـ AppLovin
            ROUND_ROBIN,      // دوّر بين الشركات
            HIGHEST_CPM       // اختر الأعلى سعر (يحتاج API)
        }
        
        public static MediationStrategy ACTIVE_STRATEGY = MediationStrategy.ADMOB_FIRST;
        
        // ⏱️ وقت انتظار الإعلان (ملي ثانية)
        public static int AD_LOAD_TIMEOUT = 5000;
        
        // 📈 تكرار عرض الإعلانات
        public static int INTERSTITIAL_FREQUENCY = 3; // كل 3 مستويات
        public static int BANNER_FREQUENCY = 1;       // دائماً
        
        // 🔕 تفعيل/تعطيل الإعلانات
        public static bool ADS_ENABLED = true;
        
        // 🧪 وضع الاختبار
        public static bool IS_TEST_MODE = true; // غيّره إلى false قبل النشر!
    }
    
    // ============================================================
    // 📋 دليل سريع للربط
    // ============================================================
    /*
     * 
     * 🎯 خطوات الربط:
     * 
     * 1️⃣ GOOGLE ADMOB:
     *    - اذهب إلى https://admob.google.com/
     *    - سجل تطبيق جديد
     *    - انسخ App ID ومعرفات الوحدات الإعلانية
     *    - الصق هنا في AdMob.APP_ID و Android/iOS Units
     * 
     * 2️⃣ IRONSOURCE:
     *    - اذهب إلى https://www.ironsrc.com/
     *    - سجل تطبيق جديد
     *    - انسخ App Key من Dashboard
     *    - الصق هنا في IronSource.APP_KEY
     * 
     * 3️⃣ APPLOVIN:
     *    - اذهب إلى https://www.applovin.com/
     *    - سجل تطبيق جديد
     *    - انسخ SDK Key
     *    - انسخ معرفات الوحدات الإعلانية
     *    - الصق هنا في AppLovin.SDK_KEY و Unit IDs
     * 
     * 4️⃣ UNITY ADS:
     *    - اذهب إلى https://unity.com/products/ads
     *    - سجل تطبيق جديد
     *    - انسخ Game ID
     *    - الصق هنا في UnityAds.GAME_ID
     * 
     * ⚠️ تهم:
     *    ✅ استبدل جميع YOUR_*** بالقيم الفعلية
     *    ✅ استخدم معرفات الاختبار أولاً
     *    ✅ غيّر IS_TEST_MODE إلى false قبل النشر
     *    ✅ احفظ معرفات الإنتاج في مكان آمن
     * 
     */
}
