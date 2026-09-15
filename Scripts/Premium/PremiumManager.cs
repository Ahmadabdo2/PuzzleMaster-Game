using UnityEngine;
using System.Collections.Generic;

public class PremiumManager : MonoBehaviour
{
    public static PremiumManager Instance { get; private set; }

    [System.Serializable]
    public class PremiumBenefit
    {
        public string benefitId;
        public string description;
        public float multiplier; // coins/exp multiplier
    }

    private bool isPremiumActive = false;
    private System.DateTime premiumExpiryDate;
    private Dictionary<string, PremiumBenefit> premiumBenefits = new Dictionary<string, PremiumBenefit>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadPremiumStatus();
    }

    private void LoadPremiumStatus()
    {
        string premiumExpiryStr = PlayerPrefs.GetString("PremiumExpiry", "");
        if (string.IsNullOrEmpty(premiumExpiryStr))
        {
            isPremiumActive = false;
        }
        else
        {
            premiumExpiryDate = System.DateTime.Parse(premiumExpiryStr);
            isPremiumActive = System.DateTime.Now < premiumExpiryDate;
        }

        SetupPremiumBenefits();
    }

    private void SetupPremiumBenefits()
    {
        premiumBenefits.Add("double_coins", new PremiumBenefit { benefitId = "double_coins", description = "2x Coins from levels", multiplier = 2f });
        premiumBenefits.Add("ad_free", new PremiumBenefit { benefitId = "ad_free", description = "No advertisements", multiplier = 1f });
        premiumBenefits.Add("daily_bonus", new PremiumBenefit { benefitId = "daily_bonus", description = "Extra daily reward", multiplier = 1.5f });
        premiumBenefits.Add("early_access", new PremiumBenefit { benefitId = "early_access", description = "New levels 1 week early", multiplier = 1f });
    }

    public void ActivatePremium(int daysCount = 30)
    {
        premiumExpiryDate = System.DateTime.Now.AddDays(daysCount);
        isPremiumActive = true;
        PlayerPrefs.SetString("PremiumExpiry", premiumExpiryDate.ToString());
        PlayerPrefs.Save();
        
        Debug.Log($"Premium activated for {daysCount} days. Expires: {premiumExpiryDate}");
    }

    public void DeactivatePremium()
    {
        isPremiumActive = false;
        PlayerPrefs.DeleteKey("PremiumExpiry");
        PlayerPrefs.Save();
        Debug.Log("Premium deactivated");
    }

    public bool IsPremiumActive() => isPremiumActive;
    public float GetCoinMultiplier() => isPremiumActive ? 2f : 1f;
    public bool HasNoAds() => isPremiumActive;
    public System.DateTime GetPremiumExpiryDate() => premiumExpiryDate;
}
