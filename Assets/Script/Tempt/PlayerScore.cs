using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    // Hàm này sẽ được gọi bởi các đối tượng khác (như PointZone)
    public void AddPoint(int points)
    {
        // Kiểm tra để đảm bảo PointManager đã tồn tại
        if (PointManager.Instance != null)
        {
            // Gọi hàm AddScore từ PointManager singleton
            PointManager.Instance.AddScore(points);
        }
        else
        {
            Debug.LogError("PointManager instance not found!");
        }
    }
}