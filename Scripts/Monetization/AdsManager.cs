using UnityEngine;
using GoogleMobileAds.Client;
using GoogleMobileAds.Common;
using System.Collections.Generic;

public class AdsManager : MonoBehaviour
{
    public static AdsManager Instance { get; private set; }

    // AdMob IDs - Replace with your actual IDs
    private string bannerAdUnitId = "ca-app-pub-3940256099942544/6300978111"; // Test ID
    private string interstitialAdUnitId = "ca-app-pub-3940256099942544/1033173712"; // Test ID
    private string rewardedAdUnitId = "ca-app-pub-3940256099942544/5224354917"; // Test ID

    private BannerView bannerView;
    private InterstitialAd interstitialAd;
    private RewardedAd rewardedAd;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        MobileAds.Initialize(initStatus => { });
        LoadBannerAd();
        LoadInterstitialAd();
        LoadRewardedAd();
    }

    public void LoadBannerAd()
    {
        var adRequest = new AdRequest();

        bannerView = new BannerView(bannerAdUnitId, AdSize.Banner, AdPosition.Bottom);
        bannerView.LoadAd(adRequest);

        bannerView.OnBannerAdLoaded += () => Debug.Log("Banner ad loaded.");
        bannerView.OnBannerAdLoadFailed += (LoadAdError error) => Debug.Log("Banner ad failed to load: " + error);
    }

    public void LoadInterstitialAd()
    {
        var adRequest = new AdRequest();

        InterstitialAd.Load(interstitialAdUnitId, adRequest, (InterstitialAd ad, LoadAdError error) =>
        {
            if (error != null || ad == null)
            {
                Debug.Log("Interstitial ad failed to load: " + error);
                return;
            }

            interstitialAd = ad;
            RegisterEventHandlers(ad);
        });
    }

    public void LoadRewardedAd()
    {
        var adRequest = new AdRequest();

        RewardedAd.Load(rewardedAdUnitId, adRequest, (RewardedAd ad, LoadAdError error) =>
        {
            if (error != null || ad == null)
            {
                Debug.Log("Rewarded ad failed to load: " + error);
                return;
            }

            rewardedAd = ad;
            RegisterRewardedEventHandlers(ad);
        });
    }

    public void ShowInterstitialAd()
    {
        if (interstitialAd != null && interstitialAd.CanShowAd())
        {
            interstitialAd.Show();
        }
        else
        {
            Debug.Log("Interstitial ad is not ready.");
            LoadInterstitialAd();
        }
    }

    public void ShowRewardedAd(System.Action onRewardEarned)
    {
        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            rewardedAd.Show((Reward reward) =>
            {
                Debug.Log($"Reward earned: {reward.Amount} {reward.Type}");
                onRewardEarned?.Invoke();
                LoadRewardedAd(); // Reload for next use
            });
        }
        else
        {
            Debug.Log("Rewarded ad is not ready.");
            LoadRewardedAd();
        }
    }

    private void RegisterEventHandlers(InterstitialAd ad)
    {
        ad.OnAdFullScreenContentOpened += () => Debug.Log("Interstitial ad opened.");
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Interstitial ad closed.");
            LoadInterstitialAd();
        };
        ad.OnAdFullScreenContentFailed += (AdError error) => Debug.Log("Interstitial ad failed to open");
    }

    private void RegisterRewardedEventHandlers(RewardedAd ad)
    {
        ad.OnAdFullScreenContentOpened += () => Debug.Log("Rewarded ad opened.");
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Rewarded ad closed.");
            LoadRewardedAd();
        };
        ad.OnAdFullScreenContentFailed += (AdError error) => Debug.Log("Rewarded ad failed to open");
    }

    public void DestroyBannerAd()
    {
        bannerView?.Destroy();
    }

    private void OnDestroy()
    {
        DestroyBannerAd();
    }
}
