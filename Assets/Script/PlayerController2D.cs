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

    private bool isGrounded;
    private bool jumpRequested;
    private Vector2 moveInput;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;

        animator = GetComponent<Animator>();

        inputActions = new PlayerInputActions();
        inputActions.Enable();

        stateMachine = GetComponent<PlayerStateMachine>();
        stateMachine.OnStateChanged += HandleStateChanged;
    }

    void Update()
    {
        ReadInput();
        bool isWalking = Mathf.Abs(moveInput.x) > 0.1f;

        if (isWalking && stateMachine.CurrentState != PlayerState.Walking)
            stateMachine.CurrentState = PlayerState.Walking;
        else if (!isWalking && stateMachine.CurrentState != PlayerState.Idle)
            stateMachine.CurrentState = PlayerState.Idle;

        if ((isWalking && moveInput.x > 0 && transform.localScale.x < 0) ||
            (isWalking && moveInput.x < 0 && transform.localScale.x > 0))
            FlipCharacter();

        HandleJumpInput();
    }

    void ReadInput()
    {
        moveInput = inputActions.Player.Move.ReadValue<Vector2>();
    }

    void HandleJumpInput()
    {
        if (inputActions.Player.Jump.triggered && isGrounded)
        {
            Debug.Log($"[Jump Input] Trigger pressed — isGrounded={isGrounded}");
            jumpRequested = true;
            stateMachine.CurrentState = PlayerState.Jumping;
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);

        if (jumpRequested)
        {
            Debug.Log($"[Jump] Triggered — isGrounded={isGrounded}, velocityY={rb.linearVelocity.y}");
            AudioManager.Instance?.PlaySFX("PlayerJump");
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpRequested = false;
            isGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                Debug.Log($"[CollisionEnter] Ground contact normal={contact.normal}, point={contact.point}");
                if (contact.normal.y > 0.7f)
                {
                    isGrounded = true;
                    animator.SetBool("isJumping", false);
                    Debug.Log($"[Grounded] Player landed on {collision.gameObject.name}");
                    return;
                }
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // Giữ grounded nếu vẫn còn chạm ground
        if (collision.gameObject.CompareTag("Ground"))
        {
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

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log($"[CollisionExit] Left ground: {collision.gameObject.name}");
            // Không set false liền — chờ kiểm tra lại frame sau
            Invoke(nameof(ResetGrounded), 0.02f);
        }
    }

    private void ResetGrounded()
    {
        // Nếu sau 0.02s không có OnCollisionStay, thì mới thực sự rời đất
        isGrounded = false;
    }

    private void FlipCharacter()
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Sign(moveInput.x) * Mathf.Abs(scale.x);
        transform.localScale = scale;
    }

    private void HandleStateChanged(PlayerState oldState, PlayerState newState)
    {
        switch (newState)
        {
            case PlayerState.Idle:
                animator.SetBool("isWalking", false);
                break;
            case PlayerState.Walking:
                animator.SetBool("isWalking", true);
                break;
            case PlayerState.Jumping:
                animator.SetBool("isJumping", true);
                break;
        }
    }

    private void OnEnable() => inputActions.Player.Enable();
    private void OnDisable() => inputActions.Player.Disable();

    private void OnDestroy()
    {
        inputActions.Dispose();
        stateMachine.OnStateChanged -= HandleStateChanged;
    }
}
