using UnityEngine;
using UnityEngine.SceneManagement; // Để quản lý scene (tải lại,...)

public class JumpGameManager : MonoBehaviour
{
    // === SINGLETON PATTERN ===
    public static JumpGameManager Instance { get; private set; }

    // (Tùy chọn) Tham chiếu đến màn hình UI chiến thắn

    private bool isGameWon = false;

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
        // Đảm bảo game đang chạy
        Time.timeScale = 1f;
    }

    // Hàm này sẽ được PointManager gọi
    public void PlayerHasWon()
    {
        // Dùng biến bool để đảm bảo hàm này chỉ chạy 1 lần
        if (isGameWon) return;

        isGameWon = true;
        Debug.Log("PLAYER HAS WON THE GAME!");

        // UIManager.Instance.UpdateFinalScore();
        // UIManager.Instance.ShowWinPanle();

        // Dừng game lại
        Time.timeScale = 0f;

    }
}