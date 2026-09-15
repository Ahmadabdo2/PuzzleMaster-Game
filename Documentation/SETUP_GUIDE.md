# Setup Guide - دليل الإعداد

## المتطلبات
- Unity 2022.3 LTS أو أحدث
- Android SDK (للنشر على Android)
- Xcode (للنشر على iOS)
- Firebase Account
- Google AdMob Account

## خطوات الإعداد الأولية

### 1. استنساخ المشروع
```bash
git clone https://github.com/Ahmadabdo2/PuzzleMaster-Game.git
cd PuzzleMaster-Game
```

### 2. فتح المشروع في Unity
- افتح Unity Hub
- اختر "Open Project"
- اختر المجلد المستنسخ
- اختر إصدار Unity 2022.3 أو أحدث

### 3. تثبيت المكتبات المطلوبة

#### Firebase Setup
1. انتقل إلى Window > Firebase > Setup
2. اختر "Create a new Firebase Project" أو استخدم مشروع موجود
3. اتبع الخطوات لتنزيل وإضافة Firebase SDK
4. انقل ملف `google-services.json` إلى `Assets/Plugins/Android/`

#### Google Mobile Ads
1. انت��ل إلى Window > Google Mobile Ads > Settings
2. أدخل AdMob App ID
3. أضف معرفات الإعلانات (Ad Unit IDs)

#### TextMesh Pro
1. انتقل إلى Window > TextMeshPro > Import TMP Essential Resources
2. اختر Import

### 4. إعداد المشهد الأولي
1. افتح المجلد `Scenes`
2. افتح المشهد `MainMenu.unity`
3. في Hierarchy:
   - ابحث عن `GameManager` وأضفه إذا لم يكن موجوداً
   - ابحث عن `AdsManager` وأضفه
   - ابحث عن `AudioManager` وأضفه

### 5. إعدادات AdMob

```csharp
// في AdsManager.cs، استبدل معرفات الاختبار بمعرفات AdMob الفعلية:

// Android
private string bannerAdUnitId = "ca-app-pub-xxxxxxxxxxxxxxxx/yyyyyyyyyy";
private string interstitialAdUnitId = "ca-app-pub-xxxxxxxxxxxxxxxx/yyyyyyyyyy";
private string rewardedAdUnitId = "ca-app-pub-xxxxxxxxxxxxxxxx/yyyyyyyyyy";

// iOS
private string bannerAdUnitId_iOS = "ca-app-pub-xxxxxxxxxxxxxxxx/yyyyyyyyyy";
private string interstitialAdUnitId_iOS = "ca-app-pub-xxxxxxxxxxxxxxxx/yyyyyyyyyy";
private string rewardedAdUnitId_iOS = "ca-app-pub-xxxxxxxxxxxxxxxx/yyyyyyyyyy";
```

### 6. إعدادات البناء

#### للـ Android:
1. انتقل إلى File > Build Settings
2. اختر Android
3. اذهب إلى Player Settings:
   - Company Name: "YourCompany"
   - Product Name: "PuzzleMaster"
   - Bundle Identifier: "com.puzzlemaster.game"
   - Minimum API Level: 24
   - Target API Level: 33

#### للـ iOS:
1. انتقل إلى File > Build Settings
2. اختر iOS
3. اذهب إلى Player Settings:
   - Company Name: "YourCompany"
   - Product Name: "PuzzleMaster"
   - Bundle Identifier: "com.puzzlemaster.game"
   - iOS Version: 12.0 أو أحدث

### 7. اختبار اللعبة
1. افتح المشهد `MainMenu.unity`
2. اضغط على زر التشغيل (Play)
3. جرب جميع الميزات الأساسية

## استكشاف الأخطاء

### Firebase لا يتصل
- تحقق من ملف `google-services.json`
- تأكد من أن Firebase مفعل في Console
- تحقق من حالة الإنترنت

### الإعلانات لا تظهر
- استخدم معرفات الاختبار أولاً
- تأكد من تفعيل الإعلانات في AdMob
- انتظر 24 ساعة بعد إضافة الحساب الجديد

### أخطاء في الترجمة (Compilation)
- تأكد من وجود جميع المكتبات
- أعد استيراد المشروع
- اختر File > Refresh
