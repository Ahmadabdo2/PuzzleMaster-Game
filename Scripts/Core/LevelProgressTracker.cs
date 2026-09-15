using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class LevelProgressTracker : MonoBehaviour
{
    public static LevelProgressTracker Instance { get; private set; }

    [SerializeField] private int totalLevels = 1000;
    private Dictionary<int, LevelProgress> levelProgress = new Dictionary<int, LevelProgress>();
    private SaveSystem saveSystem;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        saveSystem = SaveSystem.Instance;
        LoadAllProgress();
    }

    /// <summary>
    /// تحميل تقدم جميع المستويات
    /// </summary>
    private void LoadAllProgress()
    {
        levelProgress.Clear();

        for (int i = 1; i <= totalLevels; i++)
        {
            LevelResult result = saveSystem.LoadLevelResult(i);
            
            if (result != null)
            {
                levelProgress[i] = new LevelProgress
                {
                    levelNumber = i,
                    isCompleted = true,
                    starsEarned = result.starsEarned,
                    coinsEarned = result.coinsEarned,
                    bestTime = result.timeSpent,
                    bestMoves = result.movesUsed,
                    completedDate = result.completedDate
                };
            }
            else
            {
                levelProgress[i] = new LevelProgress
                {
                    levelNumber = i,
                    isCompleted = false,
                    starsEarned = 0,
                    coinsEarned = 0,
                    bestTime = 0,
                    bestMoves = 0
                };
            }
        }

        Debug.Log($"✓ تم تحميل تقدم {GetCompletedLevelsCount()} مستوى من أصل {totalLevels}");
    }

    /// <summary>
    /// تحديث تقدم المستوى
    /// </summary>
    public void UpdateLevelProgress(int levelNumber, int starsEarned, int coinsEarned, int movesUsed, long timeSpent)
    {
        if (!levelProgress.ContainsKey(levelNumber))
            return;

        var progress = levelProgress[levelNumber];
        bool isNewCompletion = !progress.isCompleted;

        progress.isCompleted = true;
        progress.starsEarned = Mathf.Max(progress.starsEarned, starsEarned);
        progress.coinsEarned += coinsEarned;
        progress.bestMoves = (progress.bestMoves == 0) ? movesUsed : Mathf.Min(progress.bestMoves, movesUsed);
        progress.bestTime = (progress.bestTime == 0) ? timeSpent : Mathf.Min(progress.bestTime, timeSpent);
        progress.completedDate = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        progress.attemptCount++;

        // حفظ النتيجة
        saveSystem.SaveLevelResult(levelNumber, progress.starsEarned, coinsEarned, movesUsed, timeSpent);

        if (isNewCompletion)
        {
            Debug.Log($"🎉 تم إنجاز المستوى {levelNumber}! ⭐ {starsEarned}");
        }
    }

    /// <summary>
    /// الحصول على تقدم مستوى محدد
    /// </summary>
    public LevelProgress GetLevelProgress(int levelNumber)
    {
        return levelProgress.ContainsKey(levelNumber) ? levelProgress[levelNumber] : null;
    }

    /// <summary>
    /// التحقق من اكتمال مستوى
    /// </summary>
    public bool IsLevelCompleted(int levelNumber)
    {
        return levelProgress.ContainsKey(levelNumber) && levelProgress[levelNumber].isCompleted;
    }

    /// <summary>
    /// الحصول على عدد المستويات المكتملة
    /// </summary>
    public int GetCompletedLevelsCount()
    {
        return levelProgress.Values.Count(p => p.isCompleted);
    }

    /// <summary>
    /// الحصول على إجمالي النجوم المحصول عليها
    /// </summary>
    public int GetTotalStars()
    {
        return levelProgress.Values.Sum(p => p.starsEarned);
    }

    /// <summary>
    /// الحصول على إجمالي العملات
    /// </summary>
    public long GetTotalCoinsEarned()
    {
        return levelProgress.Values.Sum(p => p.coinsEarned);
    }

    /// <summary>
    /// الحصول على أعلى مستوى وصل إليه
    /// </summary>
    public int GetHighestLevel()
    {
        for (int i = totalLevels; i >= 1; i--)
        {
            if (IsLevelCompleted(i))
                return i;
        }
        return 1;
    }

    /// <summary>
    /// الحصول على نسبة الإنجاز
    /// </summary>
    public float GetCompletionPercentage()
    {
        int completed = GetCompletedLevelsCount();
        return (float)completed / totalLevels * 100f;
    }

    /// <summary>
    /// الحصول على المستويات المكتملة بـ 3 نجوم
    /// </summary>
    public int GetPerfectLevelsCount()
    {
        return levelProgress.Values.Count(p => p.starsEarned == 3);
    }

    /// <summary>
    /// إعادة تعيين جميع البيانات
    /// </summary>
    public void ResetAllProgress()
    {
        levelProgress.Clear();
        saveSystem.DeleteAllData();
        LoadAllProgress();
        Debug.Log("🔄 تم إعادة تعيين التقدم");
    }

    /// <summary>
    /// الحصول على قائمة المستويات غير المكتملة
    /// </summary>
    public List<int> GetIncompleteLevels(int limit = 10)
    {
        return levelProgress.Values
            .Where(p => !p.isCompleted)
            .Take(limit)
            .Select(p => p.levelNumber)
            .ToList();
    }

    /// <summary>
    /// الحصول على إحصائيات شاملة
    /// </summary>
    public LevelProgressStats GetProgressStats()
    {
        return new LevelProgressStats
        {
            totalLevels = totalLevels,
            completedLevels = GetCompletedLevelsCount(),
            perfectLevels = GetPerfectLevelsCount(),
            totalStars = GetTotalStars(),
            totalCoinsEarned = GetTotalCoinsEarned(),
            completionPercentage = GetCompletionPercentage(),
            highestLevel = GetHighestLevel()
        };
    }
}

/// <summary>
/// نموذج تقدم المستوى
/// </summary>
[System.Serializable]
public class LevelProgress
{
    public int levelNumber;
    public bool isCompleted;
    public int starsEarned;
    public long coinsEarned;
    public int bestMoves;
    public long bestTime;
    public string completedDate;
    public int attemptCount = 0;
}

/// <summary>
/// إحصائيات التقدم الشاملة
/// </summary>
[System.Serializable]
public class LevelProgressStats
{
    public int totalLevels;
    public int completedLevels;
    public int perfectLevels;
    public int totalStars;
    public long totalCoinsEarned;
    public float completionPercentage;
    public int highestLevel;
}
