# نصدر لغة هولندية

## الطريقة السريعة لبناء APK (بدون التحميل المباشر)

## ✅ الحل السريع

لسوء الحظ, **لا يمكنني بناء APK مباشرة من GitHub**, لكن يمكنك بنانوها بطريقتين سهلة:

---

## الطريقة 1️⃣: بواسطة Unity Cloud Build (الأسهل)

### المتطلبات:
- حساب Unity (Pro أو Enterprise)
- زرب Google Play Developer

### الخطوات:

#### 1. في Unity Editor
```
File > Build Settings > Android
اختر Switch Platform إذا لم تكن Android مختارة
```

#### 2. إنشاء Project Settings
```
Edit > Project Settings > Player

Android:
- Company Name: PuzzleMaster
- Product Name: PuzzleMaster
- Bundle Identifier: com.puzzlemaster.game
- Minimum API Level: 24
- Target API Level: 33
- Icon: أضف 512x512 صورة
```

#### 3. ربط Unity Cloud Build
```
Window > Unity Cloud Build
- سجل دخولك
- اربط GitHub repo
- اختر Android platform
- اضغط Build
```

#### 4. الربط مع التوقيع
```
Player Settings > Publishing Settings:
- اعمل Keystore جديد (Generate...)
- اختر Alias (مثل: release)
- اختر Password قوي
```

---

## الطريقة 2️⃣: بناء محلي (ما تحتاج Android Studio)

### المتطلبات:
- Unity Editor (2022.3 LTS أو أحدث)
- Android SDK
- JDK 11+

### الخطوات المفصلة:

#### Step 1: تحضير Unity
```bash
# في Unity Editor:
1. File > Build Settings
2. اختر Android Platform
3. Switch Platform
```

#### Step 2: إعدادات البناء
```
Edit > Preferences > External Tools

- Android SDK Path: (حدد مسار SDK)
- JDK Path: (حدد مسار JDK)
- Android NDK: (اختياري)
```

#### Step 3: إعدادات Player
```
Edit > Project Settings > Player

Android Tab:
- Identification:
  * Package Name: com.puzzlemaster.game
  * Version: 1.0
  * Build: 1
  
- Minimum API Level: 24 (Android 7.0)
- Target API Level: 33 (Android 13)

- Graphics:
  * Graphics APIs: OpenGL ES 3.0+
  * Multithreaded Rendering: تفعيل

- Publishing Settings:
  * Build Type: Release
  * Create New Keystore
    - Alias: release
    - Password: اختر كلمة مرور قوية جداً
```

#### Step 4: بناء APK
```
File > Build Settings

1. التأكد من:
   ☐ Android Platform محددة
   ☐ جميع المشاهد مضافة
   ☐ Player Settings محدثة

2. اختر Build Type:
   - APK (لتوزيع مباشر)
   - AAB (لـ Google Play Store - الأفضل)

3. اضغط Build or Build and Run

4. اختر مجلد للحفظ
   (سيستغرق 5-10 دقائق)
```

#### Step 5: التحقق من الملف
```
ستجد:
- PuzzleMaster.apk (حوالي 100-200 MB)
- أو PuzzleMaster.aab (حوالي 80-150 MB)
```

---

## 🎯 الطريقة الموصى بها (Google Play Store)

### استخدم AAB بدلاً من APK:

```
File > Build Settings > Android

بدلاً من "Build" اضغط على مثلث الخيارات
↓
اختر "Build App Bundle (Google Play)"
```

**لماذا AAB أفضل:**
- ✅ حجم أصغر للمستخدمين
- ✅ تحسين تلقائي حسب الجهاز
- ✅ مطلوب من Google Play

---

## ⚠️ مشاكل شائعة وحلولها

### المشكلة 1: "SDK or NDK not found"
```
الحل:
1. Edit > Preferences > External Tools
2. حمل Android SDK من: https://developer.android.com/studio/
3. حدد المسار في Preferences
```

### المشكلة 2: "Gradle build failed"
```
الحل:
1. تحديث Google Play Services
2. حذف مجلد Library من المشروع
3. إعادة فتح Unity
```

### المشكلة 3: "Signing config not found"
```
الحل:
1. Player Settings > Publishing Settings
2. اضغط "Create New Keystore"
3. ملء جميع البيانات المطلوبة
```

### المشكلة 4: "Plugin not found"
```
الحل:
1. تأكد من وجود google-services.json
2. ضعه في: Assets/Plugins/Android/
3. أعد بناء المشروع
```

---

## 📊 معلومات الملف النهائي

### حجم الملف:
- **APK**: 100-200 MB (حسب الرسوميات)
- **AAB**: 80-150 MB (الموصى به)

### متطلبات التشغيل:
- Android 7.0+ (API 24)
- 500 MB مساحة خالية
- RAM 2GB على الأقل

### وقت البناء:
- **المرة الأولى**: 10-15 دقيقة
- **المرات التالية**: 5-10 دقائق

---

## ✅ قائمة التحقق قبل الإطلاق

- [ ] جميع المشاهد موجودة في Build Settings
- [ ] AdMob معرفات موجودة (أو معرفات اختبار)
- [ ] Keystore محفوظ في مكان آمن
- [ ] إصدار اللعبة محدث
- [ ] Bundle ID صحيح
- [ ] إيقونة التطبيق 512x512
- [ ] لا توجد أخطاء في Console
- [ ] اختبر على جهاز حقيقي
- [ ] فحص الأداء (FPS ثابت 60)
- [ ] التوقيع صحيح

---

## 🚀 بعد بناء APK

### لتوزيع مباشر:
```
1. شارك الملف .apk
2. المستخدمون يمكنهم تثبيته مباشرة
```

### لنشر على Google Play:
```
1. رفع ملف .aab إلى Google Play Console
2. انتظر الموافقة (عادة 24-48 ساعة)
3. انشر للمستخدمين
```

---

## 📱 اختبار الملف

```bash
# على جهاز متصل:
adb install -r PuzzleMaster.apk

# أو اختبر مباشرة من Unity:
File > Build and Run
```

---

## 💡 نصائح مهمة

✅ احتفظ بنسخة احتياطية من Keystore
✅ استخدم Build Profiles مختلفة للاختبار والإطلاق
✅ اختبر على أجهزة حقيقية قبل النشر
✅ راقب حجم APK باستخدام Asset Store Analyzer
✅ فعّل Optimization في Graphics Settings

---

**هل تحتاج إلى مساعدة في خطوة محددة؟**

📌 جميع التفاصيل في الملفات التالية:
- `Documentation/BUILD_AND_TESTING.md`
- `Documentation/PUBLISHING_GUIDE.md`
