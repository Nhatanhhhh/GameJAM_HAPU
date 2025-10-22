using UnityEngine;

public class Stuff : MonoBehaviour
{
    // Số điểm mà vật phẩm này mang lại
    public int pointValue = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Kiểm tra xem có phải người chơi (có tag "Player") đã va chạm không
        if (collision.CompareTag("Player"))
        {
            // Tìm component PlayerScore trên người chơi
            PlayerScore playerScore = collision.GetComponent<PlayerScore>();

            if (playerScore != null)
            {
                // Gọi hàm cộng điểm trên người chơi
                playerScore.AddPoint(pointValue);

                // Tự hủy vật phẩm sau khi được thu thập
                Destroy(gameObject);
            }
        }
    }
}