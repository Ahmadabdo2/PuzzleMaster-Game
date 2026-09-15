using UnityEngine;
using System.Collections.Generic;
using Firebase.Database;
using System.Linq;

public class LeaderboardManager : MonoBehaviour
{
    public static LeaderboardManager Instance { get; private set; }

    [System.Serializable]
    public class LeaderboardEntry
    {
        public string userId;
        public string playerName;
        public int totalStars;
        public int currentLevel;
        public long totalScore;
        public string lastUpdate;
    }

    private List<LeaderboardEntry> globalLeaderboard = new List<LeaderboardEntry>();
    private List<LeaderboardEntry> friendsLeaderboard = new List<LeaderboardEntry>();
    private FirebaseDatabase firebaseDB;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        firebaseDB = FirebaseDatabase.DefaultInstance;
    }

    public void UpdatePlayerScore(int level, int stars, long score)
    {
        string userId = PlayerPrefs.GetString("UserID");
        string playerName = PlayerPrefs.GetString("PlayerName", "Player");

        var entry = new LeaderboardEntry
        {
            userId = userId,
            playerName = playerName,
            totalStars = stars,
            currentLevel = level,
            totalScore = score,
            lastUpdate = System.DateTime.Now.ToString()
        };

        string json = JsonUtility.ToJson(entry);
        firebaseDB.GetReference($"leaderboard/global/{userId}").SetRawJsonValueAsync(json);
    }

    public void LoadGlobalLeaderboard()
    {
        firebaseDB.GetReference("leaderboard/global").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                globalLeaderboard.Clear();

                foreach (var child in snapshot.Children)
                {
                    var entry = JsonUtility.FromJson<LeaderboardEntry>(child.GetRawJsonValue());
                    globalLeaderboard.Add(entry);
                }

                // Sort by stars descending
                globalLeaderboard = globalLeaderboard.OrderByDescending(x => x.totalStars).ToList();
            }
        });
    }

    public void LoadFriendsLeaderboard(List<string> friendIds)
    {
        friendsLeaderboard.Clear();
        foreach (var friendId in friendIds)
        {
            firebaseDB.GetReference($"leaderboard/global/{friendId}").GetValueAsync().ContinueWith(task =>
            {
                if (task.IsCompleted && task.Result.Value != null)
                {
                    var entry = JsonUtility.FromJson<LeaderboardEntry>(task.Result.GetRawJsonValue());
                    friendsLeaderboard.Add(entry);
                    friendsLeaderboard = friendsLeaderboard.OrderByDescending(x => x.totalStars).ToList();
                }
            });
        }
    }

    public List<LeaderboardEntry> GetGlobalLeaderboard() => globalLeaderboard.Take(100).ToList();
    public List<LeaderboardEntry> GetFriendsLeaderboard() => friendsLeaderboard;
}
