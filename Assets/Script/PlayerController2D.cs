using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController2D_Minimal : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    private Rigidbody2D rb;
    private PlayerInputActions inputActions;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inputActions = new PlayerInputActions();
        inputActions.Enable(); // bật input luôn
    }

    void FixedUpdate()
    {
        // Lấy input di chuyển
        Vector2 moveInput = inputActions.Player.Move.ReadValue<Vector2>();

        // Cập nhật vận tốc theo trục X
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);

        // Nhảy khi nhấn Jump
        if (inputActions.Player.Jump.triggered)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }
}
