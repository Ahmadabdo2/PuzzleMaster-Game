using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameHUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI movesText;
    [SerializeField] private TextMeshProUGUI targetText;
    [SerializeField] private Slider progressBar;
    [SerializeField] private Button pauseButton;
    [SerializeField] private GameObject pausePanel;

    private int currentScore = 0;
    private int movesRemaining = 20;
    private int targetScore = 1000;

    private void Start()
    {
        pauseButton.onClick.AddListener(PauseGame);
        UpdateUI();
    }

    public void AddScore(int points)
    {
        currentScore += points;
        UpdateUI();
        CheckProgress();
    }

    public void DecrementMoves()
    {
        if (movesRemaining > 0)
        {
            movesRemaining--;
            UpdateUI();

            if (movesRemaining == 0)
            {
                GameOver();
            }
        }
    }

    private void UpdateUI()
    {
        levelText.text = $"Level {GameManager.Instance.GetCurrentLevel()}";
        scoreText.text = $"Score: {currentScore}";
        movesText.text = $"Moves: {movesRemaining}";
        targetText.text = $"Target: {targetScore}";
        progressBar.value = (float)currentScore / targetScore;
    }

    private void CheckProgress()
    {
        if (currentScore >= targetScore)
        {
            LevelComplete();
        }
    }

    private void LevelComplete()
    {
        int starsEarned = 3;
        if (movesRemaining < 5) starsEarned = 1;
        else if (movesRemaining < 10) starsEarned = 2;

        int coinsEarned = 100 * starsEarned;
        GameManager.Instance.CompleteLevel(GameManager.Instance.GetCurrentLevel(), starsEarned, coinsEarned);

        Debug.Log($"Level Complete! Stars: {starsEarned}, Coins: {coinsEarned}");
    }

    private void GameOver()
    {
        Debug.Log("Game Over! You ran out of moves.");
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }
}
