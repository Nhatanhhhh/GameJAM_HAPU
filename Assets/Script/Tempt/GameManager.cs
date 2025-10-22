using UnityEngine;
using UnityEngine.SceneManagement; // Để quản lý scene (tải lại,...)

public class GameManager : MonoBehaviour
{
    // === SINGLETON PATTERN ===
    public static GameManager Instance { get; private set; }

    // (Tùy chọn) Tham chiếu đến màn hình UI chiến thắng
    public GameObject winScreenUI;

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
        // Ẩn màn hình chiến thắng khi bắt đầu game
        if (winScreenUI != null)
        {
            winScreenUI.SetActive(false);
        }
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

        // === DÒNG MỚI ĐƯỢC THÊM VÀO ===
        // Trước khi dừng game, hãy yêu cầu GameDataManager lưu lại dữ liệu.
        // Lấy điểm từ PointManager.
        GameDataManager.Instance.RecordSessionData(PointManager.Instance.currentScore);
        // ===================================

        // Dừng game lại
        Time.timeScale = 0f;

        // Hiển thị màn hình chiến thắng
        if (winScreenUI != null)
        {
            winScreenUI.SetActive(true);
        }

        // Tại đây bạn có thể thêm các hành động khác như:
        // - Chơi âm thanh chiến thắng
        // - Lưu điểm cao
        // - Tải scene tiếp theo sau một khoảng thời gian
    }
}