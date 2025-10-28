using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class MovingLeaf : MonoBehaviour
{
    [Header("CONFIG")]
    public LeafType leafType = LeafType.Normal;

    public int maxMoves = 3; // số lần được phép kéo
    private int remainingMoves;

    [Header("STATE")]
    public bool isPlayerOnLeaf = false; // true nếu người chơi đang đứng trên lá
    public bool CanMove => remainingMoves > 0 && !isPlayerOnLeaf; // kiểm tra có được kéo không

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        remainingMoves = maxMoves;
    }

    public bool TryConsumeMove()
    {
        if (remainingMoves > 0)
        {
            remainingMoves--;
            Debug.Log($"🍃 {name} bị kéo, còn lại {remainingMoves} lần!");
            return true;
        }

        Debug.Log($"🚫 {name} không thể di chuyển nữa — đã hết lượt!");
        return false;
    }

    // Khi người chơi đứng lên lá
    public void SetPlayerOnLeaf(bool state)
    {
        isPlayerOnLeaf = state;

        if (isPlayerOnLeaf)
        {
            //rb.bodyType = RigidbodyType2D.Kinematic;
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.constraints = RigidbodyConstraints2D.FreezeAll; // khóa mọi chuyển động
            Debug.Log($"🧍 Người chơi đang đứng trên {name}, khóa vật lý!");
        }
        else
        {
            // Mở lại vật lý
            ApplyDefaultConstraints();
            Debug.Log($"✅ Người chơi rời khỏi {name}, mở lại vật lý!");
        }
    }

    // Áp constraint mặc định theo loại lá
    private void ApplyDefaultConstraints()
    {
        switch (leafType)
        {
            case LeafType.Moving:
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                break;

            case LeafType.MovingY:
                rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
                break;

            case LeafType.MovingX:
                rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
                break;

            default:
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                break;
        }
    }

    // --- Bắt va chạm với người chơi ---
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            // chỉ khi player đứng phía trên lá (theo hướng normal)
            if (collision.contacts.Length > 0 && collision.contacts[0].normal.y < -0.3f)
            {
                SetPlayerOnLeaf(true);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            SetPlayerOnLeaf(false);
        }
    }
}
