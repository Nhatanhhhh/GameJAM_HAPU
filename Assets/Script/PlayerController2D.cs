using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerStateMachine))]
public class PlayerController2D : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;


    private Animator animator;
    private Rigidbody2D rb;
    private PlayerInputActions inputActions;
    private PlayerStateMachine stateMachine;

    // Biến trạng thái
    private bool isGrounded;
    private bool jumpRequested;
    private Vector2 moveInput;

    // Biến cờ (flag) để giao tiếp giữa Update và FixedUpdate


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;

        animator = GetComponent<Animator>();

        inputActions = new PlayerInputActions();
        inputActions.Enable();

        stateMachine = GetComponent<PlayerStateMachine>();
        stateMachine.OnStateChanged += HandleStateChanged; // đăng ký listener
    }

    // Update được dùng để bắt input mỗi frame
    void Update()
    {
        moveInput = inputActions.Player.Move.ReadValue<Vector2>();
        bool isWalking = Mathf.Abs(moveInput.x) > 0.1f;

        // if (isGrounded && isWalking)
        //     stateMachine.CurrentState = PlayerState.Walking;
        // else if (isGrounded && !isWalking)
        //     stateMachine.CurrentState = PlayerState.Idle;


        if ((isWalking && moveInput.x > 0 && transform.localScale.x < 0) ||
            (isWalking && moveInput.x < 0 && transform.localScale.x > 0))
        {
            FlipCharacter();
        }


        if (inputActions.Player.Jump.triggered && isGrounded)
        {
            jumpRequested = true;
        }

        if (inputActions.Player.Attack.triggered)
        {
            Debug.Log("Attack Triggered!");
            stateMachine.CurrentState = PlayerState.Attacking;
        }
    }

    // FixedUpdate được dùng để áp dụng các thay đổi vật lý
    void FixedUpdate()
    {
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

    private void FlipCharacter()
    {

        Vector3 scale = transform.localScale;
        scale.x = Mathf.Sign(moveInput.x) * Mathf.Abs(scale.x);
        transform.localScale = scale;

    }

    public void OnAttackAnimationEnd()
    {
        Debug.Log("Triggered Attack Ended Event");

        // Quay lại Idle sau khi attack xong
        if (stateMachine.CurrentState == PlayerState.Attacking)
        {
            Debug.Log("Attack Animation Ended");
            stateMachine.CurrentState = PlayerState.Idle;
        }
    }

    private void HandleStateChanged(PlayerState oldState, PlayerState newState)
    {
        Debug.Log($"Player changed from {oldState} to {newState}");

        switch (newState)
        {
            case PlayerState.Idle:
                animator.SetBool("isWalking", false);
                animator.SetBool("isAttacking", false);
                break;
            case PlayerState.Walking:
                animator.SetBool("isWalking", true);
                animator.SetBool("isAttacking", false);
                break;
            case PlayerState.Attacking:
                animator.SetBool("isAttacking", true);
                animator.SetBool("isWalking", false);
                break;
        }
    }
}