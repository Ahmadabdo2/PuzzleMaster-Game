# 🎯 دليل ربط شبكات الإعلانات

## 📋 المحتويات
1. [AdNetworkConfig.cs](#admnetworkcs) - ملف الإعدادات
2. [UnifiedAdManager.cs](#unifiedadmanagercs) - مدير الإعلانات
3. [خطوات الربط](#خطوات-الربط)
4. [تفعيل كل شركة](#تفعيل-كل-شركة)
5. [الاختبار](#الاختبار)

---

## AdNetworkConfig.cs

### 📁 موقع الملف:
```
Scripts/Monetization/AdNetworkConfig.cs
```

### 📝 محتوى الملف:
ملف إعدادات موحد يحتوي على:
- ✅ معرفات جميع الشركات الإعلانية
- ✅ مكان واحد لتعديل كل البيانات
- ✅ دليل سريع بداخل الملف

---

## UnifiedAdManager.cs

### 📁 موقع الملف:
```
Scripts/Monetization/UnifiedAdManager.cs
```

### 📝 المهام:
- ✅ استخدام بيانات AdNetworkConfig
- ✅ تهيئة جميع الشبكات
- ✅ عرض الإعلانات
- ✅ التحقق من صحة المعرفات

---

## خطوات الربط

### 🔴 الخطوة 1: فتح AdNetworkConfig.cs

```
Assets > Scripts > Monetization > AdNetworkConfig.cs
```

### 🟡 الخطوة 2: ملء البيانات

كل شركة لها قسم خاص:

---

## تفعيل كل شركة

### 1️⃣ Google AdMob (مدمج تماماً ✅)

**الخطوات:**
1. اذهب إلى https://admob.google.com/
2. سجل حسابك وأنشئ تطبيق
3. احصل على:
   - **App ID** (مثال: `ca-app-pub-xxxxxxxxxxxxxxxx~yyyyyyyyyy`)
   - **Banner ID** (مثال: `ca-app-pub-3940256099942544/6300978111`)
   - **Interstitial ID** (مثال: `ca-app-pub-3940256099942544/1033173712`)
   - **Rewarded ID** (مثال: `ca-app-pub-3940256099942544/5224354917`)

**أين تضع البيانات:**
```csharp
// في AdNetworkConfig.cs - الأسطر 12-18
public static class AdMob
{
    public static string APP_ID = "ca-app-pub-xxxxxxxxxxxxxxxx~yyyyyyyyyy"; // ضع هنا
    
    public static class Android
    {
        public static string BANNER_AD_UNIT_ID = "ca-app-pub-3940256099942544/6300978111"; // ضع هنا
        public static string INTERSTITIAL_AD_UNIT_ID = "ca-app-pub-3940256099942544/1033173712"; // ضع هنا
        public static string REWARDED_AD_UNIT_ID = "ca-app-pub-3940256099942544/5224354917"; // ضع هنا
    }
}
```

---

### 2️⃣ IronSource (جاهز للربط)

**الخطوات:**
1. اذهب إلى https://www.ironsrc.com/
2. سجل حسابك وأنشئ تطبيق
3. احصل على **App Key**
4. ابحث عن SDK في Asset Store وثبته

**أين تضع البيانات:**
```csharp
// في AdNetworkConfig.cs - الأسطر 41-48
public static class IronSource
{
    public static string APP_KEY = "YOUR_IRONSOURCE_APP_KEY"; // استبدل بـ App Key الخاص بك
    public static string PROVIDER_ID = "";
}
```

**تفعيل IronSource في الكود:**
```csharp
// في AdNetworkConfig.cs - السطر 106
public static MediationStrategy ACTIVE_STRATEGY = MediationStrategy.IRONSOURCE_FIRST; // تغيير من ADMOB_FIRST
```

---

### 3️⃣ AppLovin MAX (جاهز للربط)

**الخطوات:**
1. اذهب إلى https://www.applovin.com/
2. سجل حسابك وأنشئ تطبيق
3. احصل على:
   - **SDK Key**
   - **Ad Unit IDs** (لكل نوع إعلان)
4. حمّل SDK من Unity Asset Store

**أين تضع البيانات:**
```csharp
// في AdNetworkConfig.cs - الأسطر 50-85
public static class AppLovin
{
    public static string SDK_KEY = "YOUR_APPLOVIN_SDK_KEY"; // ضع SDK Key
    
    public static class Android
    {
        public static string BANNER_AD_UNIT_ID = "YOUR_ANDROID_BANNER_AD_UNIT_ID"; // ضع ID
        public static string INTERSTITIAL_AD_UNIT_ID = "YOUR_ANDROID_INTERSTITIAL_AD_UNIT_ID"; // ضع ID
        public static string REWARDED_AD_UNIT_ID = "YOUR_ANDROID_REWARDED_AD_UNIT_ID"; // ضع ID
    }
}
```

**تفعيل AppLovin في الكود:**
```csharp
// في AdNetworkConfig.cs - السطر 106
public static MediationStrategy ACTIVE_STRATEGY = MediationStrategy.APPLOVIN_FIRST; // تغيير من ADMOB_FIRST
```

---

### 4️⃣ Unity Ads (جاهز للربط)

**الخطوات:**
1. اذهب إلى https://unity.com/products/ads
2. سجل حسابك
3. فعّل Unity Ads في Project
4. احصل على **Game ID**

**أين تضع البيانات:**
```csharp
// في AdNetworkConfig.cs - الأسطر 87-107
public static class UnityAds
{
    public static string GAME_ID = "YOUR_UNITY_GAME_ID"; // ضع Game ID
    
    public static class Android
    {
        public static string INTERSTITIAL_PLACEMENT_ID = "Interstitial_Android";
        public static string REWARDED_PLACEMENT_ID = "Rewarded_Android";
    }
}
```

**تفعيل Unity Ads في الكود:**
```csharp
// في AdNetworkConfig.cs - السطر 106
public static MediationStrategy ACTIVE_STRATEGY = MediationStrategy.ROUND_ROBIN; // توزيع متوازن
```

---

## 🧪 الاختبار

### قبل النشر:

#### ✅ تفعيل وضع الاختبار:
```csharp
// في AdNetworkConfig.cs - السطر 123
public static bool IS_TEST_MODE = true; // أبقِ عليها true للاختبار
```

#### ✅ استخدام معرفات الاختبار:
```csharp
// معرفات اختبار AdMob (تعمل بدون موافقة):
public static string BANNER_AD_UNIT_ID = "ca-app-pub-3940256099942544/6300978111";
public static string INTERSTITIAL_AD_UNIT_ID = "ca-app-pub-3940256099942544/1033173712";
public static string REWARDED_AD_UNIT_ID = "ca-app-pub-3940256099942544/5224354917";
```

#### ✅ التحقق من الكود:
```csharp
// في أي Scene افتح أداة Console ثم:
UnifiedAdManager.Instance.ValidateAllConfigs();

// ستظهر رسائل توضح حالة كل شركة
```

---

## 🚀 بعد الاختبار

### قبل النشر على Google Play:

1. **غيّر معرفات الاختبار بمعرفات الإنتاج:**
```csharp
// استبدل معرفات الاختبار بمعرفاتك الفعلية
public static string BANNER_AD_UNIT_ID = "ca-app-pub-YOUR_REAL_ID"; // معرفك الحقيقي
```

2. **عطّل وضع الاختبار:**
```csharp
public static bool IS_TEST_MODE = false; // غيّر إلى false
```

3. **تحقق من الاستراتيجية:**
```csharp
public static MediationStrategy ACTIVE_STRATEGY = MediationStrategy.ADMOB_FIRST; // أو الشركة المفضلة
```

4. **تأكد من تفعيل الإعلانات:**
```csharp
public static bool ADS_ENABLED = true; // تأكد أنه true
```

---

## 📝 نموذج استخدام سريع

### في أي Script:

```csharp
public class YourGameScript : MonoBehaviour
{
    public void OnLevelComplete()
    {
        // عرض إعلان بين المستويات
        UnifiedAdManager.Instance.ShowInterstitialAd();
    }
    
    public void OnLevelFailed()
    {
        // عرض إعلان مكافأة لمحاولة جديدة
        UnifiedAdManager.Instance.ShowRewardedAd(() => 
        {
            // أعطِ اللاعب مكافأة
            GameManager.Instance.AddCoins(100);
        });
    }
}
```

---

## 📊 مقارنة الشركات

| الشركة | سهولة الربط | أداء | CPM | دعم عربي |
|--------|-----------|------|-----|----------|
| **AdMob** | ⭐⭐⭐⭐⭐ | ⭐⭐⭐⭐⭐ | متوسط | نعم |
| **IronSource** | ⭐⭐⭐⭐ | ⭐⭐⭐⭐ | عالي | نعم |
| **AppLovin** | ⭐⭐⭐⭐ | ⭐⭐⭐⭐⭐ | عالي جداً | نعم |
| **Unity Ads** | ⭐⭐⭐⭐ | ⭐⭐⭐ | منخفض | نعم |

---

## ❓ الأسئلة الشائعة

**س: ماذا إذا لم أملأ بيانات شركة معينة؟**
ج: سيتم تخطيها والانتقال للشركة التالية في الاستراتيجية

**س: هل يمكن استخدام عدة شركات معاً؟**
ج: نعم! غيّر `MediationStrategy` إلى `ROUND_ROBIN`

**س: كم الوقت بين البيانات والظهور؟**
ج: عادة 24 ساعة بعد إضافة معرفات جديدة

**س: ماذا لو ظهرت أخطاء؟**
ج: افتح Console وشوف الرسائل - كل رسالة توضح المشكلة والحل

---

## 🎉 انتهيت!

الآن كل ما تحتاجه هو:
1. ✅ ملء بيانات الشركات في AdNetworkConfig.cs
2. ✅ اختبار باستخدام معرفات الاختبار
3. ✅ تبديل معرفات الإنتاج قبل النشر
4. ✅ تشغيل ValidateAllConfigs() للتحقق

**والنجاح بإذن الله!** 🚀
