using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class SpawnStuff : MonoBehaviour
{
    // Prefab của vật phẩm cần spawn
    public GameObject stuffPrefab;

    // Thời gian chờ giữa mỗi lần spawn (tính bằng giây)
    public float spawnInterval = 2f;

    // Vùng spawn, được xác định bởi BoxCollider2D
    private BoxCollider2D spawnArea;

    void Awake()
    {
        // Lấy component BoxCollider2D trên cùng GameObject
        spawnArea = GetComponent<BoxCollider2D>();
    }

    void Start()
    {
        // Bắt đầu vòng lặp spawn
        // "Spawn" là tên hàm sẽ được gọi
        // 0.5f là thời gian chờ trước lần spawn đầu tiên
        // spawnInterval là khoảng thời gian lặp lại
        InvokeRepeating("Spawn", 0.5f, spawnInterval);
    }

    void Spawn()
    {
        // Lấy thông tin về kích thước và vị trí của vùng spawn
        Bounds bounds = spawnArea.bounds;

        // Tạo một vị trí ngẫu nhiên bên trong vùng spawn
        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomY = Random.Range(bounds.min.y, bounds.max.y);

        Vector2 spawnPosition = new Vector2(randomX, randomY);

        // Tạo một bản sao của stuffPrefab tại vị trí ngẫu nhiên
        Instantiate(stuffPrefab, spawnPosition, Quaternion.identity);
    }
}