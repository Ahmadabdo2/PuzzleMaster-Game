using UnityEngine;
using System.Collections.Generic;
using Firebase.Database;
using System.Linq;

public class DailyRewardsManager : MonoBehaviour
{
    public static DailyRewardsManager Instance { get; private set; }

    [System.Serializable]
    public class DailyReward
    {
        public int day;
        public int coins;
        public int gems;
        public string rewardType; // "coins", "gems", "booster"
    }

    private List<DailyReward> rewardSequence = new List<DailyReward>();
    private int currentStreak = 0;
    private string lastRewardDate;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        GenerateRewardSequence();
        LoadRewardProgress();
    }

    private void GenerateRewardSequence()
    {
        rewardSequence.Clear();
        rewardSequence.Add(new DailyReward { day = 1, coins = 100, gems = 0, rewardType = "coins" });
        rewardSequence.Add(new DailyReward { day = 2, coins = 200, gems = 0, rewardType = "coins" });
        rewardSequence.Add(new DailyReward { day = 3, coins = 300, gems = 0, rewardType = "coins" });
        rewardSequence.Add(new DailyReward { day = 4, coins = 0, gems = 5, rewardType = "gems" });
        rewardSequence.Add(new DailyReward { day = 5, coins = 500, gems = 10, rewardType = "mixed" });
        rewardSequence.Add(new DailyReward { day = 6, coins = 600, gems = 0, rewardType = "coins" });
        rewardSequence.Add(new DailyReward { day = 7, coins = 1000, gems = 50, rewardType = "mega" }); // Bonus day!
    }

    private void LoadRewardProgress()
    {
        lastRewardDate = PlayerPrefs.GetString("LastRewardDate", "");
        currentStreak = PlayerPrefs.GetInt("DailyStreak", 0);
    }

    public DailyReward ClaimDailyReward()
    {
        string todayDate = System.DateTime.Now.Date.ToString("yyyy-MM-dd");

        if (lastRewardDate == todayDate)
        {
            Debug.Log("Already claimed reward today!");
            return null;
        }

        // Check if streak is broken
        if (!string.IsNullOrEmpty(lastRewardDate))
        {
            System.DateTime last = System.DateTime.Parse(lastRewardDate);
            int daysDifference = (int)(System.DateTime.Now.Date - last).TotalDays;

            if (daysDifference == 1)
            {
                currentStreak++;
            }
            else if (daysDifference > 1)
            {
                currentStreak = 1;
            }
        }
        else
        {
            currentStreak = 1;
        }

        // Get reward for current day in streak
        int rewardIndex = (currentStreak - 1) % rewardSequence.Count;
        DailyReward reward = rewardSequence[rewardIndex];

        // Apply reward
        GameManager gm = GameManager.Instance;
        if (reward.coins > 0) gm.AddCoins(reward.coins);
        if (reward.gems > 0) gm.AddGems(reward.gems);

        // Save progress
        PlayerPrefs.SetString("LastRewardDate", todayDate);
        PlayerPrefs.SetInt("DailyStreak", currentStreak);
        PlayerPrefs.Save();

        Debug.Log($"Claimed daily reward! Streak: {currentStreak}, Coins: {reward.coins}, Gems: {reward.gems}");
        return reward;
    }

    public int GetCurrentStreak() => currentStreak;
    public DailyReward GetNextReward() => rewardSequence[(currentStreak) % rewardSequence.Count];
    public bool CanClaimToday()
    {
        string todayDate = System.DateTime.Now.Date.ToString("yyyy-MM-dd");
        return lastRewardDate != todayDate;
    }
}
