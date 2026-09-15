# دليل النشر على متاجر التطبيقات

## النشر على Google Play Store

### الخطوة 1: تحضير المشروع

1. تحديث رقم الإصدار:
   - File > Build Settings > Android > Player Settings
   - Bundle Version Code: 1
   - Bundle Version Name: 1.0.0

2. التوقيع (Signing):
   - Player Settings > Publishing Settings
   - Create new Keystore
   - Alias: release
   - Password: (اختر كلمة مرور قوية)

3. الإعدادات النهائية:
   - Graphics Settings: مخصص
   - Player Settings:
     - API Level: 24 (minimum)
     - Target: 33
     - Screen Orientation: Portrait
     - Icon: أضف 512x512 صورة
     - Splash Image: اختياري

### الخطوة 2: بناء APK/AAB

1. File > Build Settings
2. Select Platform: Android
3. Build Type: AAB (الأفضل للـ Google Play)
4. اضغط Build
5. حدد مجلد الإنتاج

### الخطوة 3: إنشاء حساب Google Play Developer

1. اذهب إلى: https://play.google.com/apps/publish
2. أنشئ حساب و ادفع رسم التسجيل ($25)
3. أكمل ملف التاجر الخاص بك

### الخطوة 4: إنشاء تطبيق جديد

1. في Google Play Console:
   - اضغط "Create app"
   - اسم التطبيق: PuzzleMaster
   - اللغة الافتراضية: Arabic
   - صنف التطبيق: Games
   - اختر "Free"

### الخطوة 5: ملء التفاصيل

1. **App details**:
   - Title: PuzzleMaster Game
   - Short description: لعبة بازل تفاعلية
   - Full description: (انظر أدناه)
   - Category: Puzzle
   - Content Rating: اكمل الاستبيان

2. **Screenshots** (5-8 لقطات):
   - حجم الشاشة: 540x720
   - اعرض أفضل أجزاء اللعبة

3. **Feature Graphic**:
   - الحجم: 1024x500
   - صورة ترويجية جذابة

4. **Icon**:
   - 512x512
   - PNG format
   - بدون حدود

### الخطوة 6: تحميل AAB

1. في Release management:
   - اضغط "Create new release"
   - اختر "Production"
   - حمل ملف AAB
   - أضف Release notes

### الخطوة 7: مراجعة ونشر

1. تحقق من:
   - Privacy Policy URL
   - Permissions (في AndroidManifest.xml)
   - Content Rating

2. اضغط "Review and publish"

3. انتظر الموافقة (عادة 24-48 ساعة)

---

## النشر على App Store (iOS)

### المتطلبات:
- جهاز Mac
- Xcode
- حساب Apple Developer ($99/سنة)
- Certificate + Provisioning Profile

### الخطوات:

1. **إعداد المشروع**:
   - File > Build Settings > iOS
   - Bundle Identifier: com.puzzlemaster.game
   - Version: 1.0
   - Build: 1

2. **البناء**:
   - Build for iOS (Xcode)
   - اختر جهاز حقيقي أو Simulator

3. **التوقيع**:
   - في Xcode:
     - Select Team
     - Configure Signing Certificate
     - Choose Provisioning Profile

4. **App Store Connect**:
   - إنشاء App ID جديد
   - إنشاء إصدار جديد
   - تحميل بيانات التطبيق
   - إضافة Screenshots و Icon

5. **الإرسال**:
   - في Xcode: Product > Archive
   - Validate
   - Distribute to App Store

---

## نسخة وصف التطبيق

### العربية:

**PuzzleMaster - لعبة البازل الممتعة**

استمتع بأكثر من 1000 مستوى تفاعلي مليء بالتحديات والمرح!

✨ **المميزات:**
• 1000+ مستوى متنوع الصعوبة
• نظام نجوم (1-3 نجوم)
• تحديات يومية وأسبوعية
• معينات قوية (مطرقة، قنبلة، برق)
• لوحة مراتب عالمية
• أصدقاء وتحديات اجتماعية
• نظام إنجازات شامل
• جوائز يومية سخية
• رسوميات جميلة وسلسة
• موسيقى وأصوات رائعة

🎮 **كيفية اللعب:**
1. اختر 3 بلاطات متشابهة أو أكثر
2. اجمع النقاط المطلوبة
3. استخدم المعينات بذكاء
4. احصل على 3 نجوم واستمتع!

💎 **تحديث مستمر:**
نضيف مستويات وميزات جديدة كل أسبوع!

---

## قائمة التحقق قبل النشر

- [ ] إصدار APK/AAB يعمل بدون أخطاء
- [ ] جميع الإعلانات تظهر بشكل صحيح
- [ ] Firebase تعمل بشكل صحيح
- [ ] لا توجد أخطاء في السجلات (Logs)
- [ ] الأداء جيد على أجهزة قديمة
- [ ] الاختبار على أجهزة حقيقية
- [ ] نسخة احتياطية من المشروع
- [ ] معرفات AdMob الصحيحة
- [ ] Privacy Policy جاهزة
- [ ] Screenshots و Icon بجودة عالية
- [ ] جميع الأذونات معرفة في Manifest
