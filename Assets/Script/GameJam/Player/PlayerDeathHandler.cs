using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlayerDeathHandler : MonoBehaviour
{
    [Header("DEATH SETTINGS")]
    [Tooltip("Tag của vùng nước hoặc vùng chết")]
    public string deathZoneTag = "Water";

    private bool isDead = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("isDead status: " + isDead);
        // Nếu người chơi đã chết rồi thì không xử lý lại
        if (isDead) return;

        // Kiểm tra tag vùng chết
        if (other.CompareTag(deathZoneTag))
        {
            isDead = true;
            //Debug.Log("PlayerDeathHandler: Người chơi đã rơi vào vùng chết (" + other.name + ")");



            // Báo cho GameManager
            if (GameManager.Instance != null)
            {
                //Debug.Log("Báo cho GameManager về việc người chơi chết");
                GameManager.Instance.GameOver();
            }

            else
                Debug.LogWarning("GameManager chưa được khởi tạo khi người chơi chết!");
        }
    }
}
