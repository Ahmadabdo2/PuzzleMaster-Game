using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    [SerializeField] private TextAsset levelDataJson;
    private Dictionary<int, LevelData> levelDatabase = new Dictionary<int, LevelData>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        LoadLevelData();
    }

    private void LoadLevelData()
    {
        if (levelDataJson != null)
        {
            var levelList = JsonConvert.DeserializeObject<List<LevelData>>(levelDataJson.text);
            foreach (var level in levelList)
            {
                levelDatabase[level.levelNumber] = level;
            }
        }
        else
        {
            GenerateDefaultLevels();
        }

        Debug.Log($"Loaded {levelDatabase.Count} levels");
    }

    private void GenerateDefaultLevels()
    {
        for (int i = 1; i <= 1000; i++)
        {
            var level = new LevelData
            {
                levelNumber = i,
                targetScore = 100 * i,
                moveLimit = 20 + (i / 10),
                difficulty = GetDifficulty(i),
                tileTypes = GenerateTileTypes(i),
                rewards = new LevelRewards
                {
                    coinsForStar1 = 100 * i,
                    coinsForStar2 = 200 * i,
                    coinsForStar3 = 300 * i,
                    gemsForStar3 = (i % 5 == 0) ? 1 : 0
                }
            };

            levelDatabase[i] = level;
        }
    }

    private string GetDifficulty(int levelNumber)
    {
        if (levelNumber <= 100) return "Easy";
        if (levelNumber <= 300) return "Medium";
        if (levelNumber <= 600) return "Hard";
        return "Expert";
    }

    private List<string> GenerateTileTypes(int levelNumber)
    {
        List<string> types = new List<string> { "Red", "Blue", "Green", "Yellow" };
        if (levelNumber > 50) types.Add("Purple");
        if (levelNumber > 150) types.Add("Orange");
        if (levelNumber > 300) types.Add("Pink");
        return types;
    }

    public LevelData GetLevel(int levelNumber)
    {
        if (levelDatabase.ContainsKey(levelNumber))
            return levelDatabase[levelNumber];

        Debug.LogWarning($"Level {levelNumber} not found!");
        return null;
    }

    public int GetTotalLevels() => levelDatabase.Count;
}

[System.Serializable]
public class LevelData
{
    public int levelNumber;
    public int targetScore;
    public int moveLimit;
    public string difficulty;
    public List<string> tileTypes;
    public LevelRewards rewards;
}

[System.Serializable]
public class LevelRewards
{
    public int coinsForStar1;
    public int coinsForStar2;
    public int coinsForStar3;
    public int gemsForStar3;
}
