# دليل دمج شبكات الإعلانات

## دعم شبكات الإعلانات المتعددة

تم بناء المشروع بـ نظام **Ad Mediation** متقدم يدعم:

### ✅ 4 شبكات إعلانات
1. **Google AdMob** (مدمج بالكامل)
2. **IronSource** (جاهز للربط)
3. **AppLovin** (جاهز للربط)
4. **Unity Ads** (جاهز للربط)

---

## خطوات الربط

### 1️⃣ Google AdMob (بالفعل مدمج)
```csharp
// File: Scripts/Monetization/AdsManager.cs
// - معرفات الاختبار موجودة
// - جاهز للعمل الفوري
```

### 2️⃣ IronSource

#### الخطوة أ: التسجيل
1. اذهب إلى: https://www.ironsrc.com/
2. سجل حسابك
3. أنشئ تطبيق جديد
4. احصل على **App Key**

#### الخطوة ب: التثبيت
1. فتح Window > TextMesh Pro > Import TMP Essential Resources
2. اذهب إلى IronSource موقع الويب وحمّل SDK
3. أضف `YOUR_IRONSOURCE_APP_KEY` في:
   ```csharp
   // File: Scripts/Monetization/IronSourceAdsManager.cs - السطر 9
   private string appKey = "YOUR_IRONSOURCE_APP_KEY";
   ```

#### الخطوة ج: الاختبار
```csharp
// في GameManager أو أي Scene
AdMediation.Instance.SetAdNetwork(AdMediation.AdNetwork.IronSource);
AdMediation.Instance.ShowInterstitialAd();
```

---

### 3️⃣ AppLovin (MAX)

#### الخطوة أ: التسجيل
1. اذهب إلى: https://www.applovin.com/
2. سجل الحساب
3. أنشئ تطبيق
4. احصل على **SDK Key**

#### الخطوة ب: التثبيت
1. حمّل MAX SDK من Unity Asset Store:
   - ابحث عن "AppLovin MAX"
   - انقر "Import"

2. أضف `YOUR_APPLOVIN_SDK_KEY` في:
   ```csharp
   // File: Scripts/Monetization/ApplovinAdsManager.cs - السطر 9
   private string sdkKey = "YOUR_APPLOVIN_SDK_KEY";
   ```

3. احصل على معرفات الإعلانات:
   - استبدل `BANNER_AD_UNIT_ID`
   - استبدل `INTERSTITIAL_AD_UNIT_ID`
   - استبدل `REWARDED_AD_UNIT_ID`

#### الخطوة ج: الاختبار
```csharp
AdMediation.Instance.SetAdNetwork(AdMediation.AdNetwork.AppLovin);
AdMediation.Instance.ShowRewardedAd(() => {
    Debug.Log("حصل اللاعب على مكافأة");
});
```

---

### 4️⃣ Unity Ads

#### الخطوة أ: التفعيل
1. فتح Window > TextMesh Pro > Import TMP Essential Resources
2. اذهب إلى Unity Gaming Services Dashboard
3. فعّل Unity Ads

#### الخطوة ب: الإعدادات
1. أضف **Game ID** في:
   ```csharp
   // File: Scripts/Monetization/UnityAdsManager.cs - السطر 9
   private string gameID = "YOUR_UNITY_GAME_ID";
   ```

2. في Unity Editor:
   - Edit > Project Settings > Services
   - فعّل "Ads" Service

#### الخطوة ج: الاختبار
```csharp
AdMediation.Instance.SetAdNetwork(AdMediation.AdNetwork.UnityAds);
AdMediation.Instance.ShowInterstitialAd();
```

---

## استخدام نظام Ad Mediation

### عرض إعلان بناءً على الشبكة المختارة:
```csharp
// استيراد الأساسيات
using UnityEngine;

public class YourGameScript : MonoBehaviour
{
    public void ShowAd()
    {
        // تبديل إلى IronSource
        AdMediation.Instance.SetAdNetwork(AdMediation.AdNetwork.IronSource);
        AdMediation.Instance.ShowInterstitialAd();
    }

    public void ShowRewardedAd()
    {
        // البقاء على AdMob
        AdMediation.Instance.ShowRewardedAd(() => {
            Debug.Log("✓ حصل اللاعب على مكافأة");
            GameManager.Instance.AddCoins(100);
        });
    }
}
```

---

## توزيع الإعلانات الموصى به

```
📊 نموذج الإيرادات الأمثل:

30% - Google AdMob (موثوق وسهل)
30% - IronSource (معدل ملء عالي)
20% - AppLovin (معدلات جيدة)
20% - Unity Ads (تعويض)

✅ الفائدة: إذا كانت شبكة واحدة معطلة، تعمل الأخرى
```

---

## قائمة الأشياء المطلوبة

### قبل النشر:
- [ ] استبدل معرفات AdMob (اختياري - معرفات الاختبار تعمل الآن)
- [ ] استبدل IronSource App Key
- [ ] استبدل AppLovin SDK Key و Ad Unit IDs
- [ ] استبدل Unity Game ID
- [ ] اختبر جميع الشبكات قبل الإطلاق
- [ ] تحقق من إعدادات الخصوصية لكل شبكة
- [ ] أضف سياسة الخصوصية في وصف التطبيق

---

## استكشاف الأخطاء

### الإعلانات لا تظهر؟
1. تحقق من معرفات الإعلانات
2. تأكد من الاتصال بالإنترنت
3. استخدم معرفات الاختبار أولاً
4. انتظر 24 ساعة لتفعيل الحسابات الجديدة
5. تحقق من سجلات Unity (Ctrl+Shift+C)

### معدل الملء منخفض؟
1. أضف وسيط (Mediation) متعدد
2. استخدم معدل عرض مثالي (كل 3-5 مستويات)
3. قدم إعلانات مجانية بدلاً من الإجبار
4. راقب Analytics من كل شبكة

---

## دعم إضافي

### مصادر مفيدة:
- **Google AdMob**: https://admob.google.com/
- **IronSource**: https://www.ironsrc.com/
- **AppLovin MAX**: https://www.applovin.com/
- **Unity Ads**: https://unity.com/products/ads

### للدعم الفني:
اتصل بفريق الدعم لكل شبكة من خلال لوحة تحكمهم
