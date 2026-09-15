using UnityEngine;
using System.Collections.Generic;

public class AchievementSystem : MonoBehaviour
{
    public static AchievementSystem Instance { get; private set; }

    [System.Serializable]
    public class Achievement
    {
        public string achievementId;
        public string title;
        public string description;
        public Sprite icon;
        public int rewardGems;
        public bool unlocked;
        public string unlockedDate;
    }

    private Dictionary<string, Achievement> achievements = new Dictionary<string, Achievement>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        InitializeAchievements();
    }

    private void InitializeAchievements()
    {
        achievements.Add("first_level", new Achievement { achievementId = "first_level", title = "First Steps", description = "Complete your first level", rewardGems = 5, unlocked = false });
        achievements.Add("level_50", new Achievement { achievementId = "level_50", title = "Halfway There", description = "Reach level 50", rewardGems = 25, unlocked = false });
        achievements.Add("level_100", new Achievement { achievementId = "level_100", title = "Century", description = "Reach level 100", rewardGems = 50, unlocked = false });
        achievements.Add("level_500", new Achievement { achievementId = "level_500", title = "Legend", description = "Reach level 500", rewardGems = 100, unlocked = false });
        achievements.Add("level_1000", new Achievement { achievementId = "level_1000", title = "Master", description = "Complete all 1000 levels", rewardGems = 250, unlocked = false });
        achievements.Add("perfect_score", new Achievement { achievementId = "perfect_score", title = "Perfect", description = "Earn 3 stars on 10 levels", rewardGems = 30, unlocked = false });
        achievements.Add("combo_master", new Achievement { achievementId = "combo_master", title = "Combo Master", description = "Create 100 combos", rewardGems = 40, unlocked = false });
        achievements.Add("spender", new Achievement { achievementId = "spender", title = "Big Spender", description = "Spend 100,000 coins", rewardGems = 35, unlocked = false });
        achievements.Add("social_butterfly", new Achievement { achievementId = "social_butterfly", title = "Social Butterfly", description = "Add 10 friends", rewardGems = 20, unlocked = false });
        achievements.Add("daily_warrior", new Achievement { achievementId = "daily_warrior", title = "Daily Warrior", description = "Play 30 days in a row", rewardGems = 60, unlocked = false });
    }

    public void UnlockAchievement(string achievementId)
    {
        if (achievements.ContainsKey(achievementId) && !achievements[achievementId].unlocked)
        {
            achievements[achievementId].unlocked = true;
            achievements[achievementId].unlockedDate = System.DateTime.Now.ToString();
            
            GameManager.Instance.AddGems(achievements[achievementId].rewardGems);
            Debug.Log($"Achievement unlocked: {achievements[achievementId].title}! +{achievements[achievementId].rewardGems} gems");
        }
    }

    public Achievement GetAchievement(string achievementId) => achievements.ContainsKey(achievementId) ? achievements[achievementId] : null;
    public Dictionary<string, Achievement> GetAllAchievements() => achievements;
    public int GetUnlockedCount() => System.Linq.Enumerable.Count(achievements.Values, x => x.unlocked);
}
