using UnityEngine;
using System.Collections.Generic;
using Firebase.Database;

public class ChallengesManager : MonoBehaviour
{
    public static ChallengesManager Instance { get; private set; }

    [System.Serializable]
    public class Challenge
    {
        public string challengeId;
        public string title;
        public string description;
        public string challengeType; // "score", "moves", "time", "streak"
        public int targetValue;
        public int rewardCoins;
        public int rewardGems;
        public string difficulty; // "easy", "medium", "hard"
        public bool completed;
    }

    private List<Challenge> activeChallenges = new List<Challenge>();
    private List<Challenge> completedChallenges = new List<Challenge>();
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
        GenerateChallenges();
    }

    private void GenerateChallenges()
    {
        activeChallenges.Clear();

        // Daily Challenges
        activeChallenges.Add(new Challenge
        {
            challengeId = "daily_score_1",
            title = "Score Master",
            description = "Earn 10,000 points",
            challengeType = "score",
            targetValue = 10000,
            rewardCoins = 500,
            rewardGems = 10,
            difficulty = "medium",
            completed = false
        });

        activeChallenges.Add(new Challenge
        {
            challengeId = "daily_streak_1",
            title = "Win Streak",
            description = "Complete 5 levels in a row",
            challengeType = "streak",
            targetValue = 5,
            rewardCoins = 750,
            rewardGems = 15,
            difficulty = "hard",
            completed = false
        });

        activeChallenges.Add(new Challenge
        {
            challengeId = "daily_efficient_1",
            title = "Efficient Player",
            description = "Complete level with 5+ moves remaining",
            challengeType = "moves",
            targetValue = 5,
            rewardCoins = 300,
            rewardGems = 5,
            difficulty = "easy",
            completed = false
        });

        // Weekly Challenges
        activeChallenges.Add(new Challenge
        {
            challengeId = "weekly_power_1",
            title = "Power User",
            description = "Use 20 boosters",
            challengeType = "booster",
            targetValue = 20,
            rewardCoins = 2000,
            rewardGems = 50,
            difficulty = "hard",
            completed = false
        });
    }

    public void CompleteChallenge(string challengeId)
    {
        var challenge = activeChallenges.Find(x => x.challengeId == challengeId);
        if (challenge != null)
        {
            challenge.completed = true;
            activeChallenges.Remove(challenge);
            completedChallenges.Add(challenge);

            GameManager gm = GameManager.Instance;
            gm.AddCoins(challenge.rewardCoins);
            gm.AddGems(challenge.rewardGems);

            Debug.Log($"Challenge completed: {challenge.title}");
        }
    }

    public List<Challenge> GetActiveChallenges() => activeChallenges;
    public List<Challenge> GetCompletedChallenges() => completedChallenges;
}
