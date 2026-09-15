# دليل الهندسة المعمارية - Architecture Guide

## هيكل المشروع

```
PuzzleMaster-Game/
├── Assets/
│   ├── Scripts/
│   │   ├── Core/              # نظام اللعب الأساسي
│   │   │   ├── GameManager.cs
│   │   │   ├── LevelManager.cs
│   │   │   ├── TileManager.cs
│   │   │   └── Tile.cs
│   │   ├── Monetization/      # أنظمة الربح
│   │   │   ├── AdsManager.cs
│   │   │   └── ShopManager.cs
│   │   ├── UI/                # واجهات المستخدم
│   │   │   ├── MainMenuController.cs
│   │   │   └── GameHUD.cs
│   │   ├── Analytics/         # تحليل البيانات
│   │   │   └── AnalyticsManager.cs
│   │   ├── Social/            # الأنظمة الاجتماعية
│   │   │   ├── LeaderboardManager.cs
│   │   │   └── SocialManager.cs
│   │   ├── Rewards/           # نظام الجوائز
│   │   │   └── DailyRewardsManager.cs
│   │   ├── Challenges/        # نظام التحديات
│   │   │   └── ChallengesManager.cs
│   │   ├── Boosters/          # نظام المعينات
│   │   │   └── BoosterSystem.cs
│   │   ├── Premium/           # النسخة المدفوعة
│   │   │   └── PremiumManager.cs
│   │   ├── Achievements/      # نظام الإنجازات
│   │   │   └── AchievementSystem.cs
│   │   ├── Audio/             # إدارة الصوت
│   │   │   └── AudioManager.cs
│   │   └── Utils/             # أدوات مساعدة
│   │       └── LevelDataGenerator.cs
│   ├── Scenes/                # المشاهد
│   ├── Sprites/               # الرسوميات
│   ├── Audio/                 # ملفات الصوت
│   └── Plugins/               # المكتبات الخارجية
├── Docs/                      # التوثيق
└── README.md
```

## نمط Singleton

المديرين الرئيسيين يستخدمون نمط Singleton:

```csharp
public static GameManager Instance { get; private set; }

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
```

## تدفق اللعبة الأساسي

### عند بدء اللعبة:
1. **GameManager** يتم تحميله (Singleton)
2. يحمل بيانات اللاعب من PlayerPrefs
3. يتصل بـ Firebase لمزامنة البيانات
4. **AdsManager** يحمل الإعلانات
5. **AudioManager** يبدأ تشغيل الموسيقى

### عند بدء مستوى:
1. **LevelManager** يحمل بيانات المستوى
2. **TileManager** ينشئ الشبكة
3. **GameHUD** يعرض المعلومات
4. اللاعب يلعب ويجمع النقاط

### عند إنهاء المستوى:
1. حساب النجوم (1-3)
2. حساب المكافآت (عملات، جواهر)
3. تحديث **GameManager**
4. حفظ في PlayerPrefs و Firebase
5. عرض شاشة النجاح

## نظام المكافآت

### المصادر الرئيسية للمكافآت:

1. **إكمال المستويات**
   - عملات: 100-300 حسب النجوم
   - جواهر: 1 جوهر في كل 5 مستويات (للنجم الثالث)

2. **المكافآت اليومية**
   - سلسلة من 7 أيام
   - اليوم 7: 1000 عملة + 50 جوهر (مكافأة إضافية)

3. **الإعلانات**
   - Rewarded Ads: 50-200 عملة
   - Interstitial Ads: لا مكافآت (زيادة الربح)

4. **المشتريات**
   - 50 جوهر = $0.99
   - 250 جوهر = $4.99
   - 1500 جوهر = $19.99

## نموذج الربح

```
إيرادات شهرية متوقعة:
│
├─ الإعلانات: 70% من الإيرادات
│  ├─ Banner Ads: $0.5-1.5 لكل 1000 مشاهدة
│  ├─ Interstitial: $1-3 لكل 1000 مشاهدة
│  └─ Rewarded: $2-5 لكل 1000 مشاهدة
│
├─ المشتريات: 25% من الإيرادات
│  ├─ حزم الجواهر
│  ├─ حزم العملات
│  └─ المعينات
│
└─ Premium Pass: 5% من الإيرادات
   └─ 30 يوم = $2.99
```

## الاتصال بـ Firebase

### القراءة:
```csharp
firebaseDB.GetReference("path").GetValueAsync().ContinueWith(task =>
{
    if (task.IsCompleted)
    {
        DataSnapshot snapshot = task.Result;
        // معالجة البيانات
    }
});
```

### الكتابة:
```csharp
string json = JsonUtility.ToJson(data);
firebaseDB.GetReference("path").SetRawJsonValueAsync(json);
```

## نظام التحليلات

جميع الأحداث المهمة يتم تسجيلها:
- بدء المستوى
- إنهاء المستوى (مع النجوم والنقاط)
- الشراء
- مشاهدة الإعلانات
- الإنجازات
- الأداء اليومي

## أفضل الممارسات

1. **التخزين المؤقت**: استخدم PlayerPrefs للبيانات المحلية
2. **المزامنة**: حفظ على Firebase كل 10 ثوان
3. **الأداء**: استخدم Object Pooling للبلاطات
4. **الذاكرة**: أفرغ الموارد غير المستخدمة
5. **الشبكة**: تعامل مع فقدان الاتصال بشكل صحيح
