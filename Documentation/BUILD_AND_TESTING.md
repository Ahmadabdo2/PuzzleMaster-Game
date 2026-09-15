# متطلبات البناء والاختبار

## بيئة التطوير

### الأجهزة المدعومة:
```
Android:
├─ Phone: ✓ (4.5" - 6.7")
├─ Tablet: ✓
└─ Minimum SDK: 24
    Target SDK: 33

iOS:
├─ iPhone: ✓ (6s والإصدارات الأحدث)
├─ iPad: ✓
└─ iOS: 12.0+
```

### مواصفات الأداء:
```
الحد الأدنى:
├─ CPU: Snapdragon 625 / A10 Bionic
├─ RAM: 2GB
├─ Storage: 100MB
└─ Screen: 5" / 720x1280

موصى به:
├─ CPU: Snapdragon 855+ / A14 Bionic
├─ RAM: 4GB+
├─ Storage: 500MB
└─ Screen: 6.1" / 2340x1080
```

## خطوات الاختبار

### 1. اختبار وحدة (Unit Testing)
```csharp
// مثال اختبار GameManager
using NUnit.Framework;

public class GameManagerTests
{
    [Test]
    public void AddCoins_IncrementsBalance()
    {
        GameManager gm = GameManager.Instance;
        long initialCoins = gm.GetCoinsBalance();
        
        gm.AddCoins(100);
        
        Assert.AreEqual(initialCoins + 100, gm.GetCoinsBalance());
    }
}
```

### 2. اختبار الأداء
- استخدم Unity Profiler
- تحقق من استخدام الذاكرة
- تحقق من FPS (هدف 60 FPS)
- اختبر على أجهزة ضعيفة

### 3. اختبار التوافق
- أجهزة بأحجام مختلفة
- إصدارات Android مختلفة
- اختبر مع/بدون إعلانات

### 4. اختبار الإعلانات
- استخدم معرفات الاختبار أولاً
- تحقق من كل نوع إعلان
- قس معدل الملء (Fill Rate)

### 5. اختبار Firebase
- حفظ البيانات
- تحميل البيانات
- المزامنة
- معالجة الأخطاء

## أوامر البناء

### Android APK:
```bash
unity -batchmode -nographics \
  -projectPath . \
  -buildApk \
  -executeMethod BuildScript.BuildAPK \
  -quit
```

### Android AAB:
```bash
unity -batchmode -nographics \
  -projectPath . \
  -buildAAB \
  -executeMethod BuildScript.BuildAAB \
  -quit
```

## السجلات والأخطاء

### عرض السجلات:
```bash
# Android
adb logcat | grep Unity

# iOS (على Mac)
xcode console output
```

### مشاكل شائعة:

1. **أخطاء Firebase**
   - تحقق من `google-services.json`
   - تأكد من الاتصال بالإنترنت
   - تحقق من قواعد الأمان

2. **أخطاء الإعلانات**
   - استخدم معرفات الاختبار
   - تحقق من AdMob Account
   - انتظر 24 ساعة للحساب الجديد

3. **أخطاء الأداء**
   - قلل جودة الرسوميات
   - استخدم Object Pooling
   - أفرغ الذاكرة المؤقتة

## قائمة التحقق من الجودة

- [ ] لا توجد تسريبات ذاكرة
- [ ] FPS ثابت 60
- [ ] لا توجد أخطاء في Console
- [ ] الأصوات تعمل بشكل صحيح
- [ ] الحفظ والتحميل يعمل
- [ ] الإعلانات تظهر بشكل صحيح
- [ ] واجهة سهلة الاستخدام
- [ ] سرعة التحميل معقولة
- [ ] اختبار على 5+ أجهزة
