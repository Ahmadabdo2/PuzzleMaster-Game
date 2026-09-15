using UnityEngine;
using System.Collections.Generic;
using Firebase.Database;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance { get; private set; }

    [System.Serializable]
    public class ShopItem
    {
        public string itemId;
        public string itemName;
        public long gemsPrice;
        public long coinsPrice;
        public int quantity;
        public string itemType; // "booster", "gems", "coins", "package"
    }

    private List<ShopItem> shopItems = new List<ShopItem>();
    private FirebaseDatabase firebaseDB;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        firebaseDB = FirebaseDatabase.DefaultInstance;
        LoadShopItems();
    }

    private void LoadShopItems()
    {
        // 🔹 Gem Packages
        shopItems.Add(new ShopItem { itemId = "gems_50", itemName = "50 Gems", gemsPrice = 0, coinsPrice = 0, quantity = 50, itemType = "gems" });
        shopItems.Add(new ShopItem { itemId = "gems_250", itemName = "250 Gems", gemsPrice = 0, coinsPrice = 0, quantity = 250, itemType = "gems" });
        shopItems.Add(new ShopItem { itemId = "gems_650", itemName = "650 Gems", gemsPrice = 0, coinsPrice = 0, quantity = 650, itemType = "gems" });
        shopItems.Add(new ShopItem { itemId = "gems_1500", itemName = "1500 Gems", gemsPrice = 0, coinsPrice = 0, quantity = 1500, itemType = "gems" });

        // 🔹 Coin Packages
        shopItems.Add(new ShopItem { itemId = "coins_1000", itemName = "1000 Coins", gemsPrice = 0, coinsPrice = 0, quantity = 1000, itemType = "coins" });
        shopItems.Add(new ShopItem { itemId = "coins_5000", itemName = "5000 Coins", gemsPrice = 0, coinsPrice = 0, quantity = 5000, itemType = "coins" });
        shopItems.Add(new ShopItem { itemId = "coins_15000", itemName = "15000 Coins", gemsPrice = 0, coinsPrice = 0, quantity = 15000, itemType = "coins" });

        // 🔹 Boosters
        shopItems.Add(new ShopItem { itemId = "booster_hammer", itemName = "Hammer", gemsPrice = 10, coinsPrice = 500, quantity = 1, itemType = "booster" });
        shopItems.Add(new ShopItem { itemId = "booster_bomb", itemName = "Bomb", gemsPrice = 15, coinsPrice = 750, quantity = 1, itemType = "booster" });
        shopItems.Add(new ShopItem { itemId = "booster_lightning", itemName = "Lightning", gemsPrice = 12, coinsPrice = 600, quantity = 1, itemType = "booster" });
        shopItems.Add(new ShopItem { itemId = "booster_moves", itemName = "Extra Moves (+5)", gemsPrice = 5, coinsPrice = 250, quantity = 5, itemType = "booster" });

        // 🔹 Premium Packages
        shopItems.Add(new ShopItem { itemId = "package_starter", itemName = "Starter Pack", gemsPrice = 0, coinsPrice = 0, quantity = 1, itemType = "package" });
        shopItems.Add(new ShopItem { itemId = "package_pro", itemName = "Pro Pass (30 days)", gemsPrice = 0, coinsPrice = 0, quantity = 1, itemType = "package" });
    }

    public void PurchaseItem(string itemId)
    {
        var item = shopItems.Find(x => x.itemId == itemId);
        if (item == null) return;

        GameManager gm = GameManager.Instance;
        bool canPurchase = false;

        if (item.gemsPrice > 0 && gm.GetGemsBalance() >= item.gemsPrice)
        {
            gm.RemoveGems(item.gemsPrice);
            canPurchase = true;
        }
        else if (item.coinsPrice > 0 && gm.GetCoinsBalance() >= item.coinsPrice)
        {
            gm.RemoveCoins(item.coinsPrice);
            canPurchase = true;
        }

        if (canPurchase)
        {
            HandlePurchase(item);
            Debug.Log($"Successfully purchased: {item.itemName}");
        }
        else
        {
            Debug.Log($"Not enough currency to purchase {item.itemName}");
        }
    }

    private void HandlePurchase(ShopItem item)
    {
        switch (item.itemType)
        {
            case "gems":
                GameManager.Instance.AddGems(item.quantity);
                break;
            case "coins":
                GameManager.Instance.AddCoins(item.quantity);
                break;
            case "booster":
                // Store booster in inventory
                int currentCount = PlayerPrefs.GetInt($"booster_{item.itemId}", 0);
                PlayerPrefs.SetInt($"booster_{item.itemId}", currentCount + item.quantity);
                break;
            case "package":
                // Handle premium package
                PlayerPrefs.SetString($"package_{item.itemId}_purchased", System.DateTime.Now.ToString());
                break;
        }

        PlayerPrefs.Save();
    }

    public List<ShopItem> GetShopItems() => shopItems;
    public ShopItem GetItemById(string itemId) => shopItems.Find(x => x.itemId == itemId);
}
