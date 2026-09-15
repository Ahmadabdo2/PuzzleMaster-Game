using UnityEngine;
using System.Collections.Generic;
using Firebase.Database;
using System.Linq;

public class BoosterSystem : MonoBehaviour
{
    public static BoosterSystem Instance { get; private set; }

    [System.Serializable]
    public class Booster
    {
        public string boosterId;
        public string name;
        public string description;
        public int powerLevel;
        public int quantity;
    }

    private Dictionary<string, Booster> boosters = new Dictionary<string, Booster>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadBoosters();
    }

    private void LoadBoosters()
    {
        boosters.Add("hammer", new Booster { boosterId = "hammer", name = "Hammer", description = "Remove any tile", powerLevel = 1, quantity = PlayerPrefs.GetInt("booster_hammer", 0) });
        boosters.Add("bomb", new Booster { boosterId = "bomb", name = "Bomb", description = "Remove 9 tiles in a 3x3 area", powerLevel = 2, quantity = PlayerPrefs.GetInt("booster_bomb", 0) });
        boosters.Add("lightning", new Booster { boosterId = "lightning", name = "Lightning", description = "Remove entire row or column", powerLevel = 2, quantity = PlayerPrefs.GetInt("booster_lightning", 0) });
        boosters.Add("rocket", new Booster { boosterId = "rocket", name = "Rocket", description = "Remove multiple rows", powerLevel = 3, quantity = PlayerPrefs.GetInt("booster_rocket", 0) });
        boosters.Add("dynamite", new Booster { boosterId = "dynamite", name = "Dynamite", description = "Massive board explosion", powerLevel = 3, quantity = PlayerPrefs.GetInt("booster_dynamite", 0) });
    }

    public void UseBooster(string boosterId)
    {
        if (boosters.ContainsKey(boosterId))
        {
            if (boosters[boosterId].quantity > 0)
            {
                boosters[boosterId].quantity--;
                PlayerPrefs.SetInt($"booster_{boosterId}", boosters[boosterId].quantity);
                PlayerPrefs.Save();

                Debug.Log($"Used booster: {boosters[boosterId].name}. Remaining: {boosters[boosterId].quantity}");
            }
            else
            {
                Debug.Log($"No {boosters[boosterId].name} available!");
            }
        }
    }

    public Booster GetBooster(string boosterId) => boosters.ContainsKey(boosterId) ? boosters[boosterId] : null;
    public Dictionary<string, Booster> GetAllBoosters() => boosters;
}
