using UnityEngine;
using TMPro;

public class PointManager : MonoBehaviour
{
    // === SINGLETON PATTERN ===
    public static PointManager Instance { get; private set; }

    public int currentScore { get; private set; }

    // ĐIỂM SỐ CẦN ĐẠT ĐỂ THẮNG
    public int scoreToWin = 20;

    public TextMeshProUGUI scoreText;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        currentScore = 0;
        UpdateScoreUI();
    }

    public void AddScore(int pointsToAdd)
    {
        currentScore += pointsToAdd;
        Debug.Log($"Score updated! Current Score: {currentScore}");
        UpdateScoreUI();

        // KIỂM TRA ĐIỀU KIỆN THẮNG
        CheckForWinCondition();
    }

    private void CheckForWinCondition()
    {
        // Nếu điểm hiện tại lớn hơn hoặc bằng điểm cần để thắng
        if (currentScore >= scoreToWin)
        {
            // Kiểm tra xem GameManager có tồn tại không
            if (GameManager.Instance != null)
            {
                // Thông báo cho GameManager rằng người chơi đã thắng
                GameManager.Instance.PlayerHasWon();
            }
            else
            {
                Debug.LogError("GameManager instance not found!");
            }
        }
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + currentScore.ToString();
        }
    }
}