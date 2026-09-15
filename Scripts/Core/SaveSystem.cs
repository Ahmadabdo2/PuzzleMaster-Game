using UnityEngine;
using System.Collections.Generic;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem Instance { get; private set; }

    private const string PLAYER_DATA_KEY = "PuzzleMaster_PlayerData";
    private const string LEVEL_DATA_KEY = "PuzzleMaster_LevelData";
    private const string USER_ID_KEY = "PuzzleMaster_UserID";

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

    /// <summary>
    /// حفظ بيانات اللاعب الأساسية
    /// </summary>
    public void SavePlayerData(int currentLevel, int totalStars, long coins, long gems, string lastPlayDate)
    {
        PlayerData data = new PlayerData
        {
            currentLevel = currentLevel,
            totalStars = totalStars,
            coinsBalance = coins,
            gemsBalance = gems,
            lastPlayDate = lastPlayDate,
            saveDate = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        };

        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(PLAYER_DATA_KEY, json);
        PlayerPrefs.Save();

        Debug.Log("✓ تم حفظ بيانات اللاعب");
    }

    /// <summary>
    /// تحميل بيانات اللاعب
    /// </summary>
    public PlayerData LoadPlayerData()
    {
        if (PlayerPrefs.HasKey(PLAYER_DATA_KEY))
        {
            string json = PlayerPrefs.GetString(PLAYER_DATA_KEY);
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);
            Debug.Log("✓ تم تحميل بيانات اللاعب");
            return data;
        }

        Debug.Log("! لا توجد بيانات محفوظة - إنشاء بيانات جديدة");
        return CreateNewPlayerData();
    }

    /// <summary>
    /// إنشاء بيانات لاعب جديدة
    /// </summary>
    private PlayerData CreateNewPlayerData()
    {
        return new PlayerData
        {
            currentLevel = 1,
            totalStars = 0,
            coinsBalance = 0,
            gemsBalance = 100,
            lastPlayDate = System.DateTime.Now.ToString(),
            saveDate = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        };
    }

    /// <summary>
    /// حفظ نتائج المستوى
    /// </summary>
    public void SaveLevelResult(int levelNumber, int starsEarned, int coinsEarned, int movesUsed, long timeSpent)
    {
        LevelResult result = new LevelResult
        {
            levelNumber = levelNumber,
            starsEarned = starsEarned,
            coinsEarned = coinsEarned,
            movesUsed = movesUsed,
            timeSpent = timeSpent,
            completedDate = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        };

        string key = LEVEL_DATA_KEY + "_" + levelNumber;
        string json = JsonUtility.ToJson(result);
        PlayerPrefs.SetString(key, json);
        PlayerPrefs.Save();

        Debug.Log($"✓ تم حفظ نتائج المستوى {levelNumber}");
    }

    /// <summary>
    /// تحميل نتائج المستوى
    /// </summary>
    public LevelResult LoadLevelResult(int levelNumber)
    {
        string key = LEVEL_DATA_KEY + "_" + levelNumber;

        if (PlayerPrefs.HasKey(key))
        {
            string json = PlayerPrefs.GetString(key);
            return JsonUtility.FromJson<LevelResult>(json);
        }

        return null;
    }

    /// <summary>
    /// حفظ معرّف اللاعب الفريد
    /// </summary>
    public void SaveUserID(string userID)
    {
        PlayerPrefs.SetString(USER_ID_KEY, userID);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// تحميل معرّف اللاعب الفريد
    /// </summary>
    public string LoadUserID()
    {
        if (!PlayerPrefs.HasKey(USER_ID_KEY))
        {
            string newID = System.Guid.NewGuid().ToString();
            SaveUserID(newID);
            return newID;
        }

        return PlayerPrefs.GetString(USER_ID_KEY);
    }

    /// <summary>
    /// حفظ إحصائيات اللاعب
    /// </summary>
    public void SavePlayerStats(PlayerStats stats)
    {
        string json = JsonUtility.ToJson(stats);
        PlayerPrefs.SetString("PuzzleMaster_Stats", json);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// تحميل إحصائيات اللاعب
    /// </summary>
    public PlayerStats LoadPlayerStats()
    {
        if (PlayerPrefs.HasKey("PuzzleMaster_Stats"))
        {
            string json = PlayerPrefs.GetString("PuzzleMaster_Stats");
            return JsonUtility.FromJson<PlayerStats>(json);
        }

        return new PlayerStats();
    }

    /// <summary>
    /// حفظ الإنجازات
    /// </summary>
    public void SaveAchievement(string achievementId, bool unlocked)
    {
        string key = "Achievement_" + achievementId;
        PlayerPrefs.SetInt(key, unlocked ? 1 : 0);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// التحقق من الإنجاز
    /// </summary>
    public bool IsAchievementUnlocked(string achievementId)
    {
        string key = "Achievement_" + achievementId;
        return PlayerPrefs.GetInt(key, 0) == 1;
    }

    /// <summary>
    /// حفظ الإعدادات
    /// </summary>
    public void SaveGameSettings(GameSettings settings)
    {
        string json = JsonUtility.ToJson(settings);
        PlayerPrefs.SetString("PuzzleMaster_Settings", json);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// تحميل الإعدادات
    /// </summary>
    public GameSettings LoadGameSettings()
    {
        if (PlayerPrefs.HasKey("PuzzleMaster_Settings"))
        {
            string json = PlayerPrefs.GetString("PuzzleMaster_Settings");
            return JsonUtility.FromJson<GameSettings>(json);
        }

        return new GameSettings { musicEnabled = true, soundEnabled = true, languageCode = "ar" };
    }

    /// <summary>
    /// حذف كل البيانات المحفوظة
    /// </summary>
    public void DeleteAllData()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("✗ تم حذف جميع البيانات المحفوظة");
    }

    /// <summary>
    /// الحصول على حجم البيانات المحفوظة (تقريبي)
    /// </summary>
    public string GetSaveDataSize()
    {
        long totalSize = 0;
        foreach (var key in System.Enum.GetNames(typeof(SaveDataType)))
        {
            if (PlayerPrefs.HasKey(key))
            {
                totalSize += PlayerPrefs.GetString(key).Length;
            }
        }
        return $"{totalSize / 1024f:F2} KB";
    }
}

/// <summary>
/// نموذج بيانات اللاعب
/// </summary>
[System.Serializable]
public class PlayerData
{
    public int currentLevel;
    public int totalStars;
    public long coinsBalance;
    public long gemsBalance;
    public string lastPlayDate;
    public string saveDate;
}

/// <summary>
/// نموذج نتائج المستوى
/// </summary>
[System.Serializable]
public class LevelResult
{
    public int levelNumber;
    public int starsEarned;
    public int coinsEarned;
    public int movesUsed;
    public long timeSpent;
    public string completedDate;
}

/// <summary>
/// نموذج إحصائيات اللاعب
/// </summary>
[System.Serializable]
public class PlayerStats
{
    public int totalLevelsCompleted = 0;
    public int totalCoinsEarned = 0;
    public int totalGemsEarned = 0;
    public long totalPlayTime = 0;
    public int highestStreak = 0;
    public int totalMatches = 0;
}

/// <summary>
/// نموذج إعدادات اللعبة
/// </summary>
[System.Serializable]
public class GameSettings
{
    public bool musicEnabled = true;
    public bool soundEnabled = true;
    public float musicVolume = 1f;
    public float soundVolume = 1f;
    public string languageCode = "ar";
    public bool notificationsEnabled = true;
}

public enum SaveDataType
{
    PlayerData,
    LevelData,
    UserID,
    Settings,
    Statistics
}
