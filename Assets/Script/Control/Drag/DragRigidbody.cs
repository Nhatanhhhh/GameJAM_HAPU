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
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(
        new Vector3(Input.mousePosition.x, Input.mousePosition.y, -mainCamera.transform.position.z));


        // --- BẮT ĐẦU KÉO ---
        if (Input.GetMouseButtonDown(0))
        {
            Collider2D hit = Physics2D.OverlapPoint(mouseWorldPosition);

            if (hit != null)
            {
                Debug.Log($"🟢 Đã chạm vào: {hit.name}");

                if (hit.attachedRigidbody != null)
                {
                    selectedRigidbody = hit.attachedRigidbody;
                    selectedRigidbody.gravityScale = draggingGravity;
                    Debug.Log($"✅ Rigidbody của {hit.name} đã được chọn để kéo");
                }
                else
                {
                    Debug.Log($"⚠️ {hit.name} không có Rigidbody2D — không thể kéo!");
                }
            }
            else
            {
                Debug.Log("❌ Không chạm vào vật thể nào!");
            }

        }


        // --- THẢ RA ĐỂ NÉM ---
        if (Input.GetMouseButtonUp(0) && selectedRigidbody != null)
        {
            Debug.Log($"🟠 Thả vật: {selectedRigidbody.name}");

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