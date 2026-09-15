using UnityEngine;
using System.Collections.Generic;
using Firebase.Database;
using Firebase.Analytics;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private int currentLevel = 1;
    [SerializeField] private int totalStars = 0;
    [SerializeField] private long coinsBalance = 0;
    [SerializeField] private long gemsBalance = 100;

    private FirebaseDatabase firebaseDB;
    private UserProfile userProfile;
    private LevelManager levelManager;
    private AdsManager adsManager;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        InitializeFirebase();
        LoadUserData();
    }

    private void InitializeFirebase()
    {
        firebaseDB = FirebaseDatabase.DefaultInstance;
        firebaseDB.GetReference("version").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("Firebase initialized successfully");
            }
        });
    }

    private void LoadUserData()
    {
        string userKey = PlayerPrefs.GetString("UserID", System.Guid.NewGuid().ToString());
        PlayerPrefs.SetString("UserID", userKey);

        userProfile = new UserProfile
        {
            userId = userKey,
            currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1),
            totalStars = PlayerPrefs.GetInt("TotalStars", 0),
            coinsBalance = PlayerPrefs.GetInt("CoinsBalance", 0),
            gemsBalance = PlayerPrefs.GetInt("GemsBalance", 100),
            lastPlayDate = PlayerPrefs.GetString("LastPlayDate", System.DateTime.Now.ToString())
        };

        SaveUserDataToCloud();
    }

    public void CompleteLevel(int levelNumber, int starsEarned, int coinsEarned)
    {
        if (levelNumber == currentLevel)
        {
            currentLevel++;
            totalStars += starsEarned;
            coinsBalance += coinsEarned;

            // Log analytics
            FirebaseAnalytics.LogEvent("level_completed", new Parameter("level", levelNumber), new Parameter("stars", starsEarned));

            SaveUserData();
            SaveUserDataToCloud();
        }
    }

    public void AddCoins(long amount)
    {
        coinsBalance += amount;
        SaveUserData();
    }

    public void RemoveCoins(long amount)
    {
        if (coinsBalance >= amount)
        {
            coinsBalance -= amount;
            SaveUserData();
        }
    }

    public void AddGems(long amount)
    {
        gemsBalance += amount;
        SaveUserData();
    }

    public void RemoveGems(long amount)
    {
        if (gemsBalance >= amount)
        {
            gemsBalance -= amount;
            SaveUserData();
        }
    }

    private void SaveUserData()
    {
        PlayerPrefs.SetInt("CurrentLevel", currentLevel);
        PlayerPrefs.SetInt("TotalStars", totalStars);
        PlayerPrefs.SetInt("CoinsBalance", (int)coinsBalance);
        PlayerPrefs.SetInt("GemsBalance", (int)gemsBalance);
        PlayerPrefs.Save();
    }

    private void SaveUserDataToCloud()
    {
        if (firebaseDB != null)
        {
            string json = JsonUtility.ToJson(userProfile);
            firebaseDB.GetReference("users/" + userProfile.userId).SetRawJsonValueAsync(json);
        }
    }

    public int GetCurrentLevel() => currentLevel;
    public int GetTotalStars() => totalStars;
    public long GetCoinsBalance() => coinsBalance;
    public long GetGemsBalance() => gemsBalance;
}

[System.Serializable]
public class UserProfile
{
    public string userId;
    public int currentLevel;
    public int totalStars;
    public long coinsBalance;
    public long gemsBalance;
    public string lastPlayDate;
}
