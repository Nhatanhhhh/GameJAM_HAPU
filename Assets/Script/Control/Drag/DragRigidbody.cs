using UnityEngine;

public class DragAndThrowController : MonoBehaviour
{
    [Header("CONFIG")]
    [SerializeField] private float dragForce = 20f;
    [SerializeField] private float maxDragSpeed = 40f;
    [SerializeField] private float originalGravity = 1f;
    [SerializeField] private float draggingGravity = 0.2f;

    private Camera mainCamera;
    private Rigidbody2D selectedRigidbody;
    private MovingLeaf currentMovingLeaf; // tham chiếu nếu đang kéo lá loại Moving

    private Vector3 lastMousePosition;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, -mainCamera.transform.position.z));

        // --- BẮT ĐẦU KÉO ---
        if (Input.GetMouseButtonDown(0))
        {
            Collider2D hit = Physics2D.OverlapPoint(mouseWorldPosition);

            if (hit != null)
            {
                Debug.Log($"🟢 Đã chạm vào: {hit.name}");

                MovingLeaf movingLeaf = hit.GetComponent<MovingLeaf>();
                if (movingLeaf != null)
                {
                    if (!movingLeaf.CanMove)
                    {
                        Debug.Log($"🚫 Không thể kéo {hit.name} (hết lượt hoặc có người đứng)");
                        return;
                    }

                    if (!movingLeaf.TryConsumeMove())
                        return;

                    currentMovingLeaf = movingLeaf;
                }

                if (hit.attachedRigidbody != null)
                {
                    selectedRigidbody = hit.attachedRigidbody;
                    selectedRigidbody.gravityScale = draggingGravity;
                    selectedRigidbody.linearVelocity = Vector2.zero; // reset vận tốc cũ
                    Debug.Log($"✅ Rigidbody của {hit.name} đã được chọn để kéo");
                }
                else
                {
                    Debug.Log($"⚠️ {hit.name} không có Rigidbody2D — không thể kéo!");
                }
            }
        }

        // --- THẢ RA ---
        if (Input.GetMouseButtonUp(0) && selectedRigidbody != null)
        {
            Debug.Log($"🟠 Thả vật: {selectedRigidbody.name}");

            // ✅ Chỉ khôi phục trọng lực, không thêm vận tốc ném
            selectedRigidbody.gravityScale = originalGravity;
            selectedRigidbody.linearVelocity = Vector2.zero; // đảm bảo không bị "bay"
            selectedRigidbody = null;
            currentMovingLeaf = null;
        }

        lastMousePosition = mouseWorldPosition;
    }

    void FixedUpdate()
    {
        if (selectedRigidbody != null)
        {
            Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPosition.z = 0f;

            Vector2 direction = (mouseWorldPosition - selectedRigidbody.transform.position);
            float distance = direction.magnitude;
            direction.Normalize();

            BoxCollider2D box = selectedRigidbody.GetComponent<BoxCollider2D>();
            if (box != null)
            {
                // 🔍 Check chặn
                RaycastHit2D hit = Physics2D.BoxCast(
                    box.bounds.center,
                    box.bounds.size,
                    0f,
                    direction,
                    distance,
                    LayerMask.GetMask("Ground", "Obstacle")
                );

                if (hit.collider != null)
                {
                    Debug.Log($"🚫 Bị chặn bởi {hit.collider.name}, không thể kéo xuyên qua!");

                    // Giữ nguyên rigidbody, chỉ chặn di chuyển xuyên
                    selectedRigidbody.linearVelocity = Vector2.zero;
                    return; // giữ trạng thái đang kéo
                }

                // --- Nếu không bị chặn, cho phép kéo bình thường ---
                Vector2 targetVelocity = direction * dragForce * distance;

                if (targetVelocity.magnitude > maxDragSpeed)
                    targetVelocity = targetVelocity.normalized * maxDragSpeed;

                selectedRigidbody.linearVelocity = targetVelocity;
            }
        }
    }
}
