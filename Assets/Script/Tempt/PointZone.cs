using UnityEngine;

public class PointZone : MonoBehaviour
{
    // Số điểm sẽ được cộng khi người chơi đi vào vùng này
    // Bạn có thể thay đổi giá trị này trong Inspector cho mỗi PointZone khác nhau
    public int pointsToAdd = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Kiểm tra xem đối tượng va chạm có phải là người chơi không
        if (collision.gameObject.CompareTag("Player"))
        {
            // Cố gắng lấy component PlayerScore từ đối tượng người chơi
            PlayerScore playerScore = collision.gameObject.GetComponent<PlayerScore>();

            // Nếu tìm thấy component PlayerScore (playerScore không null)
            if (playerScore != null)
            {
                Debug.Log($"Player entered the point zone! Adding {pointsToAdd} points.");

                // Gọi hàm AddPoint trên người chơi và truyền vào số điểm
                playerScore.AddPoint(pointsToAdd);

                // Vô hiệu hóa GameObject này để người chơi không thể nhận điểm lần nữa
                gameObject.SetActive(false);
            }
            else
            {
                Debug.LogWarning("Player object does not have a PlayerScore component!");
            }
        }
    }
}