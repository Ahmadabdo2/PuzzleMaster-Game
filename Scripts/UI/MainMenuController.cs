using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI starsText;
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private TextMeshProUGUI gemsText;
    [SerializeField] private Button playButton;
    [SerializeField] private Button shopButton;
    [SerializeField] private Button leaderboardButton;
    [SerializeField] private Button settingsButton;

    private void Start()
    {
        UpdateUI();
        playButton.onClick.AddListener(() => SceneManager.LoadScene("GameScene"));
        shopButton.onClick.AddListener(OpenShop);
        leaderboardButton.onClick.AddListener(OpenLeaderboard);
        settingsButton.onClick.AddListener(OpenSettings);
    }

    private void UpdateUI()
    {
        GameManager gm = GameManager.Instance;
        levelText.text = $"Level {gm.GetCurrentLevel()}";
        starsText.text = $"⭐ {gm.GetTotalStars()}";
        coinsText.text = $"💰 {gm.GetCoinsBalance()}";
        gemsText.text = $"💎 {gm.GetGemsBalance()}";
    }

    private void OpenShop()
    {
        SceneManager.LoadScene("ShopScene");
    }

    private void OpenLeaderboard()
    {
        SceneManager.LoadScene("LeaderboardScene");
    }

    private void OpenSettings()
    {
        SceneManager.LoadScene("SettingsScene");
    }
}
