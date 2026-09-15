using UnityEngine;
using System.Collections.Generic;

public class LevelDataGenerator
{
    public static List<LevelData> GenerateCompleteLevelDatabase()
    {
        List<LevelData> levels = new List<LevelData>();

        for (int i = 1; i <= 1000; i++)
        {
            string difficulty = GetDifficultyByLevel(i);
            int targetScore = CalculateTargetScore(i, difficulty);
            int moveLimit = CalculateMoveLimit(i, difficulty);
            List<string> tileTypes = GetTileTypesByLevel(i);

            var level = new LevelData
            {
                levelNumber = i,
                targetScore = targetScore,
                moveLimit = moveLimit,
                difficulty = difficulty,
                tileTypes = tileTypes,
                rewards = new LevelRewards
                {
                    coinsForStar1 = targetScore / 100,
                    coinsForStar2 = targetScore / 50,
                    coinsForStar3 = targetScore / 25,
                    gemsForStar3 = (i % 5 == 0) ? 1 : 0
                }
            };

            levels.Add(level);
        }

        return levels;
    }

    private static string GetDifficultyByLevel(int levelNumber)
    {
        if (levelNumber <= 100) return "Easy";
        if (levelNumber <= 250) return "Medium";
        if (levelNumber <= 500) return "Hard";
        if (levelNumber <= 750) return "Expert";
        return "Impossible";
    }

    private static int CalculateTargetScore(int levelNumber, string difficulty)
    {
        int baseScore = 1000;
        float multiplier = difficulty switch
        {
            "Easy" => 1.0f,
            "Medium" => 1.5f,
            "Hard" => 2.0f,
            "Expert" => 2.5f,
            "Impossible" => 3.0f,
            _ => 1.0f
        };
        
        return (int)(baseScore * multiplier * (1 + levelNumber / 100f));
    }

    private static int CalculateMoveLimit(int levelNumber, string difficulty)
    {
        int baseMovies = 20;
        int reduction = difficulty switch
        {
            "Easy" => 0,
            "Medium" => 2,
            "Hard" => 5,
            "Expert" => 8,
            "Impossible" => 10,
            _ => 0
        };
        
        return baseMovies - reduction + (levelNumber / 100);
    }

    private static List<string> GetTileTypesByLevel(int levelNumber)
    {
        List<string> types = new List<string> { "Red", "Blue", "Green", "Yellow" };
        if (levelNumber > 50) types.Add("Purple");
        if (levelNumber > 150) types.Add("Orange");
        if (levelNumber > 300) types.Add("Pink");
        if (levelNumber > 500) types.Add("Cyan");
        if (levelNumber > 700) types.Add("Lime");
        return types;
    }
}
