using UnityEngine;

public class DragGameManager : MonoBehaviour
{
    public static DragGameManager Instance { get; private set; }
    private int score;

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
        // Gọi sau khi tất cả Awake() khác đã chạy
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ChangeState(GameState.Playing);
            Debug.Log("🎮 DragGameManager: Bắt đầu game (state -> Playing)");
        }
        else
        {
            Debug.LogWarning("⚠️ GameManager chưa được khởi tạo khi DragGameManager Start!");
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        Debug.Log($"🎯 Score: {score}");
        AudioManager.Instance.PlaySFX("Drag_ScorePointSFX");
    }
}
