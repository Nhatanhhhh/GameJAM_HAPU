using UnityEngine;
using TMPro;

public class PointManager : MonoBehaviour
{
    // === SINGLETON PATTERN ===
    public static PointManager Instance { get; private set; }

    public int currentScore { get; private set; }
    // Thời điểm màn chơi bắt đầu
    public float playTime;

    // ĐIỂM SỐ CẦN ĐẠT ĐỂ THẮNG
    public int scoreToWin = 20;



    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

        }
    }

    void Start()
    {
        // Ghi lại thời điểm bắt đầu của game/level
        playTime = Time.time;
        Debug.Log($"Game started at time: {playTime:F2} seconds");
        currentScore = 0;
    }

    void Update()
    {
        float elapsedTime = Time.time - playTime;
        //UIManager.Instance.UpdateTimeUI(elapsedTime);
    }

    public void AddScore(int pointsToAdd)
    {
        currentScore += pointsToAdd;
        Debug.Log($"Score updated! Current Score: {currentScore}");

        //UIManager.Instance.UpdateScoreUI(currentScore);

        // KIỂM TRA ĐIỀU KIỆN THẮNG
        CheckForWinCondition();
    }

    private void CheckForWinCondition()
    {
        // Nếu điểm hiện tại lớn hơn hoặc bằng điểm cần để thắng
        if (currentScore >= scoreToWin)
        {
            // Kiểm tra xem GameManager có tồn tại không
            if (JumpGameManager.Instance != null)
            {
                // Thông báo cho GameManager rằng người chơi đã thắng
                JumpGameManager.Instance.PlayerHasWon();
            }
            else
            {
                Debug.LogError("GameManager instance not found!");
            }
        }
    }




}