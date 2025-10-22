using UnityEngine;

public class DragAndThrowController : MonoBehaviour
{
    [Header("CONFIG")]
    [SerializeField] private float dragForce = 20f;          // Lực kéo vật theo chuột, càng cao càng bám
    [SerializeField] private float throwMultiplier = 1.5f;   // Lực ném
    [SerializeField] private float maxDragSpeed = 40f;       // Tốc độ kéo tối đa (để không bị xuyên tường)
    [SerializeField] private float originalGravity = 1f;     // Trọng lực gốc của vật
    [SerializeField] private float draggingGravity = 0.2f;   // Trọng lực khi đang kéo (để kéo lên dễ hơn)

    private Camera mainCamera;
    private Rigidbody2D selectedRigidbody;

    // Biến để tính vận tốc chuột
    private Vector3 lastMousePosition;
    private Vector3 mouseVelocity;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Chuyển đổi tọa độ chuột sang tọa độ thế giới một cách CHUẨN XÁC
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f; // BẮT BUỘC: Đặt Z=0 để nó nằm trên mặt phẳng 2D

        // --- BẮT ĐẦU KÉO ---
        if (Input.GetMouseButtonDown(0))
        {
            // Bắn tia để tìm vật thể
            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPosition, Vector2.zero);
            if (hit.collider != null && hit.collider.attachedRigidbody != null)
            {
                selectedRigidbody = hit.collider.attachedRigidbody;
                // Giảm trọng lực để kéo cho dễ
                selectedRigidbody.gravityScale = draggingGravity;
            }
        }

        // --- THẢ RA ĐỂ NÉM ---
        if (Input.GetMouseButtonUp(0) && selectedRigidbody != null)
        {
            // Trả lại trọng lực như cũ
            selectedRigidbody.gravityScale = originalGravity;
            // Ném vật đi bằng vận tốc cuối cùng của chuột
            selectedRigidbody.linearVelocity = mouseVelocity * throwMultiplier;
            // Thả vật ra
            selectedRigidbody = null;
        }

        // Luôn tính toán vận tốc chuột để có giá trị mới nhất khi thả tay
        mouseVelocity = (mouseWorldPosition - lastMousePosition) / Time.deltaTime;
        lastMousePosition = mouseWorldPosition;
    }

    void FixedUpdate()
    {
        // --- DI CHUYỂN VẬT KHI ĐANG KÉO ---
        if (selectedRigidbody != null)
        {
            // Lấy vị trí chuột lần nữa trong FixedUpdate để đồng bộ với vật lý
            Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPosition.z = 0f;

            // Tính toán vận tốc cần thiết để vật bay tới chuột
            Vector2 targetVelocity = (mouseWorldPosition - selectedRigidbody.transform.position) * dragForce;

            // Giới hạn tốc độ tối đa
            if (targetVelocity.magnitude > maxDragSpeed)
            {
                targetVelocity = targetVelocity.normalized * maxDragSpeed;
            }

            // Gán vận tốc để di chuyển vật
            selectedRigidbody.linearVelocity = targetVelocity;
        }
    }
}