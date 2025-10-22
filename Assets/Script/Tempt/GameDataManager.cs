using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    // === SINGLETON PATTERN ===
    public static GameDataManager Instance { get; private set; }

    // Dữ liệu sẽ được lưu lại khi kết thúc màn chơi
    public int finalScore { get; private set; }
    public float finalTime { get; private set; }

    // Thời điểm màn chơi bắt đầu
    private float startTime;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            // Không hủy đối tượng này khi tải scene mới
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        // Ghi lại thời điểm bắt đầu của game/level
        startTime = Time.time;
    }

    // Hàm này được GameManager gọi khi người chơi thắng
    public void RecordSessionData(int score)
    {
        finalScore = score;
        // Tính tổng thời gian chơi bằng cách lấy thời gian hiện tại trừ đi thời gian bắt đầu
        finalTime = Time.time - startTime;

        Debug.Log($"Session data recorded! Score: {finalScore}, Time: {finalTime:F2} seconds.");
    }
}