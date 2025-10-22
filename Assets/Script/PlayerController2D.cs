using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController2D_Debug : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    private Rigidbody2D rb;
    private PlayerInputActions inputActions;

    // Biến trạng thái
    private bool isGrounded = false;
    private Vector2 moveInput;

    // Biến cờ (flag) để giao tiếp giữa Update và FixedUpdate
    private bool jumpRequested = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;

        inputActions = new PlayerInputActions();
        inputActions.Enable();
    }

    // Update được dùng để bắt input mỗi frame
    void Update()
    {
        // 1. Đọc input di chuyển ở đây
        moveInput = inputActions.Player.Move.ReadValue<Vector2>();

        // 2. Kiểm tra input nhảy. Nếu nút Jump được nhấn và đang chạm đất,
        //    thì bật cờ "yêu cầu nhảy" lên.
        if (inputActions.Player.Jump.triggered && isGrounded)
        {
            jumpRequested = true;
        }
    }

    // FixedUpdate được dùng để áp dụng các thay đổi vật lý
    void FixedUpdate()
    {
        // 3. Áp dụng lực di chuyển trong FixedUpdate
        // Luôn cập nhật vận tốc ngang dựa trên input đọc được từ Update()
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);

        // 4. Nếu có yêu cầu nhảy, thực hiện nhảy và tắt cờ đi
        if (jumpRequested)
        {
            Debug.Log("Jump Triggered!");
            // Sử dụng AddForce với Impulse để có cú nhảy nảy và tự nhiên hơn
            // Cách này tốt hơn việc gán trực tiếp vận tốc.
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            // Tắt cờ ngay lập tức để không nhảy nhiều lần
            jumpRequested = false;
            // Đặt isGrounded thành false ngay khi nhảy để ngăn double jump
            isGrounded = false;
        }
    }

    // Xử lý khi va chạm bắt đầu (chạm đất)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Kiểm tra xem có đang tiếp xúc từ phía trên không
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.normal.y > 0.7f)
                {
                    isGrounded = true;
                    return;
                }
            }
        }
    }

    // Xử lý khi va chạm kết thúc (rời khỏi đất)
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}